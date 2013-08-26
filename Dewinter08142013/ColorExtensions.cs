using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightness_Contrast
{
    public static class ColorExtensions
    {
        public static ColorHSL RGBToHSL(ColorRGB colorRGB)
        {
            double val1_1 = (double)colorRGB.R / 256.0;
            double val1_2 = (double)colorRGB.G / 256.0;
            double val2 = (double)colorRGB.B / 256.0;
            double num1 = Math.Max(val1_1, Math.Max(val1_2, val2));
            double num2 = Math.Min(val1_1, Math.Min(val1_2, val2));
            double num3;
            double num4;
            double num5;
            if (num2 == num1)
            {
                num3 = 0.0;
                num4 = 0.0;
                num5 = val1_1;
            }
            else
            {
                num5 = (num2 + num1) / 2.0;
                num4 = num5 >= 0.5 ? (num1 - num2) / (2.0 - num1 - num2) : (num1 - num2) / (num1 + num2);
                num3 = (val1_1 != num1 ? (val1_2 != num1 ? 4.0 + (val1_1 - val1_2) / (num1 - num2) : 2.0 + (val2 - val1_1) / (num1 - num2)) : (val1_2 - val2) / (num1 - num2)) / 6.0;
                if (num3 < 0.0)
                    ++num3;
            }
            ColorHSL colorHsl;
            colorHsl.H = (int)(num3 * (double)byte.MaxValue);
            colorHsl.S = (int)(num4 * (double)byte.MaxValue);
            colorHsl.L = (int)(num5 * (double)byte.MaxValue);
            return colorHsl;
        }

        public static ColorRGB HSLToRGB(ColorHSL colorHSL)
        {
            double num1 = (double)colorHSL.H / 256.0;
            double num2 = (double)colorHSL.S / 256.0;
            double num3 = (double)colorHSL.L / 256.0;
            double num4;
            double num5;
            double num6;
            if (num2 == 0.0)
            {
                double num7;
                num4 = num7 = num3;
                num5 = num7;
                num6 = num7;
            }
            else
            {
                double num7 = num3 >= 0.5 ? num3 + num2 - num3 * num2 : num3 * (1.0 + num2);
                double num8 = 2.0 * num3 - num7;
                double num9 = num1 + 1.0 / 3.0;
                if (num9 > 1.0)
                    --num9;
                double num10 = num1;
                double num11 = num1 - 1.0 / 3.0;
                if (num11 < 0.0)
                    ++num11;
                num6 = num9 >= 1.0 / 6.0 ? (num9 >= 0.5 ? (num9 >= 2.0 / 3.0 ? num8 : num8 + (num7 - num8) * (2.0 / 3.0 - num9) * 6.0) : num7) : num8 + (num7 - num8) * 6.0 * num9;
                num5 = num10 >= 1.0 / 6.0 ? (num10 >= 0.5 ? (num10 >= 2.0 / 3.0 ? num8 : num8 + (num7 - num8) * (2.0 / 3.0 - num10) * 6.0) : num7) : num8 + (num7 - num8) * 6.0 * num10;
                num4 = num11 >= 1.0 / 6.0 ? (num11 >= 0.5 ? (num11 >= 2.0 / 3.0 ? num8 : num8 + (num7 - num8) * (2.0 / 3.0 - num11) * 6.0) : num7) : num8 + (num7 - num8) * 6.0 * num11;
            }
            ColorRGB colorRgb;
            colorRgb.R = (int)(num6 * (double)byte.MaxValue);
            colorRgb.G = (int)(num5 * (double)byte.MaxValue);
            colorRgb.B = (int)(num4 * (double)byte.MaxValue);
            return colorRgb;
        }
        public static int IntColorFromBytes(byte a, byte r, byte g, byte b)
        {
            return (int)a << 24 | (int)r << 16 | (int)g << 8 | (int)b;
        }

        public static int[] ToPixels(this byte[] bytes)
        {
            int[] numArray = new int[bytes.Length >> 2];
            int index1 = 0;
            int index2 = 0;
            while (index2 < bytes.Length)
            {
                numArray[index1] = ColorExtensions.IntColorFromBytes(bytes[index2 + 3], bytes[index2 + 2], bytes[index2 + 1], bytes[index2]);
                ++index1;
                index2 += 4;
            }
            return numArray;
        }

        public static ColorHSV RGBToHSV(ColorRGB colorRGB)
        {
            double val1_1 = (double)colorRGB.R / 256.0;
            double val1_2 = (double)colorRGB.G / 256.0;
            double val2 = (double)colorRGB.B / 256.0;
            double num1 = Math.Max(val1_1, Math.Max(val1_2, val2));
            double num2 = Math.Min(val1_1, Math.Min(val1_2, val2));
            double num3 = num1;
            double num4 = num1 != 0.0 ? (num1 - num2) / num1 : 0.0;
            double num5;
            if (num4 == 0.0)
            {
                num5 = 0.0;
            }
            else
            {
                num5 = (val1_1 != num1 ? (val1_2 != num1 ? 4.0 + (val1_1 - val1_2) / (num1 - num2) : 2.0 + (val2 - val1_1) / (num1 - num2)) : (val1_2 - val2) / (num1 - num2)) / 6.0;
                if (num5 < 0.0)
                    ++num5;
            }
            ColorHSV colorHsv;
            colorHsv.H = (int)(num5 * (double)byte.MaxValue);
            colorHsv.S = (int)(num4 * (double)byte.MaxValue);
            colorHsv.V = (int)(num3 * (double)byte.MaxValue);
            return colorHsv;
        }

        public static ColorRGB HSVToRGB(ColorHSV colorHSV)
        {
            double num1 = (double)colorHSV.H / 256.0;
            double num2 = (double)colorHSV.S / 256.0;
            double num3 = (double)colorHSV.V / 256.0;
            double num4;
            double num5;
            double num6;
            if (num2 == 0.0)
            {
                double num7;
                num4 = num7 = num3;
                num5 = num7;
                num6 = num7;
            }
            else
            {
                double d = num1 * 6.0;
                int num7 = (int)Math.Floor(d);
                double num8 = d - (double)num7;
                double num9 = num3 * (1.0 - num2);
                double num10 = num3 * (1.0 - num2 * num8);
                double num11 = num3 * (1.0 - num2 * (1.0 - num8));
                switch (num7)
                {
                    case 0:
                        num6 = num3;
                        num5 = num11;
                        num4 = num9;
                        break;
                    case 1:
                        num6 = num10;
                        num5 = num3;
                        num4 = num9;
                        break;
                    case 2:
                        num6 = num9;
                        num5 = num3;
                        num4 = num11;
                        break;
                    case 3:
                        num6 = num9;
                        num5 = num10;
                        num4 = num3;
                        break;
                    case 4:
                        num6 = num11;
                        num5 = num9;
                        num4 = num3;
                        break;
                    case 5:
                        num6 = num3;
                        num5 = num9;
                        num4 = num10;
                        break;
                    default:
                        double num12;
                        num4 = num12 = num3;
                        num5 = num12;
                        num6 = num12;
                        break;
                }
            }
            ColorRGB colorRgb;
            colorRgb.R = (int)(num6 * (double)byte.MaxValue);
            colorRgb.G = (int)(num5 * (double)byte.MaxValue);
            colorRgb.B = (int)(num4 * (double)byte.MaxValue);
            return colorRgb;
        }
    }
}
