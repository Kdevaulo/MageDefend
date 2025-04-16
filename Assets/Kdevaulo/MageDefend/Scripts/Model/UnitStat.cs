namespace Kdevaulo.MageDefend.Model
{
    public class UnitStat
    {
        public float Value => _value;

        private float _value;

        public UnitStat(float stat)
        {
            Set(stat);
        }

        public void Set(float value)
        {
            if (value < 0)
            {
                value = 0;
            }

            _value = value;
        }
    }
}