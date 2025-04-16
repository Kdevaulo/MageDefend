using System;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class SpellView : MonoBehaviour
    {
        public event Action<Collision> CollisionEntered;

        [SerializeField] private Rigidbody _rigidbody;

        public void Move(Vector3 position)
        {
            _rigidbody.MovePosition(_rigidbody.position + position);
        }

        private void OnCollisionEnter(Collision other)
        {
            CollisionEntered?.Invoke(other);
        }
    }
}