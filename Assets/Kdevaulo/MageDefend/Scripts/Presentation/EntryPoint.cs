using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    [RequireComponent(typeof(GameContext))]
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameContext _gameContext;

        private GameplayFlow _gameplayFlow;

        private void Awake()
        {
            _gameplayFlow = new GameplayFlow(_gameContext);
        }

        private void Start()
        {
            _gameplayFlow.Initialize();
        }

        private void Update()
        {
            _gameplayFlow.Tick();
        }

        private void OnDestroy()
        {
            _gameplayFlow.Dispose();
        }
    }
}