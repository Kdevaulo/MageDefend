using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        public void Move(Vector3 position)
        {
            _rigidbody.MovePosition(position);
        }

        public void Rotate(Quaternion rotation)
        {
            _rigidbody.rotation = rotation;
        }
    }
}