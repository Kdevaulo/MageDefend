using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    [CreateAssetMenu(fileName = nameof(EnemyConfig), menuName = nameof(MageDefend) + "/" + nameof(EnemyConfig))]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyParameters[] EnemyParametersCollection { get; private set; }
    }
}