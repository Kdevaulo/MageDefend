using Kdevaulo.MageDefend.Model;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    [CreateAssetMenu(fileName = nameof(UnitsData), menuName = nameof(MageDefend) + "/" + nameof(UnitsData))]
    public class UnitsData : ScriptableObject
    {
        [field: SerializeField] public UnitConfig[] Datasets;
    }
}