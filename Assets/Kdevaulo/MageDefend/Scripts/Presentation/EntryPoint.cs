using System;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    [RequireComponent(typeof(GameContext))]
    public class EntryPoint : MonoBehaviour
    {
        private GameplayFlow _gameplayFlow;
        private GameContext _gameContext;

        private void Awake()
        {
            _gameplayFlow = new GameplayFlow();

            _gameContext = GetComponent<GameContext>();
        }

        private void Start()
        {
            _gameplayFlow.Initialize(_gameContext);
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