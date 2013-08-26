using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Brightness_Contrast
{
    public static class ChangeHue
    {
        public static WriteableBitmap ChangeHue1(this WriteableBitmap target, double amount)
        {
            IBufferExtensions.PixelBufferInfo pixels = IBufferExtensions.GetPixels(target.PixelBuffer);
            int index = 0;
            while (index < pixels.Bytes.Length)
            {
                if ((int)pixels.Bytes[index + 3] > 0)
                {
                    double num1 = (double)pixels.Bytes[index + 2];
                    double num2 = (double)pixels.Bytes[index + 1];
                    double num3 = (double)pixels.Bytes[index];
                    ColorRGB colorRGB;
                    colorRGB.R = (int)num1;
                    colorRGB.G = (int)num2;
                    colorRGB.B = (int)num3;
                    ColorHSL colorHSL = ColorExtensions.RGBToHSL(colorRGB);
                    colorHSL.H = (int)(amount * 1.0);
                    colorHSL.H %= (int)byte.MaxValue;
                    colorRGB = ColorExtensions.HSLToRGB(colorHSL);
                    pixels.Bytes[index] = (byte)colorRGB.B;
                    pixels.Bytes[index + 1] = (byte)colorRGB.G;
                    pixels.Bytes[index + 2] = (byte)colorRGB.R;
                }
                index += 4;
            }
            pixels.UpdateFromBytes();
            target.Invalidate();
            return target;
        }
    }
}
