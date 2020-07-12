using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine
{
    public static class LightingSettings
    {
        public static Color32 ambientColor = new Color32(10, 10, 10);
        public const int lightFalloff = 3;
    }
}
