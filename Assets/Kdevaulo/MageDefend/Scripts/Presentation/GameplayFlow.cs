namespace Kdevaulo.MageDefend.Presentation
{
    public class GameplayFlow
    {
        private readonly LocationController _locationController;

        public GameplayFlow(GameContext context)
        {
            _locationController = new LocationController(context);
        }

        public void Initialize()
        {
            _locationController.Initialize();
        }

        public void Dispose()
        {
            _locationController.Dispose();
        }

        public void Tick()
        {
            _locationController.Tick();
        }
    }
}