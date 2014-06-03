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
        private GameWorld World = null;
        private GameHUD HUD = null;

        public Vector Translocation { get; set; }
        public Vector Scale { get; set; }

        public Size Size
        {
            get;
            set;
        }

        public double Width { get { return Size.Width; } }
        public double Height { get { return Size.Height; } }

        public GameFieldCamera(GameWorld world, GameHUD hud)
        {
            World = world;
            HUD = hud;
            this.Translocation = new Vector(0, 0);
            this.Scale = new Vector(0, 0);
        }

        private void UpdateBackground()
        {
        }



        public void Draw(DrawingContext g)
        {
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

            double scale = Height / World.Height;
            double dx = (Width - World.Width - HUD.Width) / 2f; // Center

            pushes += 2;
            g.PushTransform(new TranslateTransform(dx, 0));
            g.PushTransform(new ScaleTransform(scale, scale, World.Width / 2f, 0));
            {

                World.Draw(g);
            }
            for (int i = 0; i < pushes; i++)
            {
                g.Pop();
            }

            // Move HUD next to the game field
            g.PushTransform(new TranslateTransform(dx + World.Width / 2 + (World.Width * scale) / 2, World.Offset.Y));
            {
                HUD.Draw(g);
            }
            g.Pop();
        }
    }
}
