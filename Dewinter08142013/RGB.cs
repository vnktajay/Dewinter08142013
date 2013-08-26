using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightness_Contrast
{
    public class RGB
    {
        public byte Red { get; set; }

        public byte Green { get; set; }

        public byte Blue { get; set; }

        public RGB(byte r, byte g, byte b)
        {
            this.Red = r;
            this.Green = g;
            this.Blue = b;
        }

        public RGB(double r, double g, double b)
        {
            this.Red = (byte)r;
            this.Green = (byte)g;
            this.Blue = (byte)b;
        }
    }
}
