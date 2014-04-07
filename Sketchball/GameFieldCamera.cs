using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball
{
    public class GameFieldCamera : Camera
    {
        internal Game Game { get; set; }

        public GameFieldCamera(Game game)
        {
            Game = game;
        }

        public void Draw(Graphics g, Rectangle bounds)
        {
            float widthRatio = (float)bounds.Width / Game.Machine.Width;
            float heightRatio = (float)bounds.Height / Game.Machine.Height;

            float ratio = Math.Min(widthRatio, heightRatio);

            GraphicsState state = g.Save();
            try
            {
                g.ScaleTransform(ratio, ratio);
                Game.Machine.Draw(g);
            }
            finally
            {
                g.Restore(state);
            }
        }
    }
}
