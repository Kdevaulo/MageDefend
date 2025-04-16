using Kdevaulo.MageDefend.Model;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    [CreateAssetMenu(fileName = nameof(UnitsConfig), menuName = nameof(MageDefend) + "/" + nameof(UnitsConfig))]
    public class UnitsConfig : ScriptableObject
    {
        [field: SerializeField] public UnitParameters[] Datasets;
    }
}