using Kdevaulo.MageDefend.Model;

namespace Kdevaulo.MageDefend.Presentation
{
    public class Enemy
    {
        public EnemyController EnemyController;
        public UnitModel EnemyModel;
        public EnemyView EnemyView;

        public Enemy(UnitModel model, EnemyView view, EnemyController controller)
        {
            EnemyController = controller;
            EnemyModel = model;
            EnemyView = view;
        }
    }
}