using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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

        private static System.Windows.Controls.Image GetWpfImage(string path)
        {
            var img = new System.Windows.Controls.Image();
            img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Sketchball;component/Resources/" + path));
            return img;
        }

        public static System.Windows.Media.ImageSource OptimizeWpfImage(string path)
        {
            return GetWpfImage(path).Source;
            
        }
        public static System.Windows.Media.ImageSource OptimizeWpfImage(string path, int width)
        {
            var img = GetWpfImage(path);
            var height = width / img.Width * img.Height;
            img.Width = width;
            img.Height = height;

            return img.Source;
        }

        public static System.Windows.Media.ImageSource OptimizeWpfImage(string path, int width, int height)
        {
            var img = GetWpfImage(path);
            img.Width = width;
            img.Height = height;

            return img.Source;
        }
    }
}
