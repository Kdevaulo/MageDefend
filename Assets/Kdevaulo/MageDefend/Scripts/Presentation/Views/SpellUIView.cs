using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class SpellUIView : MonoBehaviour
    {
        [SerializeField] private GameObject _outline;

        public void Enable()
        {
            _outline.SetActive(true);
        }

        public void Disable()
        {
            _outline.SetActive(false);
        }
    }
}