using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace Brightness_Contrast
{
    class Bitmap_Sharpen
    {
        public static int[] sharpen(int[] inputPixels, int width, int height, double strength)
        {


            int filterWidth = 3;
            int filterHeight = 3;
            int w = width;
            int h = height;

            double[,] filter = new double[filterWidth, filterHeight];

            filter[0, 0] = filter[0, 1] = filter[0, 2] = filter[1, 0] = filter[1, 2] = filter[2, 0] = filter[2, 1] = filter[2, 2] = -1;
            filter[1, 1] = 9;

            double factor = strength / 16;
            double bias = 1.0 - strength;
            int count = 0;

            int[,] twodar = new int[h, w];
            for (int x = 0; x < h; x++)
            {
                for (int y =0; y < w; y++)
                {
                    twodar[x,y] = inputPixels[count];
                    count++;
                }
            }



            Color[,] result = new Color[width, height];

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    double ar = 0.0, red = 0.0, green = 0.0, blue = 0.0;

                    for (int filterX = 0; filterX < filterWidth; filterX++)
                    {
                        for (int filterY = 0; filterY < filterHeight; filterY++)
                        {
                            int imageX = (x - filterWidth / 2 + filterX + w) % w;
                            int imageY = (y - filterHeight / 2 + filterY + h) % h;

                            var c = twodar[imageY, imageX];
                            var a = (byte)(c >> 32);
                            var r = (byte)(c >> 24);
                            var g = (byte)(c >> 16);
                            var b = (byte)(c >> 8);


                            ar = a * filter[filterX, filterY];
                            red += r * filter[filterX, filterY];
                            green += g * filter[filterX, filterY];
                            blue += b * filter[filterX, filterY];
                        }
                        int arr = Math.Min(Math.Max((int)(factor * ar + bias), 0), 255);
                        int rr = Math.Min(Math.Max((int)(factor * red + bias), 0), 255);
                        int gg = Math.Min(Math.Max((int)(factor * green + bias), 0), 255);
                        int bb = Math.Min(Math.Max((int)(factor * blue + bias), 0), 255);

                        result[x, y] = Color.FromArgb((byte)arr, (byte)rr, (byte)gg, (byte)bb);
                    }
                }
            }
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    twodar[i, j] = Convert.ToInt32((result[j, i].A + result[j, i].R + result[j, i].G + result[j, i].B) / 4);

                }
            }

            count = 0;
            for (int x = 0; x < h; x++)
            {
                for (int y = 0; y < w; y++)
                {
                    inputPixels[count] = twodar[x, y];
                    count++;
                }
            }




            return inputPixels;
        }
  
       
    }

 

    }


