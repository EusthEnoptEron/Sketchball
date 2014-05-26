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
    public class BackgroundManager
    {

        private float scaleModBG = 1.01f;

        public readonly int offsetY = 2;           //%
        public readonly int offsetTopYAbs = 25;    //abs
        public readonly int offsetRight = 450;     //offset Right due to score etc

        private static int PADDING = 60;

        private ImageSource BG_Body = Booster.OptimizeWpfImage("Notebook_body.png");
        private ImageSource BG_Bot = Booster.OptimizeWpfImage("Notebook_bot.png");
        private ImageSource BG_Rings = Booster.OptimizeWpfImage("Notebook_Ringe.png");
        private ImageSource BG_End = Booster.OptimizeWpfImage("Notebook_Ende.png");
        private ImageSource BG_LogoTop = Booster.OptimizeWpfImage("Logo_BFH.jpg");

        private ImageSource BG = Booster.OptimizeWpfImage("TableBackground.jpg");

        public event EventHandler backgroundChanged;

        private Game Game;


        public BackgroundManager(Game game)
        {
            Game = game;
        }


        public void Draw(DrawingContext g)
        {

            // Draw machine
            //Machine.Draw(g);
            int width = Game.Machine.Width;
            int height = Game.Machine.Height;

            g.DrawImage(BG, new Rect(-width / 2 - 50, 0, (height * 1.2 / BG.Height * BG.Width), height * 1.2));

            double botHeight = width / BG_Bot.Width * BG_Bot.Height;
            double h = height + botHeight;
            double ringWidth = h / BG_Rings.Height * BG_Rings.Width;
            double endWidth = h / BG_End.Height * BG_End.Width;

            g.DrawImage(BG_Body, new Rect(0, PADDING, width, height));
            g.DrawImage(BG_Rings, new Rect(-ringWidth, PADDING, ringWidth, h));
            g.DrawImage(BG_End, new Rect(width, PADDING, endWidth, h));
            g.DrawImage(BG_Bot, new Rect(0, height + PADDING, width, botHeight));


            g.PushTransform(new TranslateTransform(0, PADDING));
            Game.Machine.Draw(g);
            g.Pop();
        }


        public int Width
        {
            get
            {
                return Game.Machine.Width + PADDING * 2;
            }
        }

        public int Height
        {
            get
            {
                return Game.Machine.Height + PADDING * 2;
            }
        }
    }
}
