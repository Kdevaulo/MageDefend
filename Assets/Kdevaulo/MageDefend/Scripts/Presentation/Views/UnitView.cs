using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        public void Move(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }

        public void SetRotation(Quaternion rotation)
        {
            _rigidbody.rotation = rotation.normalized;
        }
    }
}