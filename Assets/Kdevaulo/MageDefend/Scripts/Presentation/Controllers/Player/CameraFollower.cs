using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Camera _camera;

        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void Tick()
        {
            if (_target)
            {
                _camera.transform.position = _target.position + _offset;
            }
        }
    }
}