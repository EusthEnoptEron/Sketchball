using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace Sketchball
{
    public class GameHUD
    {
        private Game Game;
        public int Width { get; set; }
        public int Height { get; set; }

        private ImageSource BG;
        double times = 0;
        int count = 0;
        double average;

        public GameHUD(Game game)
        {
            Game = game;
            Width = 200;
            Height = 178;

            BG = Booster.OptimizeWpfImage("ScoreBG.png");
        }

        public void Draw(DrawingContext g)
        {
            Stopwatch watch = new Stopwatch();

           
            Font font = new Font(FontManager.Courgette, 15);
            watch.Start();
            g.DrawImage(BG, new Rect(0, 0, Width, Height));

            watch.Stop();
            times += watch.ElapsedTicks;
            count++;
            average = times / count;

            string str = "Score: ";
            //TODO
            /*SizeF size = g.MeasureString(str, font);
            g.DrawString(str, font, Brushes.Black, Width - 150, 50);
            g.DrawString(Game.Score.ToString(), font, Brushes.Black, Width - 150 + size.Width, 50);

            str = "Lives: ";
            size = g.MeasureString(str, font);
            g.DrawString(str, font, Brushes.Black, Width - 150, 50 + size.Height);
            g.DrawString(Game.Lives.ToString(), font, Brushes.Black, Width - 150 + size.Width, 50 + size.Height);*/

        }

    }
}
