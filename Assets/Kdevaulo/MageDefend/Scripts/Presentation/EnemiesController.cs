using System;
using System.Collections.Generic;
using System.Linq;

using Kdevaulo.MageDefend.Model;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Kdevaulo.MageDefend.Presentation
{
    public class EnemiesController
    {
        private readonly EnemyData _enemiesVisualData;
        private readonly UnitsData _unitsData;

        private List<Enemy> _activeEnemies = new List<Enemy>();

        private int _maxCount;

        public EnemiesController(UnitsData unitsData, EnemyData enemiesVisualData)
        {
            _unitsData = unitsData;
            _enemiesVisualData = enemiesVisualData;
        }

        public void Initialize(int count)
        {
            _maxCount = count;
        }

        public void Dispose()
        {
        }

        public void Tick()
        {
            if (_activeEnemies.Count < _maxCount)
            {
                SpawnEnemy();
            }

            foreach (var enemy in _activeEnemies)
            {
                enemy.EnemyController.Tick();
            }
        }

        private void SpawnEnemy()
        {
            var length = _unitsData.Datasets.Length;
            var chosenData = _unitsData.Datasets[Random.Range(0, length)];

            var id = chosenData.Id;
            var item = _enemiesVisualData.EnemyConfigCollection.FirstOrDefault(x => x.Id == id);

            if (item == null)
                throw new Exception(nameof(EnemiesController));

            var model = new UnitModel(chosenData);
            var view = Object.Instantiate(item.EnemyPrefab);
            var controller = new EnemyController(model, view);

            var enemy = new Enemy(model, view, controller);
            _activeEnemies.Add(enemy);

            controller.Initialize();
        }
    }
}