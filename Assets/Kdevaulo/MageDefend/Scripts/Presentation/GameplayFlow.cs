using System.Linq;

using Kdevaulo.MageDefend.Model;

using UnityEngine.Assertions;

namespace Kdevaulo.MageDefend.Presentation
{
    public class GameplayFlow
    {
        private PlayerController _playerController;
        private SpellController _spellController;

        private SpellsModel _spellsModel;
        private UnitModel _playerModel;

        private InputSystem _playerInput;

        public void Initialize(GameContext context)
        {
            var playerDataset = context.PlayerData.Datasets.FirstOrDefault();
            Assert.IsNotNull(playerDataset);

            var player = UnityEngine.Object.Instantiate(context.PlayerPrefab, context.Parent);
            _playerModel = new UnitModel(playerDataset);
            _playerInput = new InputSystem(context.PlayerInput);
            _playerController = new PlayerController(_playerInput, _playerModel, player);

            _spellsModel = new SpellsModel();
            _spellController = new SpellController(_playerInput, _spellsModel, context.SpellsData,
                context.Parent, _playerController);

            _playerController.Initialize();
            _spellController.Initialize();
            _spellsModel.Initialize(context.SpellsData.GetSpellParams());
            _playerInput.Initialize();
        }

        public void Dispose()
        {
            _playerController.Dispose();
            _spellController.Dispose();
            _spellsModel.Dispose();
            _playerInput.Dispose();
        }

        public void Tick()
        {
            _playerController.Tick();
            _spellController.Tick();
        }
    }
}