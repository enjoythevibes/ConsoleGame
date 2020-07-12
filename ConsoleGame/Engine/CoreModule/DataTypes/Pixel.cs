using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine.CoreModule
{
    public struct Pixel
    {
        public char symbol;
        public Color32 symbolColor;
        public Color32 backgroundColor;

        public Pixel(char symbol, Color32 symbolColor, Color32 backgroundColor)
        {
            this.symbol = symbol;
            this.symbolColor = symbolColor;
            this.backgroundColor = backgroundColor;
        }    
    }
}
