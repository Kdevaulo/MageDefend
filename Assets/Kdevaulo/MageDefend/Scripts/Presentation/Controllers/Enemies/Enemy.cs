using Kdevaulo.MageDefend.Model;

namespace Kdevaulo.MageDefend.Presentation
{
    public class Enemy
    {
        public readonly EnemyController EnemyController;
        public readonly UnitModel EnemyModel;
        public readonly EnemyView EnemyView;

        public Enemy(UnitModel model, EnemyView view, EnemyController controller)
        {
            EnemyController = controller;
            EnemyModel = model;
            EnemyView = view;
        }
    }
}