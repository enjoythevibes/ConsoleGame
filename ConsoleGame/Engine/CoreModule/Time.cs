using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine
{
    public static class Time
    {
        public static float deltaTime { get => CoreModule.GameLoop.deltaTime; }
    }
}
