using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public static class VectorUtilities
    {
        public static System.Numerics.Vector3 ToNumerics(this Vector3 vector)
        {
            return new System.Numerics.Vector3(vector.x, vector.y, vector.z);
        }

        public static System.Numerics.Vector3 ToNumerics(this Vector3Int vector)
        {
            return new System.Numerics.Vector3(vector.x, vector.y, vector.z);
        }

        public static Vector3 ToUnity(this System.Numerics.Vector3 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }
    }
}