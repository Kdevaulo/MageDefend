using System;
using System.Numerics;

namespace Kdevaulo.MageDefend.Model
{
    [Serializable]
    public class UnitConfig
    {
        public Vector3 Position;
        public string Id;
        public float Hp;
        public float Protection;
        public float MoveSpeed;
        public float Damage;
    }
}