using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightness_Contrast
{
    class Saturation
    {
      
public byte[] satura( byte[] source, int width, int height,double saturate)
    {
      int num1 = width * height;
      byte[] numArray = new byte[source.Length];
      for (int index1 = 0; index1 < num1; ++index1)
      {
        int index2 = index1 * 4;
        double b = (double) source[index2];
        double g = (double) source[index2 + 1];
        double r = (double) source[index2 + 2];
        int num2 = (int) source[index2 + 3];
        HSV hsv = HSV.FromRGB(r, g, b);
        
        hsv.Saturation *= saturate/22;
        RGB rgb = hsv.ToRGB();
        int val2_1 = (int) rgb.Blue;
        int val2_2 = (int) rgb.Green;
        int val2_3 = (int) rgb.Red;
        numArray[index2] = (byte) Math.Min((int) byte.MaxValue, Math.Max(0, val2_1));
        numArray[index2 + 1] = (byte) Math.Min((int) byte.MaxValue, Math.Max(0, val2_2));
        numArray[index2 + 2] = (byte) Math.Min((int) byte.MaxValue, Math.Max(0, val2_3));
        numArray[index2 + 3] = source[index2 + 3];
      }
      return numArray;
    }
    }
}
