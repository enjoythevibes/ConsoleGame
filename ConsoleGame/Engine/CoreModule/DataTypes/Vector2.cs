using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine
{
    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static explicit operator Vector2Int(Vector2 obj)
        {
            return new Vector2Int((int)Math.Floor(obj.x), (int)Math.Floor(obj.y));
        }

        public static Vector2 operator+(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator-(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            var directionVector = a - b;
            return (float)Math.Abs(Math.Sqrt((directionVector.x * directionVector.x) + (directionVector.y * directionVector.y)));
        }
    }
}
