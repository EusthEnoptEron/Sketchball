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
    /// Represents a view on the GameWorld. It's your responsibility to feed it with the right screen sizes.
    /// </summary>
    public class GameFieldCamera : Camera
    {
        private GameWorld World = null;
        private GameHUD HUD = null;

        /// <summary>
        /// Gets or sets the (additional) translation of the camera.
        /// </summary>
        public Vector Translocation { get; set; }

        /// <summary>
        /// Gets or sets the (additional) scale of the camera.
        /// </summary>
        public Vector Scale { get; set; }

        /// <summary>
        /// Gets or sets the size of this camera.
        /// </summary>
        public Size Size
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current width.
        /// </summary>
        public double Width { get { return Size.Width; } }

        /// <summary>
        /// Gets the current height.
        /// </summary>
        public double Height { get { return Size.Height; } }

        /// <summary>
        /// Initializes a new camera.
        /// </summary>
        /// <param name="world">World the camera looks at.</param>
        /// <param name="hud">HUD to display somewhere.</param>
        public GameFieldCamera(GameWorld world, GameHUD hud)
        {
            World = world;
            HUD = hud;
            this.Translocation = new Vector(0, 0);
            this.Scale = new Vector(0, 0);
        }

        /// <summary>
        /// Draws the view.
        /// </summary>
        /// <param name="g"></param>
        public void Draw(DrawingContext g)
        {
            // Track our transform stack
            int pushes = 0;

            // Perform additional transforms
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

            // Calculate the scale factor we need to show the entire pinball machine (-> World.Height)
            double scale = Height / World.Height;
            double dx = (Width - World.Width - HUD.Width) / 2f; // Displacement to center

            pushes += 2;
            
            g.PushTransform(new TranslateTransform(dx, 0));
            g.PushTransform(new ScaleTransform(scale, scale, World.Width / 2f, 0));
            {
                // ----------  Do the magic. ---------------
                World.Draw(g);
            }

            // Unwind transform stack
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
