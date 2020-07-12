using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine
{
    public static class Mathf
    {
        /// <summary>Значение между [0..1]. При x = x1 значение на выходе 0, при x = x2 значение на выходе 1.</summary>
        public static float Lerp01(float x1, float x2, float x)
        {
            var result = (x - x1) * ((1f)/(x2 - x1));
            if (result > 1f) result = 1f;
            else
            if (result < 0f) result = 0f;
            return result;
        }
    }
}
