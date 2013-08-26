using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace Brightness_Contrast
{
    class Gamma
    {
        
        
        public byte[] GammaChange(byte[] _dstPixels, int width, int height,double gamm)
        
        {
            double gamma = gamm / 4;
            byte[] resultPixels={1};
            double BlueColorValue = gamma*4;
            double GreenColorValue = gamma+4;
            double RedColorValue = gamma-4;
            

            byte[] array1 = this.Gamma_GetArray(BlueColorValue / 10.0);
            byte[] array2 = this.Gamma_GetArray(GreenColorValue / 10.0);
            byte[] array3 = this.Gamma_GetArray(RedColorValue / 10.0);
            int CurrentByte = 0;
            while (CurrentByte < 4 * height * width)
            {
                resultPixels = this.Gamma_SetNewBGRValues(_dstPixels, CurrentByte, array1, array2, array3);
                CurrentByte += 4;
            }
            return resultPixels;
        }

        private byte[] Gamma_SetNewBGRValues(byte[] dstPixels,int CurrentByte, byte[] BlueGamma, byte[] GreenGamma, byte[] RedGamma)
        {
            dstPixels[CurrentByte] = BlueGamma[(int)dstPixels[CurrentByte]];
            dstPixels[CurrentByte + 1] = GreenGamma[(int)dstPixels[CurrentByte + 1]];
            dstPixels[CurrentByte + 2] = RedGamma[(int)dstPixels[CurrentByte + 2]];
            return dstPixels;
        }

        private byte[] Gamma_GetArray(double color)
        {
            byte[] numArray = new byte[256];
            for (int index = 0; index < 256; ++index)
                numArray[index] = (byte)Math.Min((int)byte.MaxValue, (int)((double)byte.MaxValue * Math.Pow((double)index / (double)byte.MaxValue, 1.0 / color) + 0.5));
            return numArray;
        }
    }
}
