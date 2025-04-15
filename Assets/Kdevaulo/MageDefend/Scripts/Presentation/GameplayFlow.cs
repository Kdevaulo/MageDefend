using System.Linq;

using Kdevaulo.MageDefend.Model;

using UnityEngine.Assertions;

namespace Kdevaulo.MageDefend.Presentation
{
    public class GameplayFlow
    {
        private PlayerController _playerController;
        private InputHandler _inputHandler;
        private UnitModel _playerModel;

        public void Initialize(GameContext context)
        {
            var playerDataset = context.PlayerData.Datasets.FirstOrDefault();
            Assert.IsNotNull(playerDataset);

            var player = UnityEngine.Object.Instantiate(context.PlayerPrefab, context.Parent);
            _playerModel = new UnitModel(playerDataset);
            _inputHandler = new InputHandler(context.PlayerInput);
            _playerController = new PlayerController(_inputHandler, _playerModel, player);

            _inputHandler.Initialize();
            _playerController.Initialize();
        }

        public void Dispose()
        {
            _inputHandler.Dispose();
            _playerController.Dispose();
        }

        public void Tick()
        {
            _playerController.Tick();
        }
    }
}