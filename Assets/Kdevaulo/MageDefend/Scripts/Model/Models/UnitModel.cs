using System;

namespace Kdevaulo.MageDefend.Model
{
    public class UnitModel
    {
        public UnitStat Protection;
        public UnitStat MoveSpeed;
        public UnitStat Damage;
        public UnitStat Hp;

        public UnitModel(UnitParameters parameters)
        {
            Protection = new UnitStat(parameters.Protection);
            MoveSpeed = new UnitStat(parameters.MoveSpeed);
            Damage = new UnitStat(parameters.Damage);
            Hp = new UnitStat(parameters.Hp);
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