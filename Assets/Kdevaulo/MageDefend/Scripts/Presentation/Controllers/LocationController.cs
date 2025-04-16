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
        public List<Enemy> ActiveEnemies = new List<Enemy>();
        public UnitModel PlayerModel => _playerModel;
        public PlayerView PlayerView => _playerView;

        private readonly GameContext _gameContext;
        private readonly EnemyConfig _enemiesVisualData;
        private readonly UnitModel _playerModel;
        private readonly UnitsConfig _unitsConfig;
        private readonly SpawnZone _spawnZone;
        private readonly Transform _parent;
        private readonly Camera _camera;

        private PlayerView _playerView;

        private int _maxCount;

        public LocationController(GameContext gameContext)
        {
            _enemiesVisualData = gameContext.EnemiesVisualData;
            _gameContext = gameContext;
            _unitsConfig = gameContext.EnemiesConfig;
            _spawnZone = gameContext.SpawnZone;
            _camera = gameContext.Camera;
            _parent = gameContext.Parent;

            var playerData = gameContext.PlayerConfig.Datasets.FirstOrDefault();
            Assert.IsNotNull(playerData);
            _playerModel = new UnitModel(playerData);
        }

        public void Initialize(int count)
        {
            _maxCount = count;
        }

        public void Dispose()
        {
            foreach (var enemy in ActiveEnemies)
            {
                enemy.EnemyController?.Dispose();
                Object.Destroy(enemy.EnemyView.gameObject);
            }

            ActiveEnemies.Clear();
        }

        public void Tick()
        {
            if (ActiveEnemies.Count < _maxCount)
            {
                var bounds = _spawnZone.GetBounds();

                var pointOnEdge = GetRandomPointOnEdge(bounds);

                if (IsVisibleFromCamera(_camera, pointOnEdge))
                {
                    return;
                }

                SpawnEnemy(pointOnEdge);
            }

            foreach (var enemy in ActiveEnemies)
            {
                enemy.EnemyController.Tick();
            }
        }

        public void SpawnPlayer()
        {
            _playerView = Object.Instantiate(_gameContext.PlayerPrefab, _gameContext.Parent);
        }

        public UnitModel GetUnitModel(UnitView unitView)
        {
            if (unitView is PlayerView)
            {
                return PlayerModel;
            }

            var enemy = ActiveEnemies.FirstOrDefault(x => x.EnemyView == unitView as EnemyView);
            return enemy?.EnemyModel;
        }

        public void DestroyUnit(UnitModel model)
        {
            var enemy = ActiveEnemies.FirstOrDefault(x => x.EnemyModel == model);

            if (enemy == null)
                return;

            enemy.EnemyController.Dispose();
            Object.Destroy(enemy.EnemyView.gameObject);
            ActiveEnemies.Remove(enemy);
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
            ActiveEnemies.Add(enemy);
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