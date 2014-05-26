using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Sketchball
{
    public class GameHUD
    {
        private Game Game;
        public int Width { get; set; }
        public int Height { get; set; }

        private ImageSource BG = Booster.OptimizeWpfImage("ScoreBG.png");

        public GameHUD(Game game)
        {
            Game = game;
            Width = 200;
            Height = 178;
        }

        public void Draw(DrawingContext g)
        {
            g.DrawImage(BG, new Rect(0, 0, Width, Height));

            var scoreTitle = GetText("Score: ");
            var scoreText = GetText(Game.Score.ToString());

            var livesTitle = GetText("Lives: ");
            var livesText = GetText(Game.Lives.ToString());

            g.DrawText(scoreTitle, new Point(Width - 150, 50));
            g.DrawText(scoreText, new Point(Width - 150 + scoreTitle.Width, 50));
            g.DrawText(livesTitle, new Point(Width - 150, 50 + scoreTitle.Height));
            g.DrawText(livesText, new Point(Width - 150 + livesTitle.Width, 50 + scoreTitle.Height));

            //TODO
            /*SizeF size = g.MeasureString(str, font);
            g.DrawString(str, font, Brushes.Black, Width - 150, 50);
            g.DrawString(Game.Score.ToString(), font, Brushes.Black, Width - 150 + size.Width, 50);

            str = "Lives: ";
            size = g.MeasureString(str, font);
            g.DrawString(str, font, Brushes.Black, Width - 150, 50 + size.Height);
            g.DrawString(Game.Lives.ToString(), font, Brushes.Black, Width - 150 + size.Width, 50 + size.Height);*/

        }

        private FormattedText GetText(string text)
        {
            Typeface typeface = new Typeface(FontManager.CourgetteWpf, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal, new FontFamily("Arial"));
            return new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, 22, Brushes.Black);
        }

    }
}
