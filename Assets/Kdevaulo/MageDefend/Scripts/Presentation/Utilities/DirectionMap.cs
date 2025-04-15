using System.Collections.Generic;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public static class DirectionMap
    {
        private static readonly Dictionary<Vector2Int, Direction> Directions = new Dictionary<Vector2Int, Direction>
        {
            { Vector2Int.zero, Direction.None },

            { Vector2Int.down, Direction.Backward },
            { Vector2Int.right, Direction.Right },
            { Vector2Int.up, Direction.Forward },
            { Vector2Int.left, Direction.Left },

            { new Vector2Int(-1, -1), Direction.BackwardLeft },
            { new Vector2Int(1, -1), Direction.BackwardRight },
            { new Vector2Int(-1, 1), Direction.ForwardLeft },
            { Vector2Int.one, Direction.ForwardRight }
        };

        private static readonly Dictionary<Direction, int> Rotations = new Dictionary<Direction, int>
        {
            { Direction.None, 0 },

            { Direction.Backward, 180 },
            { Direction.Forward, 0 },
            { Direction.Right, 90 },
            { Direction.Left, -90 },

            { Direction.BackwardLeft, -135 },
            { Direction.BackwardRight, 135 },
            { Direction.ForwardLeft, -45 },
            { Direction.ForwardRight, 45 }
        };

        public static int RoundClamp(float value, float epsilon)
        {
            if (Mathf.Abs(value) < epsilon)
                return 0;

            return value > 0 ? 1 : -1;
        }

        public static Direction GetDirection(Vector2Int vector)
        {
            if (!Directions.TryGetValue(vector, out var direction))
            {
                Debug.LogError("There is no correct direction defined in DirectionMap");
            }

            return direction;
        }

        public static Quaternion GetRotation(Direction direction)
        {
            if (!Rotations.TryGetValue(direction, out var angle))
            {
                Debug.LogError("There is no correct rotation defined in DirectionMap");
            }

            return Quaternion.Euler(0, angle, 0);
        }
    }
}