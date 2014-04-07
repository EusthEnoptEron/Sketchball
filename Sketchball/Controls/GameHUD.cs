using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sketchball
{
    public class GameHUD : UserControl
    {
        private Game Game;
        public GameHUD(Game game)
        {
            Game = game;
            Width = 200;
            Height = 200;

            Game.LivesChanged += (s, l) => { Invalidate(); };
            Game.ScoreChanged += (s, sc) => { Invalidate(); };

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            string str = "Score: ";
            SizeF size = g.MeasureString(str, SystemFonts.DefaultFont);
            g.DrawString(str, SystemFonts.DefaultFont, Brushes.Black, Width - 200, 50);
            g.DrawString(Game.Score.ToString(), SystemFonts.DefaultFont, Brushes.Black, Width - 200 + size.Width, 50);

            str = "Lives: ";
            size = g.MeasureString(str, SystemFonts.DefaultFont);
            g.DrawString(str, SystemFonts.DefaultFont, Brushes.Black, Width - 200, 50 + size.Height);
            g.DrawString(Game.Lives.ToString(), SystemFonts.DefaultFont, Brushes.Black, Width - 200 + size.Width, 50 + size.Height);

        }

    }
}
