using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine
{
    public struct Color32
    {
        public byte r;
        public byte g;
        public byte b;

        public Color32(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public CoreModule.float3 LightIntensity
        {
            get
            {
                return new CoreModule.float3(r/255f, g/255f, b/255f);
            }
        }

        public static Color32 operator*(Color32 color, float value)
        {
            var r = (byte)((float)color.r * value);
            var g = (byte)((float)color.g * value);
            var b = (byte)((float)color.b * value);
            return new Color32(r, g, b);
        }

        public static Color32 operator*(Color32 first, CoreModule.float3 value)
        {
            var r = first.r * value.x;
            var g = first.g * value.y;
            var b = first.b * value.z;
            return new Color32((byte)r, (byte)g, (byte)b);                
        }

        public static Color32 operator+(Color32 first, Color32 second)
        {
            var r = first.r + second.r;
            if (r > 255) r = 255;

            var g = first.g + second.g;
            if (g > 255) g = 255;

            var b = first.b + second.b;
            if (b > 255) b = 255;

            return new Color32((byte)r, (byte)g, (byte)b);
        }

        public static Color32 operator-(Color32 first, Color32 second)
        {
            var r = first.r - second.r;
            if (r < 0) r = 0;

            var g = first.g - second.g;
            if (g < 0) g = 0;

            var b = first.b - second.b;
            if (b < 0) b = 0;

            return new Color32((byte)r, (byte)g, (byte)b);
        }

        public static bool operator<(Color32 first, Color32 second)
        {
            return first.r < second.r && first.g < second.g && first.b < second.b;
        }   

        public static bool operator>(Color32 first, Color32 second)
        {
            return first.r > second.r && first.g > second.g && first.b > second.b;
        }       
    }
}
