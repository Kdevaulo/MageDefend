using System;
using System.Collections.Generic;
using System.Linq;

using Kdevaulo.MageDefend.Model;

using UnityEngine;
using UnityEngine.Assertions;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Kdevaulo.MageDefend.Presentation
{
    public class LocationController
    {
        public UnitModel PlayerModel => _playerModel;
        public PlayerView PlayerView => _playerView;

        private const int MaxEnemiesCount = 10;

        private readonly List<Enemy> _activeEnemies = new List<Enemy>();

        private readonly EnemyConfig _enemiesVisualData;
        private readonly GameContext _gameContext;
        private readonly InputSystem _playerInput;
        private readonly UnitsConfig _unitsConfig;
        private readonly SpawnZone _spawnZone;
        private readonly Transform _parent;
        private readonly Camera _camera;

        private PlayerController _playerController;
        private UnitModel _playerModel;
        private PlayerView _playerView;

        public LocationController(GameContext gameContext)
        {
            _gameContext = gameContext;
            _enemiesVisualData = gameContext.EnemiesVisualData;
            _unitsConfig = gameContext.EnemiesConfig;
            _spawnZone = gameContext.SpawnZone;
            _camera = gameContext.Camera;
            _parent = gameContext.Parent;
        }

        public void Initialize()
        {
            SpawnPlayer();
        }

        public void Dispose()
        {
            _playerController.Dispose();

            foreach (var enemy in _activeEnemies)
            {
                enemy.EnemyController?.Dispose();
                Object.Destroy(enemy.EnemyView.gameObject);
            }

            _activeEnemies.Clear();
        }

        public void Tick()
        {
            _playerController.Tick();

            if (_activeEnemies.Count < MaxEnemiesCount)
            {
                var bounds = _spawnZone.GetBounds();

                var pointOnEdge = GetRandomPointOnEdge(bounds);

                if (IsVisibleFromCamera(_camera, pointOnEdge))
                {
                    return;
                }

                SpawnEnemy(pointOnEdge);
            }

            foreach (var enemy in _activeEnemies)
            {
                enemy.EnemyController.Tick();
            }
        }

        public UnitModel GetUnitModel(UnitView unitView)
        {
            if (unitView is PlayerView)
            {
                return PlayerModel;
            }

            var enemy = _activeEnemies.FirstOrDefault(x => x.EnemyView == unitView as EnemyView);
            return enemy?.EnemyModel;
        }

        private void SpawnPlayer()
        {
            _playerView = Object.Instantiate(_gameContext.PlayerPrefab, _gameContext.Parent);
            var playerData = _gameContext.PlayerConfig.Datasets.FirstOrDefault();
            Assert.IsNotNull(playerData);
            _playerModel = new UnitModel(playerData);
            _playerModel.Died += () => Debug.Log("Game Over");

            _playerController = new PlayerController(_gameContext, this, _playerView, _playerModel);
            _playerController.Initialize();
        }

        private void DestroyUnit(UnitModel model)
        {
            var enemy = _activeEnemies.FirstOrDefault(x => x.EnemyModel == model);

            if (enemy == null)
                return;

            enemy.EnemyController.Dispose();
            Object.Destroy(enemy.EnemyView.gameObject);
            _activeEnemies.Remove(enemy);
        }

        private void SpawnEnemy(Vector3 position)
        {
            var length = _unitsConfig.Datasets.Length;
            var chosenData = _unitsConfig.Datasets[Random.Range(0, length)];

            var id = chosenData.Id;
            var item = _enemiesVisualData.EnemyParametersCollection.FirstOrDefault(x => x.Id == id);

            if (item == null)
                throw new Exception(nameof(LocationController));

            var model = new UnitModel(chosenData);
            var view = Object.Instantiate(item.EnemyPrefab, position, Quaternion.identity, _parent);
            var controller = new EnemyController(model, view, this);

            var enemy = new Enemy(model, view, controller);
            _activeEnemies.Add(enemy);
            model.Died += () => DestroyUnit(model);
            controller.Initialize(_playerView.transform);
        }

        private Vector3 GetRandomPointOnEdge(Bounds bounds)
        {
            var center = bounds.center;
            var extents = bounds.extents;

            var face = Random.Range(0, 4);

            var x = Random.Range(-extents.x, extents.x);
            var y = Random.Range(-extents.y, extents.y);
            var z = Random.Range(-extents.z, extents.z);

            return face switch
            {
                0 => new Vector3(center.x + extents.x, center.y + y, center.z + z),
                1 => new Vector3(center.x - extents.x, center.y + y, center.z + z),
                2 => new Vector3(center.x + x, center.y + y, center.z + extents.z),
                3 => new Vector3(center.x + x, center.y + y, center.z - extents.z),
                _ => center
            };
        }

        private bool IsVisibleFromCamera(Camera camera, Vector3 worldPosition)
        {
            var viewportPoint = camera.WorldToViewportPoint(worldPosition);

            var isInView =
                viewportPoint is { z: > 0, x: >= 0 and <= 1, y: >= 0 and <= 1 };

            return isInView;
        }
    }
}