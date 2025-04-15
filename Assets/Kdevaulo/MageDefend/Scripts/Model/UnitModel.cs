using System.Numerics;

namespace Kdevaulo.MageDefend.Model
{
    public class UnitModel
    {
        public Vector3 Position { get; private set; }
        public float Protection { get; }
        public float MoveSpeed { get; }
        public float Damage { get; }
        public float Hp { get; }

        public UnitModel(UnitDataset dataset)
        {
            Protection = dataset.Protection;
            MoveSpeed = dataset.MoveSpeed;
            Position = dataset.Position;
            Damage = dataset.Damage;
            Hp = dataset.Hp;
        }

        public void Move(Vector3 vector)
        {
            Position += vector * MoveSpeed;
        }
    }
}