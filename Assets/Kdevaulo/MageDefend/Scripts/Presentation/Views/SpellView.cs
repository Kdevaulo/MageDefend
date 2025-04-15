using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class SpellView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        public void Move(Vector3 position)
        {
            _rigidbody.MovePosition(position);
        }
    }
}