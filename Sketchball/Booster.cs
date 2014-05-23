using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball
{
    /// <summary>
    /// Helper class that was created to improve the overall performance.
    /// </summary>
    static class Booster
    {
        /// <summary>
        /// Optimizes an image by converting it into the right pixel format.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static Bitmap OptimizeImage(Image src)
        {
            return OptimizeImage(src, src.Width, src.Height);
        }

        public static Bitmap OptimizeImage(Image src, int width)
        {
            return OptimizeImage(src, width, (int)((float)src.Height / src.Width * width));
        }

        public static Bitmap OptimizeImage(Image src, int width, int height)
        {
            try
            {
                var img = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.DrawImage(src, 0, 0, width, height);
                }

                return img;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
