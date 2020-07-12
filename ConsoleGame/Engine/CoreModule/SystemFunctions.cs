using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine.CoreModule
{
    public static class SystemFunctions
    {
        #region Disable Quick Edit And Resize
        const uint ENABLE_QUICK_EDIT = 0x0040;
        const int STD_INPUT_HANDLE = -10;
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        private const int MF_BYCOMMAND = 0x00000000;
        private const int SC_CLOSE = 0xF060;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_SIZE = 0xF000;
        [DllImport("user32.dll")]
        private static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();        

        public static void DisableQuickEdit()
        {
            var consoleHandle = GetStdHandle(STD_INPUT_HANDLE);
            var consoleMode = default(uint);
            if (!GetConsoleMode(consoleHandle, out consoleMode))
            {
                return;
            }
            consoleMode &= ~ENABLE_QUICK_EDIT;
            if (!SetConsoleMode(consoleHandle, consoleMode))
            {
                return;
            }
            var handle = GetConsoleWindow();
            var sysMenu = GetSystemMenu(handle, false);
            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
            handle = GetStdHandle(-11);
            var mode = default(uint);
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);
        }
        #endregion
    }
}