namespace Kdevaulo.MageDefend.Presentation
{
    public class GameplayFlow
    {
        private const int MaxEnemiesCount = 10;

        private readonly LocationController _locationController;
        private readonly PlayerController _playerController;

        private readonly InputSystem _playerInput;

        public GameplayFlow(GameContext context)
        {
            _locationController = new LocationController(context);

            _playerInput = new InputSystem(context.PlayerInput);
            _playerController = new PlayerController(context, _playerInput, _locationController);
        }

        public void Initialize()
        {
            _locationController.Initialize(MaxEnemiesCount);
            _playerController.Initialize();
            _playerInput.Initialize();
        }

        public void Dispose()
        {
            _locationController.Dispose();
            _playerController.Dispose();
            _playerInput.Dispose();
        }

        public void Tick()
        {
            _locationController.Tick();
            _playerController.Tick();
        }
    }
}