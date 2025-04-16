using Kdevaulo.MageDefend.Model;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class EnemyController
    {
        private const float MinFollowDistance = 0.01f;

        private readonly UnitModel _enemyModel;
        private readonly EnemyView _enemyView;
        private readonly LocationController _locationController;
        private Transform _target;

        public EnemyController(UnitModel enemyModel, EnemyView enemyView, LocationController locationController)
        {
            _locationController = locationController;
            _enemyModel = enemyModel;
            _enemyView = enemyView;
        }

        public void Initialize(Transform target)
        {
            _target = target;
            _enemyView.CollisionEntered += HandleCollision;
        }

        public void Dispose()
        {
            _target = null;
            _enemyView.CollisionEntered -= HandleCollision;
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

        private void HandleCollision(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerView>(out var playerView))
            {
                var model = _locationController.GetUnitModel(playerView);

                if (model == null)
                    return;

                var damage = _enemyModel.Damage.Value;
                model.TakeDamage(damage);

                if (model.Hp.Value <= 0)
                {
                    Debug.Log("Game Over");
                }
            }
        }
    }
}