using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleGame.Engine.CoreModule.Debug.DebugConsole;

namespace ConsoleGame.Engine
{
    public static class Debug
    {
        public static void Log(string msg) => CoreModule.Debug.DebugConsole.WriteMessage(msg, LogType.Log);
        public static void Error(string msg) => CoreModule.Debug.DebugConsole.WriteMessage(msg, LogType.Error);
    }
}
