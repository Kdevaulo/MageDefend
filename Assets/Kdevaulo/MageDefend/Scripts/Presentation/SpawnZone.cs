using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class SpawnZone : MonoBehaviour
    {
        [SerializeField] private Bounds _bounds;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_bounds.center, _bounds.size);
        }

        public Bounds GetBounds()
        {
            return _bounds;
        }
    }
}