using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine
{
    public static class InputManager
    {
        public enum KeyCode
        {
            Right,
            Left,
            Up,
            Down
        }

        public static Dictionary<ConsoleKey, KeyCode> buttons = new Dictionary<ConsoleKey, KeyCode>
        {
            { ConsoleKey.RightArrow, KeyCode.Right },
            { ConsoleKey.LeftArrow , KeyCode.Left  },
            { ConsoleKey.UpArrow   , KeyCode.Up    },
            { ConsoleKey.DownArrow , KeyCode.Down  }
        };        
    }   
}
