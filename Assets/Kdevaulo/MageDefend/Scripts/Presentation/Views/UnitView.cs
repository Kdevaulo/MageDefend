using System;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class UnitView : MonoBehaviour
    {
        public event Action<Collision> CollisionEntered;

        [SerializeField] private Rigidbody _rigidbody;

        public void Move(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }

        public void SetRotation(Quaternion rotation)
        {
            _rigidbody.rotation = rotation.normalized;
        }

        private void OnCollisionEnter(Collision other)
        {
            CollisionEntered?.Invoke(other);
        }
    }
}