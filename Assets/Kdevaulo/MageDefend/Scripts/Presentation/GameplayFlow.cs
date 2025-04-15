using System.Linq;

using Kdevaulo.MageDefend.Model;

using UnityEngine.Assertions;

namespace Kdevaulo.MageDefend.Presentation
{
    public class GameplayFlow
    {
        private const int MaxEnemiesCount = 10;

        private PlayerController _playerController;
        private SpellController _spellController;

        private EnemiesController _enemiesController;
        private InputSystem _playerInput;

        private SpellsModel _spellsModel;
        private UnitModel _playerModel;

        public void Initialize(GameContext context)
        {
            var playerDataset = context.PlayerData.Datasets.FirstOrDefault();
            Assert.IsNotNull(playerDataset);

            var player = UnityEngine.Object.Instantiate(context.PlayerPrefab, context.Parent);
            _playerModel = new UnitModel(playerDataset);
            _playerInput = new InputSystem(context.PlayerInput);
            _playerController = new PlayerController(_playerInput, _playerModel, player);

            _spellsModel = new SpellsModel(context.SpellsData.GetSpellParams());
            _spellController = new SpellController(_playerInput, _spellsModel, context.SpellsData,
                context.Parent, _playerController);

            _enemiesController = new EnemiesController(context.EnemiesData, context.EnemiesVisualData);

            _playerController.Initialize();
            _spellController.Initialize();
            _enemiesController.Initialize(MaxEnemiesCount);
            _playerInput.Initialize();
        }

        public void Dispose()
        {
            _enemiesController.Dispose();
            _playerController.Dispose();
            _spellController.Dispose();
            _playerInput.Dispose();
        }

        public void Tick()
        {
            _enemiesController.Tick();
            _playerController.Tick();
            _spellController.Tick();
        }
    }
}