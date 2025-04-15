using Kdevaulo.MageDefend.Model;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class EnemyController
    {
        private const float MinFollowDistance = 0.01f;

        private readonly UnitModel _enemyModel;
        private readonly EnemyView _enemyView;
        private Transform _target;

        public EnemyController(UnitModel enemyModel, EnemyView enemyView)
        {
            _enemyModel = enemyModel;
            _enemyView = enemyView;
        }

        public void Initialize(Transform target)
        {
            _target = target;
        }

        public void Dispose()
        {
            _target = null;
        }

        public void Tick()
        {
            if (_target != null)
            {
                var direction = _target.position - _enemyView.transform.position;

                if (direction.magnitude > MinFollowDistance)
                {
                    _enemyView.Move(direction.normalized * _enemyModel.MoveSpeed);
                }
            }
        }
    }
}