using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine.CoreModule
{
    public static class InputCore
    {   
        private static HashSet<InputManager.KeyCode> keysPressed = new HashSet<InputManager.KeyCode>();
        public static HashSet<InputManager.KeyCode> keysPressedCache = new HashSet<InputManager.KeyCode>();

        public static void Press(InputManager.KeyCode keyCode)
        {
            if (!keysPressedCache.Contains(keyCode))
            {
                keysPressed.Add(keyCode);
                keysPressedCache.Add(keyCode);
            }
        }

        public static bool GetKey(InputManager.KeyCode keyCode)
        {
            return keysPressed.Contains(keyCode);
        }

        public static void Reset()
        {
            keysPressed.Clear();
        }

        public static void ClearCache()
        {
            keysPressedCache.Clear();
        }
    }
}

namespace ConsoleGame.Engine
{
    public static class Input
    {
        public static bool GetKey(InputManager.KeyCode keyCode) => CoreModule.InputCore.GetKey(keyCode);
    }
}
