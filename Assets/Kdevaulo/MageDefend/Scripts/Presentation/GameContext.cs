using UnityEngine;
using UnityEngine.InputSystem;

namespace Kdevaulo.MageDefend.Presentation
{
    public class GameContext : MonoBehaviour
    {
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
        [field: SerializeField] public Transform UISpellParent { get; private set; }
        [field: SerializeField] public Transform Parent { get; private set; }
        [field: SerializeField] public Camera Camera { get; private set; }

        [field: SerializeField] public SpellsConfig SpellsConfig { get; private set; }
        [field: SerializeField] public UnitsConfig EnemiesConfig { get; private set; }
        [field: SerializeField] public UnitsConfig PlayerConfig { get; private set; }
        [field: SerializeField] public EnemyConfig EnemiesVisualData { get; private set; }

        [field: SerializeField] public PlayerView PlayerPrefab { get; private set; }

        [field: SerializeField] public CameraFollower CameraFollower { get; private set; }
        [field: SerializeField] public SpawnZone SpawnZone { get; private set; }
    }
}