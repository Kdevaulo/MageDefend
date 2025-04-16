using System;
using System.Collections.Generic;
using System.Linq;

using Kdevaulo.MageDefend.Model;

using UnityEngine;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Kdevaulo.MageDefend.Presentation
{
    public class EnemiesController
    {
        private readonly IPlayerContextProvider _playerContextProvider;
        private readonly ContactHandler _contactHandler;
        private readonly EnemyData _enemiesVisualData;
        private readonly UnitsData _unitsData;
        private readonly SpawnZone _spawnZone;
        private readonly Transform _parent;
        private readonly Camera _camera;

        private List<Enemy> _activeEnemies = new List<Enemy>();

        private int _maxCount;

        public EnemiesController(GameContext gameContext, IPlayerContextProvider playerContextProvider,
            ContactHandler contactHandler)
        {
            _playerContextProvider = playerContextProvider;
            _enemiesVisualData = gameContext.EnemiesVisualData;
            _contactHandler = contactHandler;
            _unitsData = gameContext.EnemiesData;
            _spawnZone = gameContext.SpawnZone;
            _camera = gameContext.Camera;
            _parent = gameContext.Parent;
        }

        public void Initialize(int count)
        {
            _maxCount = count;
            _contactHandler.Initialize(_activeEnemies);
        }

        public void Dispose()
        {
        }

        public void Tick()
        {
            if (_activeEnemies.Count < _maxCount)
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

        private void SpawnEnemy(Vector3 position)
        {
            var length = _unitsData.Datasets.Length;
            var chosenData = _unitsData.Datasets[Random.Range(0, length)];

            var id = chosenData.Id;
            var item = _enemiesVisualData.EnemyConfigCollection.FirstOrDefault(x => x.Id == id);

            if (item == null)
                throw new Exception(nameof(EnemiesController));

            var model = new UnitModel(chosenData);
            var view = Object.Instantiate(item.EnemyPrefab, position, Quaternion.identity, _parent);
            var controller = new EnemyController(model, view, _contactHandler);

            var enemy = new Enemy(model, view, controller);
            _activeEnemies.Add(enemy);
            controller.Initialize(_playerContextProvider.Target);
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