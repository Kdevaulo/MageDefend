using System;

namespace Kdevaulo.MageDefend.Model
{
    public class UnitModel
    {
        public event Action Died;

        public readonly UnitStat Protection;
        public readonly UnitStat MoveSpeed;
        public readonly UnitStat Damage;
        public readonly UnitStat Hp;

        public UnitModel(UnitParameters parameters)
        {
            Protection = new UnitStat(parameters.Protection);
            MoveSpeed = new UnitStat(parameters.MoveSpeed);
            Damage = new UnitStat(parameters.Damage);
            Hp = new UnitStat(parameters.Hp);
        }

        public void Hit(float value)
        {
            var subtrahend = Math.Abs(value);
            var targetValue = Hp.Value - subtrahend * Protection.Value;

            Hp.Set(targetValue);

            if (Hp.Value <= 0)
            {
                Died?.Invoke();
            }
        }
    }
}