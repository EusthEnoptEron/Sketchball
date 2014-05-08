using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball
{
    public class GameFieldCamera : Camera
    {
        internal Game Game { get; set; }
        private Image Notebook = Properties.Resources.Notebook;
        private Bitmap Background;


        private Size _size;
        public Size Size
        {
            get
            {
                return _size;
            }
            set
            {
                // Size changed
                _size = value;

                UpdateBackground();
            }
        }


        private void UpdateBackground()
        {

            if (Size != Size.Empty)
            {
                float ratio = CalculateRatio(Size.Width, Size.Height);

                Background = Booster.OptimizeImage(Notebook, (int)(ratio * Game.Machine.Width), (int)(ratio * Game.Machine.Height));
            }
        }

        public GameFieldCamera(Game game)
        {
            Game = game;
            UpdateBackground();
        }

        public void Draw(Graphics g)
        {
            float ratio = CalculateRatio(Background.Width / 5f * 4, Background.Height / 6f * 5);

            GraphicsState state = g.Save();
            try
            {
                g.DrawImageUnscaled(Background, 0, 0);
                
                g.ScaleTransform(ratio, ratio);
                g.TranslateTransform(60, 50);
                Game.Machine.Draw(g);
            }
            finally
            {
                g.Restore(state);
            }
        }

        private float CalculateRatio(float width, float height) 
        {
            float widthRatio = (float)(width) / Game.Machine.Width;
            float heightRatio = (float)(height) / Game.Machine.Height;

            return Math.Min(widthRatio, heightRatio);
        }
    }
}
