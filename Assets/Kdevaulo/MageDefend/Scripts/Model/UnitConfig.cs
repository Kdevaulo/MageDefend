using System;
using System.Numerics;

namespace Kdevaulo.MageDefend.Model
{
    [Serializable]
    public class UnitConfig
    {
        public float Hp;
        public float Protection;
        public float MoveSpeed;
        public float Damage;
        public Vector3 Position;
    }
}