using UnityEngine;
using UnityEngine.InputSystem;

namespace Kdevaulo.MageDefend.Presentation
{
    public class GameContext : MonoBehaviour
    {
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
        [field: SerializeField] public Transform Parent { get; private set; }

        [field: SerializeField] public SpellsData SpellsData { get; private set; }
        [field: SerializeField] public UnitsData EnemiesData { get; private set; }
        [field: SerializeField] public UnitsData PlayerData { get; private set; }

        [field: SerializeField] public PlayerView PlayerPrefab { get; private set; }
        [field: SerializeField] public EnemyView EnemyPrefab { get; private set; }
    }
}