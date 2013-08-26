using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brightness_Contrast
{
    class Brightness_and_Contrast
    {
        /// <summary>
        /// The brightness factor.
        /// Should be in the range [-1, 1].
        /// </summary>
        public float BrightnessFactor { get; set; }

        /// <summary>
        /// The contrast factor.
        /// Should be in the range [-1, 1].
        /// </summary>
        public float ContrastFactor { get; set; }

        public int[] Process(int[] inputPixels, int width, int height)
        {
            // Prepare some variables
            var resultPixels = new int[inputPixels.Length];



            //   contrast = (float)contrast;
            // Convert to integer factors
            var bfi = (int)(BrightnessFactor * 255);
            var cf = (1f + ContrastFactor) / 1f;
            cf *= cf;
            var cfi = (int)(cf * 32768);

            for (int i = 0; i < inputPixels.Length; i++)
            {
                // Extract color components
                var c = inputPixels[i];
                var a = (byte)(c >> 24);
                var r = (byte)(c >> 16);
                var g = (byte)(c >> 8);
                var b = (byte)(c);

                // Modify brightness (addition)
                if (bfi != 0)
                {
                    // Add brightness
                    var ri = r + bfi;
                    var gi = g + bfi;
                    var bi = b + bfi;

                    // Clamp to byte boundaries
                    r = (byte)(ri > 255 ? 255 : (ri < 0 ? 0 : ri));
                    g = (byte)(gi > 255 ? 255 : (gi < 0 ? 0 : gi));
                    b = (byte)(bi > 255 ? 255 : (bi < 0 ? 0 : bi));
                }

                // Modifiy contrast (multiplication)
                if (cfi != 0)
                {
                    // Transform to range [-128, 127]
                    var ri = r - 128;
                    var gi = g - 128;
                    var bi = b - 128;

                    // Multiply contrast factor
                    ri = (ri * cfi) >> 15;
                    gi = (gi * cfi) >> 15;
                    bi = (bi * cfi) >> 15;

                    // Transform back to range [0, 255]
                    ri = ri + 128;
                    gi = gi + 128;
                    bi = bi + 128;

                    // Clamp to byte boundaries
                    r = (byte)(ri > 255 ? 255 : (ri < 0 ? 0 : ri));
                    g = (byte)(gi > 255 ? 255 : (gi < 0 ? 0 : gi));
                    b = (byte)(bi > 255 ? 255 : (bi < 0 ? 0 : bi));
                }

                // Set result color
                resultPixels[i] = (a << 24) | (r << 16) | (g << 8) | b;
            }

            return resultPixels;
        }
    }

}
