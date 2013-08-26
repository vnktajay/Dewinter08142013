using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightness_Contrast
{
     public class HSV
        {
            public double Hue { get; set; }

            public double Saturation { get; set; }

            public double Value { get; set; }

            public HSV(double h, double s, double v)
            {
                this.Hue = h;
                this.Saturation = s;
                this.Value = v;
            }

            public HSV(double[] hsv)
            {
                this.Hue = hsv[0];
                this.Saturation = hsv[1];
                this.Value = hsv[2];
            }

            public static HSV FromRGB(RGB rgb)
            {
                return HSV.FromRGB((double)rgb.Red, (double)rgb.Green, (double)rgb.Blue);
            }

            public static HSV FromRGB(double r, double g, double b)
            {
                r /= (double)byte.MaxValue;
                g /= (double)byte.MaxValue;
                b /= (double)byte.MaxValue;
                double num1 = Math.Max(Math.Max(r, g), b);
                double num2 = Math.Min(Math.Min(r, g), b);
                double num3 = num1 - num2;
                double h = 0.0;
                double s = 0.0;
                if (num3 == 0.0)
                {
                    h = 0.0;
                }
                else
                {
                    if (num1 == r)
                        h = 60.0 * (g - b) / num3 + 0.0;
                    else if (num1 == g)
                        h = 60.0 * (b - r) / num3 + 120.0;
                    else if (num1 == b)
                        h = 60.0 * (r - g) / num3 + 240.0;
                    if (h < 0.0)
                        h += 360.0;
                }
                if (num1 > 0.0)
                    s = num3 / num1;
                double v = num1;
                return new HSV(h, s, v);
            }

            public RGB ToRGB()
            {
                if (this.Saturation == 0.0)
                    return new RGB(this.Value * (double)byte.MaxValue, this.Value * (double)byte.MaxValue, this.Value * (double)byte.MaxValue);
                double num1 = 0.0;
                double num2 = 0.0;
                double num3 = 0.0;
                double num4 = this.Hue % 360.0;
                double num5 = Math.Min(1.0, Math.Max(0.0, this.Saturation));
                double num6 = Math.Min(1.0, Math.Max(0.0, this.Value));
                int num7 = (int)(num4 / 60.0);
                double num8 = num4 / 60.0 - (double)num7;
                double num9 = num6 * (1.0 - num5);
                double num10 = num6 * (1.0 - num8 * num5);
                double num11 = num6 * (1.0 - (1.0 - num8) * num5);
                if (num7 == 0)
                {
                    num1 = num6;
                    num2 = num11;
                    num3 = num9;
                }
                else if (num7 == 1)
                {
                    num1 = num10;
                    num2 = num6;
                    num3 = num9;
                }
                else if (num7 == 2)
                {
                    num1 = num9;
                    num2 = num6;
                    num3 = num11;
                }
                else if (num7 == 3)
                {
                    num1 = num9;
                    num2 = num10;
                    num3 = num6;
                }
                else if (num7 == 4)
                {
                    num1 = num11;
                    num2 = num9;
                    num3 = num6;
                }
                else if (num7 == 5)
                {
                    num1 = num6;
                    num2 = num9;
                    num3 = num10;
                }
                return new RGB(num1 * (double)byte.MaxValue, num2 * (double)byte.MaxValue, num3 * (double)byte.MaxValue);
            }
        }
    }

