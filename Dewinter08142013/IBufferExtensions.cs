using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Brightness_Contrast
{
    public static class IBufferExtensions
    {
        public static IBufferExtensions.PixelBufferInfo GetPixels(this IBuffer pixelBuffer)
        {
            return new IBufferExtensions.PixelBufferInfo(pixelBuffer);
        }

        public class PixelBufferInfo
        {
            private Stream pixelStream;
            public byte[] Bytes;

            public int this[int i]
            {
                get
                {
                    return ColorExtensions.IntColorFromBytes(this.Bytes[i * 4 + 3], this.Bytes[i * 4 + 2], this.Bytes[i * 4 + 1], this.Bytes[i * 4]);
                }
                set
                {
                    this.Bytes[i * 4 + 3] = (byte)(value >> 24 & (int)byte.MaxValue);
                    this.Bytes[i * 4 + 2] = (byte)(value >> 16 & (int)byte.MaxValue);
                    this.Bytes[i * 4 + 1] = (byte)(value >> 8 & (int)byte.MaxValue);
                    this.Bytes[i * 4] = (byte)(value & (int)byte.MaxValue);
                    this.pixelStream.Seek((long)(i * 4), SeekOrigin.Begin);
                    this.pixelStream.Write(this.Bytes, i * 4, 4);
                }
            }

            public PixelBufferInfo(IBuffer pixelBuffer)
            {
                this.pixelStream = WindowsRuntimeBufferExtensions.AsStream(pixelBuffer);
                this.Bytes = new byte[this.pixelStream.Length];
                this.pixelStream.Seek(0L, SeekOrigin.Begin);
                this.pixelStream.Read(this.Bytes, 0, this.Bytes.Length);
            }

            public byte MaxDiff(int i, int color)
            {
                return Math.Max(Math.Max(Math.Max((byte)Math.Abs((int)this.Bytes[i * 4 + 3] - (color >> 24 & (int)byte.MaxValue)), (byte)Math.Abs((int)this.Bytes[i * 4 + 2] - (color >> 16 & (int)byte.MaxValue))), (byte)Math.Abs((int)this.Bytes[i * 4 + 1] - (color >> 8 & (int)byte.MaxValue))), (byte)Math.Abs((int)this.Bytes[i * 4] - (color & (int)byte.MaxValue)));
            }

            public void UpdateFromBytes()
            {
                this.pixelStream.Seek(0L, SeekOrigin.Begin);
                this.pixelStream.Write(this.Bytes, 0, this.Bytes.Length);
            }
        }
    }
}
