using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Sketchball.GameComponents
{
    public class GameHUD
    {
        private Game Game;
        public int Width { get; set; }
        public int Height { get; set; }

        private double smilieWidth;
        private double smilieHeight;

        private double lineWidth;
        private double lineHeight;


        private ImageSource BG = Booster.OptimizeWpfImage("postit.png");
        private ImageSource Line = Booster.OptimizeWpfImage("line.png");
        private ImageSource LineThrough = Booster.OptimizeWpfImage("line_strikethrough.png");
        private ImageSource GoodSmilie = Booster.OptimizeWpfImage("smilie_happy.png");
        private ImageSource MediumSmilie = Booster.OptimizeWpfImage("smilie_nervous.png");
        private ImageSource BadSmilie = Booster.OptimizeWpfImage("smilie_lamenting.png");



        public GameHUD(Game game)
        {
            Game = game;
            Width = 300;
            Height = (int)(Width / BG.Width * BG.Height);

            lineWidth = 10;
            lineHeight = lineWidth / Line.Width * Line.Height;

            smilieWidth = 100;
            smilieHeight = smilieWidth / GoodSmilie.Width * GoodSmilie.Height;

        }

        public void Draw(DrawingContext g)
        {
            g.DrawImage(BG, new Rect(0, 0, Width, Height));

            var scoreTitle = GetText("Score:");
            var scoreText = GetText(Game.Score.ToString());

            var livesTitle = GetText("Balls: ");
            livesTitle.SetTextDecorations(new TextDecorationCollection(TextDecorations.Underline));
            scoreTitle.SetTextDecorations(new TextDecorationCollection(TextDecorations.Underline));
            
            var livesText = GetText(Game.Lives.ToString());

            g.PushTransform(new TranslateTransform(Width / 4f, Height / 3.5f));
            {
                g.DrawText(scoreTitle, new Point(0, 0));
                g.DrawText(scoreText, new Point(scoreTitle.Width + 10, 0));
                g.DrawText(livesTitle, new Point(0, scoreTitle.Height+5));

                int i = 0;
                for (; i < Game.Lives; i++)
                {
                    g.DrawImage(Line, new Rect(livesTitle.Width + (i+1) * lineWidth, scoreText.Height+5, lineWidth, lineHeight));
                }
                for (; i < Game.TOTAL_LIVES; i++)
                {
                    g.DrawImage(LineThrough, new Rect(livesTitle.Width + (i+1) * lineWidth, scoreText.Height+5, lineWidth, lineHeight));
                }

                g.PushTransform(new TranslateTransform(Width / 6, livesTitle.Height * 2));

                if (Game.Lives < Game.TOTAL_LIVES / 3)
                {
                    g.DrawImage(BadSmilie, new Rect(0, 0, smilieWidth, smilieHeight));
                }
                else if (Game.Lives < Game.TOTAL_LIVES / 3 * 2)
                {
                    g.DrawImage(MediumSmilie, new Rect(0, 0, smilieWidth, smilieHeight));
                }
                else
                {
                    g.DrawImage(GoodSmilie, new Rect(0, 0, smilieWidth, smilieHeight));
                }
                
                g.Pop();
                //g.DrawText(livesText, new Point(livesTitle.Width, scoreTitle.Height));
            }
            g.Pop();

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
