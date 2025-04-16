using System;

namespace Kdevaulo.MageDefend.Model
{
    [Serializable]
    public class SpellParameters
    {
        public string Id;
        public float MoveSpeed;
        public float Lifetime;
        public float Cooldown;
        public float Damage;
    }
}