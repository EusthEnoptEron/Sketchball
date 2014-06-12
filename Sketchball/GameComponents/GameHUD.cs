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
    /// <summary>
    /// Visual element that takes care of the head-up display.
    /// </summary>
    public class GameHUD
    {
        private Game Game;

        /// <summary>
        /// Gets the width of the HUD. Setting is not currently supported.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height of the HUD. Setting is not currently supported.
        /// </summary>
        public int Height { get; private set; }

        private double smilieWidth;
        private double smilieHeight;

        private double lineWidth;
        private double lineHeight;

        // Resources
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
            // 1. Draw BG
            g.DrawImage(BG, new Rect(0, 0, Width, Height));


            // 2. Create text objects
            var scoreTitle = GetText("Score:");
            var scoreText = GetText(Game.Score.ToString());

            var livesTitle = GetText("Balls: ");
            livesTitle.SetTextDecorations(new TextDecorationCollection(TextDecorations.Underline));
            scoreTitle.SetTextDecorations(new TextDecorationCollection(TextDecorations.Underline));
            
            var livesText = GetText(Game.Lives.ToString());

            // 3. Draw the stuff
            g.PushTransform(new TranslateTransform(Width / 4f, Height / 3.5f));
            {
                g.DrawText(scoreTitle, new Point(0, 0));
                g.DrawText(scoreText, new Point(scoreTitle.Width + 10, 0));
                g.DrawText(livesTitle, new Point(0, scoreTitle.Height + 5));


                // --- Lives ---
                int i = 0;
                for (; i < Game.Lives; i++)
                {
                    g.DrawImage(Line, new Rect(livesTitle.Width + (i + 1) * lineWidth, scoreText.Height + 5, lineWidth, lineHeight));
                }
                for (; i < Game.TOTAL_LIVES; i++)
                {
                    g.DrawImage(LineThrough, new Rect(livesTitle.Width + (i + 1) * lineWidth, scoreText.Height + 5, lineWidth, lineHeight));
                }

                g.PushTransform(new TranslateTransform(Width / 6, livesTitle.Height * 2));
                {
                    // --- Smilie ---
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
                }
                g.Pop();
            }
            g.Pop();
        }

        private FormattedText GetText(string text)
        {
            Typeface typeface = new Typeface(FontManager.CourgetteWpf, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal, new FontFamily("Arial"));
            return new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, 22, Brushes.Black);
        }

       

    }
}
