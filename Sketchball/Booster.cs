using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        public static System.Drawing.Bitmap OptimizeImage(System.Drawing.Image src)
        {
            return OptimizeImage(src, src.Width, src.Height);
        }

        public static System.Drawing.Bitmap OptimizeImage(System.Drawing.Image src, int width)
        {
            return OptimizeImage(src, width, (int)((float)src.Height / src.Width * width));
        }

        public static System.Drawing.Bitmap OptimizeImage(System.Drawing.Image src, int width, int height)
        {
            try
            {
                var img = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                using (var g = System.Drawing.Graphics.FromImage(img))
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

        private static Image GetWpfImage(string path)
        {
            var img = new Image();
            img.Source = new BitmapImage(new Uri(@"pack://application:,,,/Sketchball;component/Resources/" + path));
            return img;
        }

        public static ImageSource OptimizeWpfImage(string path)
        {
            return GetWpfImage(path).Source;
            
        }
        public static ImageSource OptimizeWpfImage(string path, int width)
        {
            var img = GetWpfImage(path);
            var height = width / img.Width * img.Height;
            img.Width = width;
            img.Height = height;

            return img.Source;
        }

        public static ImageSource OptimizeWpfImage(string path, int width, int height)
        {
            var img = GetWpfImage(path);
            img.Width = width;
            img.Height = height;

            return img.Source;
        }

        public static FormattedText GetText(string text, FontFamily family, double size, Brush color)
        {
            Typeface typeface = new Typeface(family, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal, new FontFamily("Arial"));
            return new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, size, color);
        }

        public static System.Drawing.Bitmap DrawingToBitmap(Drawing drawing, int width, int height)
        {
            var encoder = new PngBitmapEncoder();
            var drawingImage = new DrawingImage(drawing);

            var image = new Image() { Source = drawingImage };
            image.Arrange(new Rect(0, 0, width, height));

            var bitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(image);

            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            var stream = new MemoryStream();
            encoder.Save(stream);

            return new System.Drawing.Bitmap(stream);
        }


    }
}
