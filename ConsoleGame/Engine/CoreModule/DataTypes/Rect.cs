using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine
{
    public struct Rect
    {
        public int x;
        public int y;
        public int width;
        public int height;

        public Rect(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public static bool DoesOverlapping(Rect first, Rect second)
        {
            var thisHalfWidth = first.width / 2f;
            var thisHalfHeight = first.height / 2f;
            var thisCenterPositionX = thisHalfWidth + first.x;
            var thisCenterPositionY = thisHalfHeight + first.y;
            
            var otherHalfWidth = second.width / 2f;
            var otherHalfHeight = second.height / 2f;
            var otherCenterPositionX = otherHalfWidth + second.x;
            var otherCenterPositionY = otherHalfHeight + second.y;

            var sumX = thisHalfWidth + otherHalfWidth;
            var sumY = thisHalfHeight + otherHalfHeight;

            var dX = Math.Abs(otherCenterPositionX - thisCenterPositionX);
            var dY = Math.Abs(otherCenterPositionY - thisCenterPositionY);

            return dX <= sumX && dY <= sumY;
        }

        public bool CheckIfPointInside(Vector2Int pointPosition)
        {
            var thisHalfWidth = width / 2f;
            var thisHalfHeight = height / 2f;
            var thisCenterPositionX = thisHalfWidth + x;
            var thisCenterPositionY = thisHalfHeight + y;

            var dX = Math.Abs(pointPosition.x - thisCenterPositionX);
            var dY = Math.Abs(pointPosition.y - thisCenterPositionY);

            return dX <= thisHalfWidth && dY <= thisHalfHeight;
        }    

        public override string ToString()
        {
            return $"x:{x}, y:{y}, width:{width}, height:{height}";
        }  
    }
}
