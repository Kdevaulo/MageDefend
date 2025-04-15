using System;
using System.Numerics;

namespace Kdevaulo.MageDefend.Model
{
    public class SpellModel
    {
        public Vector3 MoveDirection { get; private set; }
        public float MoveSpeed { get; }
        public float Cooldown { get; }
        public float Lifetime { get; private set; }
        public float Damage { get; }
        public bool IsFinished => Lifetime <= 0;
        public bool IsLocked { get; private set; }

        public SpellModel(Spell parameters)
        {
            MoveSpeed = parameters.MoveSpeed;
            Lifetime = parameters.Lifetime;
            Cooldown = parameters.Cooldown;
            IsLocked = parameters.IsLocked;
            Damage = parameters.Damage;
        }

        public void SetDirection(Vector3 direction)
        {
            MoveDirection = direction;
        }

        public void DecreaseLifetime(float value)
        {
            var subtrahend = Math.Abs(value);
            Lifetime -= subtrahend;

            if (Lifetime < 0)
            {
                Lifetime = 0;
            }
        }
    }
}