using System;

namespace Kdevaulo.MageDefend.Model
{
    public class UnitStat
    {
        public event Action Changed;
        public float Value => _value;

        private float _value;

        public UnitStat(float stat)
        {
            Set(stat);
        }

        public void Set(float value)
        {
            _value = value;
            Changed?.Invoke();
        }
    }
}