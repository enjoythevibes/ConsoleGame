using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine.CoreModule.Debug
{
    public static class DebugConsole
    {
        private class DebugMessage
        {
            public string debugText;
            public ConsoleColor color;

            public DebugMessage(string debugText, ConsoleColor color)
            {
                this.debugText = debugText;
                this.color = color;
            }
        }

        public enum LogType
        {
            Log = ConsoleColor.White,
            Error = ConsoleColor.Cyan
        }

        [DllImport( "kernel32.dll", SetLastError = true )]
        public static extern bool SetConsoleMode( IntPtr hConsoleHandle, int mode );
        [DllImport( "kernel32.dll", SetLastError = true )]
        public static extern bool GetConsoleMode( IntPtr handle, out int mode );

        [DllImport( "kernel32.dll", SetLastError = true )]
        public static extern IntPtr GetStdHandle( int handle );

        private static List<DebugMessage> debugRows;
        private static string LogFormat = " \x1b[38;5;230m    Debug Log: {0}";

        static DebugConsole()
        {
            debugRows = new List<DebugMessage>();
            var handle = GetStdHandle( -11 );
            int mode;
            GetConsoleMode( handle, out mode );
            SetConsoleMode( handle, mode | 0x4 );
        }

        public static void WriteMessage(string msg, LogType logType)
        {
            debugRows.Add(new DebugMessage(msg, (ConsoleColor)logType));
            if (debugRows.Count > EngineSettings.MAX_DEBUG_CONSOLE_ROWS)
                debugRows.RemoveAt(0);
        }        

        public static void RenderDebugConsole()
        {
            for (int i = 0; i < debugRows.Count; i++)
            {
                System.Console.ForegroundColor = debugRows[i].color;
                System.Console.WriteLine(string.Format(LogFormat, debugRows[i].debugText));
                System.Console.ResetColor();
            }
        }
    }
}
