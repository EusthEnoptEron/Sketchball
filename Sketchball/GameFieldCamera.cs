using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace Sketchball
{
    public class GameFieldCamera : Camera
    {
        internal Game Game { get; set; }

        public BackgroundManager backgroundManager { get; private set; }

        public Vector2 Translocation { get; set; }
        public Vector2 Scale { get; set; }


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
            this.Translocation = new Vector2(0, 0);
            this.Scale = new Vector2(0, 0);
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

            double scale = Height / (float)backgroundManager.Height;
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
            float widthRatio = (float)(width) / Game.Machine.Width;
            float heightRatio = (float)(height) / Game.Machine.Height;

            return Math.Min(widthRatio, heightRatio);
        }


        public void zoom(float factor)
        {
            //if (factor > 0)
            //{
            //    this.Scale *= factor;
            //    if (Scale.X < 1||Scale.Y < 1)
            //    {
            //        Scale = new Vector2(1, 1);
            //    }
            //    this.backgroundManager.scaleMainBackground(this.Scale);
            //}
        }

        public void moveRel(Vector2 relativ)
        {
            //this.Translocation += relativ;
        }

        public void moveAbs(Vector2 absPos)
        {
            //this.Translocation = absPos;
        }

    }
}
