using System;

using Kdevaulo.MageDefend.Model;

using UnityEngine;

using Object = UnityEngine.Object;

namespace Kdevaulo.MageDefend.Presentation
{
    public class SpellController
    {
        public bool IsFinished => _lifetime <= 0;

        private readonly LocationController _locationController;
        private readonly SpellView _view;
        private readonly Vector3 _moveDirection;
        private readonly float _moveSpeed;
        private readonly float _damage;

        private float _lifetime;

        public SpellController(Vector3 direction, SpellParameters spellParameters, SpellView view,
            LocationController locationController)
        {
            _locationController = locationController;
            _moveDirection = direction;
            _moveSpeed = spellParameters.MoveSpeed;
            _lifetime = spellParameters.Lifetime;
            _damage = spellParameters.Damage;
            _view = view;
        }

        public void Initialize()
        {
            _view.CollisionEntered += HandleCollision;
        }

        public void Dispose()
        {
            _view.CollisionEntered -= HandleCollision;
        }

        private void HandleCollision(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<EnemyView>(out var enemyView))
            {
                var model = _locationController.GetUnitModel(enemyView);
                model.TakeDamage(_damage);

                if (model.Hp.Value <= 0)
                {
                    _locationController.DestroyUnit(model);
                }
            }
        }

        public void Tick()
        {
            DecreaseLifetime(Time.deltaTime);

            var step = _moveDirection * (Time.deltaTime * _moveSpeed);
            _view.Move(step);
        }

        private void DecreaseLifetime(float value)
        {
            var subtrahend = Math.Abs(value);
            _lifetime -= subtrahend;

            if (_lifetime < 0)
            {
                _lifetime = 0;
                Object.Destroy(_view?.gameObject);
            }
        }
    }
}