using System;

namespace Kdevaulo.MageDefend.Model
{
    [Serializable]
    public class Spell
    {
        public float MoveSpeed;
        public float Lifetime;
        public float Cooldown;
        public float Damage;
        public bool IsLocked;
    }
}