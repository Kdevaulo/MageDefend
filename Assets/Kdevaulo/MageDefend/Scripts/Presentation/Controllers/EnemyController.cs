using Kdevaulo.MageDefend.Model;

namespace Kdevaulo.MageDefend.Presentation
{
    public class EnemyController
    {
        private readonly UnitModel _enemyModel;
        private readonly EnemyView _enemyView;

        public EnemyController(UnitModel enemyModel, EnemyView enemyView)
        {
            _enemyModel = enemyModel;
            _enemyView = enemyView;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        public void Tick()
        {
            
        }
    }
}