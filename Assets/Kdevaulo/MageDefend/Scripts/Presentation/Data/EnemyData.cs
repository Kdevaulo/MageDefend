using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    [CreateAssetMenu(fileName = nameof(EnemyData), menuName = nameof(MageDefend) + "/" + nameof(EnemyData))]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public EnemyConfig[] EnemyConfigCollection { get; private set; }
    }
}