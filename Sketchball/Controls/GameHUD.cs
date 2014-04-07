using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sketchball
{
    public class GameHUD
    {
        private Game Game;
        public int Width;
        public int Height;

        private Image BG;
        public GameHUD(Game game)
        {
            Game = game;
            Width = 200;
            Height = 178;

            BG = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (Graphics g = Graphics.FromImage(BG))
            {
                g.DrawImage(Properties.Resources.ScoreBG, 0, 0, Width, Height);
            }
        }

        public void Draw(Graphics g)
        {
            Font font = new Font(FontManager.Courgette, 15);
            g.DrawImage(BG, 0, 0, Width, Height);

            string str = "Score: ";
            SizeF size = g.MeasureString(str, font);
            g.DrawString(str, font, Brushes.Black, Width - 150, 50);
            g.DrawString(Game.Score.ToString(), font, Brushes.Black, Width - 150 + size.Width, 50);

            str = "Lives: ";
            size = g.MeasureString(str, font);
            g.DrawString(str, font, Brushes.Black, Width - 150, 50 + size.Height);
            g.DrawString(Game.Lives.ToString(), font, Brushes.Black, Width - 150 + size.Width, 50 + size.Height);

        }

    }
}
