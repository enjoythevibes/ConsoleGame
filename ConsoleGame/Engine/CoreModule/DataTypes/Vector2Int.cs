using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine
{
    public struct Vector2Int
    {
        public int x;
        public int y;

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static implicit operator Vector2(Vector2Int obj)
        {
            return new Vector2((float)obj.x, (float)obj.y);
        }

        public static Vector2Int operator+(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x + b.x, a.y + b.y);
        }

        public static Vector2Int operator-(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x - b.x, a.y - b.y);
        }

        public static int Distance(Vector2Int a, Vector2Int b)
        {
            var directionVector = a - b;
            return (int)Math.Abs(Math.Sqrt((directionVector.x * directionVector.x) + (directionVector.y * directionVector.y)));
        }
    }
}
