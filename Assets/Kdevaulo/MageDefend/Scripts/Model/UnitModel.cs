using System.Numerics;

namespace Kdevaulo.MageDefend.Model
{
    public class UnitModel
    {
        public string Id { get; private set; }
        public float Protection { get; }
        public float MoveSpeed { get; }
        public float Damage { get; }
        public float Hp { get; }

        public UnitModel(UnitConfig config)
        {
            Protection = config.Protection;
            MoveSpeed = config.MoveSpeed;
            Damage = config.Damage;
            Hp = config.Hp;
        }
    }
}