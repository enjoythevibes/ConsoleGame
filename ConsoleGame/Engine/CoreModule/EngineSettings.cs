using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine.CoreModule
{
    public static class EngineSettings
    {
        public static int TARGET_FPS = 30;
        public static long OPTIMAL_UPDATE_TIME { get => 1000000000 / TARGET_FPS; }
        public static readonly Vector2Int RENDER_AREA_SIZE = new Vector2Int(51, 21);

        public static int MAX_DEBUG_CONSOLE_ROWS = 10;
    }
}
