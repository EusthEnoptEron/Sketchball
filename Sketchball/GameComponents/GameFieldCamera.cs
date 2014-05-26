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
    public class GameFieldCamera : Camera
    {
        internal Game Game { get; set; }

        public BackgroundManager backgroundManager { get; private set; }

        public Vector Translocation { get; set; }
        public Vector Scale { get; set; }


        public Size Size
        {
            get;
            set;
        }

        public double Width { get { return Size.Width; } }
        public double Height { get { return Size.Height; } }

        public GameFieldCamera(Game game)
        {
            Game = game;
            this.Translocation = new Vector(0, 0);
            this.Scale = new Vector(0, 0);
            //this.maximumSize = new Size(Screen.PrimaryScreen.Bounds.Width - this.offsetRight, (Screen.PrimaryScreen.Bounds.Height - offsetTopYAbs) / 100 * (100 - this.offsetY));
            this.backgroundManager = new BackgroundManager(game);
        }

        private void UpdateBackground()
        {
        }



        public void Draw(DrawingContext g)
        {
            int paddingRight = 50;

            int pushes = 0;

            if (Scale.X > 0 && Scale.Y > 0)
            {
                pushes++;
                g.PushTransform(new ScaleTransform(Scale.X, Scale.Y));
            }
            if (this.Translocation.X > 0 && this.Translocation.Y > 0)
            {
                pushes++;
                g.PushTransform(new TranslateTransform(Translocation.X, Translocation.Y));
            }

            double scale = Height / backgroundManager.Height;
            double dx    = (Width - backgroundManager.Width) / 2f - paddingRight; // Center

            pushes += 2;
            g.PushTransform(new TranslateTransform(dx, 0));
            g.PushTransform(new ScaleTransform(scale, scale, backgroundManager.Width / 2f, 0));

            backgroundManager.Draw(g);
        
            for (int i = 0; i < pushes; i++)
            {
                g.Pop();
            }
        }

        private float CalculateRatio(float width, float height) 
        {
            float widthRatio = (width) / Game.Machine.Width;
            float heightRatio = (height) / Game.Machine.Height;

            return Math.Min(widthRatio, heightRatio);
        }


        public void zoom(float factor)
        {
            //if (factor > 0)
            //{
            //    this.Scale *= factor;
            //    if (Scale.X < 1||Scale.Y < 1)
            //    {
            //        Scale = new Vector(1, 1);
            //    }
            //    this.backgroundManager.scaleMainBackground(this.Scale);
            //}
        }

        public void moveRel(Vector relativ)
        {
            //this.Translocation += relativ;
        }

        public void moveAbs(Vector absPos)
        {
            //this.Translocation = absPos;
        }

    }
}
