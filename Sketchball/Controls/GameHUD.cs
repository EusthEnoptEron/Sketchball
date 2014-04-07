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

        public GameHUD(Game game)
        {
            Game = game;
            Width = 200;
            Height = 200;

        }

        public void Draw(Graphics g)
        {
            Font font = new Font(FontManager.Courgette, 15);


            string str = "Score: ";
            SizeF size = g.MeasureString(str, font);
            g.DrawString(str, font, Brushes.Black, Width - 200, 50);
            g.DrawString(Game.Score.ToString(), font, Brushes.Black, Width - 200 + size.Width, 50);

            str = "Lives: ";
            size = g.MeasureString(str, font);
            g.DrawString(str, font, Brushes.Black, Width - 200, 50 + size.Height);
            g.DrawString(Game.Lives.ToString(), font, Brushes.Black, Width - 200 + size.Width, 50 + size.Height);

        }

    }
}
