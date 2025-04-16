using Kdevaulo.MageDefend.Model;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class EnemyController
    {
        private const float MinFollowDistance = 0.01f;

        private readonly ContactHandler _contactHandler;
        private readonly UnitModel _enemyModel;
        private readonly EnemyView _enemyView;
        private Transform _target;

        public EnemyController(UnitModel enemyModel, EnemyView enemyView, ContactHandler contactHandler)
        {
            _enemyModel = enemyModel;
            _enemyView = enemyView;
            _contactHandler = contactHandler;
        }

        public void Initialize(Transform target)
        {
            _target = target;
            _enemyView.CollisionEntered += SendContactInfo;
        }

        public void Dispose()
        {
            _target = null;
            _enemyView.CollisionEntered -= SendContactInfo;
        }

        public void Tick()
        {
            if (_target != null)
            {
                var direction = _target.position - _enemyView.transform.position;

                if (direction.magnitude > MinFollowDistance)
                {
                    _enemyView.Move(direction.normalized * _enemyModel.MoveSpeed.Value);
                }
            }
        }

        private void SendContactInfo(Collision collision)
        {
            _contactHandler.HandleContact(_enemyView, collision.gameObject);
        }
    }
}