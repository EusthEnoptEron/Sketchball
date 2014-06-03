using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace Sketchball.GameComponents
{
    /// <summary>
    /// Represents the "world" in which the pinball machine is placed. Purely graphically relevant.
    /// </summary>
    public class GameWorld
    {
        private const int MARGIN = 60;
        private const int PADDING = 40;

        private ImageSource BG_Top = Booster.OptimizeWpfImage("Notebook_top.png");
        private ImageSource BG_Bot = Booster.OptimizeWpfImage("Notebook_bot.png");
        private ImageSource BG_Rings = Booster.OptimizeWpfImage("Notebook_Ringe.png");
        private ImageSource BG_End = Booster.OptimizeWpfImage("Notebook_Ende.png");
        private ImageSource BG_LogoTop = Booster.OptimizeWpfImage("Logo_BFH.jpg");

        private ImageSource BG = Booster.OptimizeWpfImage("bg_desk.jpg");
        private ImageSource BG_Material = Booster.OptimizeWpfImage("bg_material.png");
        private ImageSource BG_Pen = Booster.OptimizeWpfImage("bg_pen.png");
        private ImageSource BG_Pen2 = Booster.OptimizeWpfImage("bg_pen2.png");
        private ImageSource BG_Coffee = Booster.OptimizeWpfImage("bg_coffee.png");
        private ImageSource BG_Paper = Booster.OptimizeWpfImage("bg_paper.jpg");


        private Game Game;


        public GameWorld(Game game)
        {
            Game = game;
        }


        public void Draw(DrawingContext g)
        {
            double height = Height + MARGIN * 4; // we're adding margins _again_ just to be sure.
            double width  = height / BG.Height * BG.Width;

            g.DrawImage(BG, new Rect( (Width - width) / 2, 0, width, height));

            // Draw other stuff
            double coffeeWidth = Width * 0.7;
            double coffeeHeight = coffeeWidth / BG_Coffee.Width * BG_Coffee.Height;
            double materialHeight = (coffeeWidth * 0.7) / BG_Material.Width * BG_Material.Height;

            g.DrawImage(BG_Coffee, new Rect(- coffeeWidth, 0, coffeeWidth, coffeeHeight));
            g.DrawImage(BG_Material, new Rect(-coffeeWidth + MARGIN + PADDING, coffeeHeight + materialHeight / 2, coffeeWidth * 0.7, materialHeight));

            DrawMachine(g);
            DrawToplist(g);

        }

        private void DrawMachine(DrawingContext g)
        {
            // InnerWidth + Padding
            int width = Game.Machine.Width + PADDING * 2;
            int height = Game.Machine.Height + PADDING * 2;


            double botHeight = (width - PADDING) / BG_Bot.Width * BG_Bot.Height;
            double topHeight = (width - PADDING) / BG_Top.Width * BG_Top.Height;

            double totalHeight = height + botHeight + topHeight;

            double ringWidth = totalHeight / BG_Rings.Height * BG_Rings.Width;
            double endWidth = totalHeight / BG_End.Height * BG_End.Width;

            double logoHeight = PADDING / 2d;
            double logoWidth  = logoHeight / BG_LogoTop.Height * BG_LogoTop.Width;


            // Draw book
            g.PushTransform(new TranslateTransform(MARGIN, MARGIN));

            g.DrawRectangle(Brushes.White, null, new Rect(-1, -1, width - PADDING / 1.5 + 2, height + 2));
            g.DrawImage(BG_Top, new Rect(ringWidth / 2 - 1, -topHeight, width - PADDING / 1.5 - ringWidth / 2, topHeight));
            g.DrawImage(BG_Rings, new Rect(-ringWidth / 2, -topHeight, ringWidth, totalHeight));
            g.DrawImage(BG_End, new Rect(width - PADDING / 1.5, -topHeight, endWidth, totalHeight));
            g.DrawImage(BG_Bot, new Rect(ringWidth / 2 - 1, height, width - PADDING / 1.5 - ringWidth / 2, botHeight));
            g.DrawImage(BG_LogoTop, new Rect(width - logoWidth - PADDING, (PADDING - logoHeight) / 2, logoWidth, logoHeight));

            // Draw machine
            g.PushTransform(new TranslateTransform(PADDING, PADDING));
            Game.Machine.Draw(g);
            g.Pop();
            g.Pop();

        }

        private void DrawToplist(DrawingContext g)
        {
            double paperWidth = Width * 0.7;
            double paperHeight = paperWidth / BG_Paper.Width * BG_Paper.Height;
            int fontSize = 20;

            g.PushTransform(new TranslateTransform(Width + MARGIN / 2, Height * 0.6));
            {
                g.PushTransform(new RotateTransform(-15));
                {
                    g.DrawImage(BG_Paper, new Rect(0, 0, paperWidth, paperHeight));

                    var title = Booster.GetText("Toplist", FontManager.CourgetteWpf, fontSize * 2, Brushes.Black);
                    title.SetTextDecorations(new TextDecorationCollection() { TextDecorations.Underline });
                    g.DrawText(title, new Point(30, 20));


                    var topTen = Game.Highscores.TakeWhile((entry, index) => { return index < 10; });
                    Point offset = new Point(30, 30 + title.Height);

                    FormattedText text;
                    double maxWidth = 0;
                    foreach (var entry in topTen)
                    {
                        text = Booster.GetText(entry.Score.ToString(), FontManager.CourgetteWpf, fontSize, Brushes.Black);
                        g.DrawText(text, offset);

                        maxWidth = Math.Max(maxWidth, text.Width);
                        offset.Y += text.Height;
                    }

                    offset = new Point(30 + maxWidth + fontSize, 30 + title.Height);
                    foreach (var entry in topTen)
                    {
                        text = Booster.GetText(entry.Player, FontManager.CourgetteWpf, fontSize, Brushes.Black);
                        g.DrawText(text, offset);

                        offset.Y += text.Height;
                    }


                    g.PushTransform(new RotateTransform(-3));
                    {
                        double penHeight = paperHeight / 2;
                        double penWidth =  penHeight / BG_Pen2.Height * BG_Pen2.Width;

                        g.DrawImage(BG_Pen2, new Rect(paperWidth - penWidth, penWidth, penWidth, penHeight));
                    }
                    g.Pop();
                }
                g.Pop();

            }
            g.Pop();
        }

        public int Width
        {
            get
            {
                return Game.Machine.Width + PADDING * 2 + MARGIN * 2;
            }
        }

        public int Height
        {
            get
            {
                return Game.Machine.Height + PADDING * 2 + MARGIN * 2;
            }
        }

        /// <summary>
        /// Gets the offset from (0, 0) to the pinball machine, i.e. the position where it is drawn at PinballMachine.Size.
        /// </summary>
        public Point Offset
        {
            get
            {
                return new Point(MARGIN + PADDING, MARGIN + PADDING);
            }
        }
    }
}
