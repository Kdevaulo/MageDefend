using System;

namespace Kdevaulo.MageDefend.Model
{
    public class UnitModel
    {
        public string Id { get; private set; }
        public UnitStat Protection;
        public UnitStat MoveSpeed;
        public UnitStat Damage;
        public UnitStat Hp;

        public UnitModel(UnitConfig config)
        {
            Protection = new UnitStat(config.Protection);
            MoveSpeed = new UnitStat(config.MoveSpeed);
            Damage = new UnitStat(config.Damage);
            Hp = new UnitStat(config.Hp);
        }

        public void TakeDamage(float value)
        {
            var subtrahend = Math.Abs(value);
            var targetValue = Hp.Value - subtrahend * Protection.Value;

            if (targetValue < 0)
            {
                targetValue = 0;
            }

            Hp.Set(targetValue);
        }
    }
}