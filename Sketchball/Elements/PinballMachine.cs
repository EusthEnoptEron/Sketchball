using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    [Serializable]
    public class PinballMachine
    {
        public const float PIXELS_TO_METERS_RATIO = 0.5f;

        public ElementCollection Elements {get; private set;}
        public Size Bounds {get; private set;}

        public int Width { get { return Bounds.Width; } }
        public int Height { get { return Bounds.Height; } }

        public float Gravity = 9.81f;

        /// <summary>
        /// Tilt of the pinball machine in radians.
        /// </summary>
        public float Tilt = (float)(Math.PI / 180 * 30);

        public PinballMachine(Size bounds)
        {
            Elements = new ElementCollection(this);
            Bounds = bounds;
        }
       

        public void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.Black, 0, 0, Width, Height);
            foreach (PinballElement element in Elements)
            {
                GraphicsState gstate = g.Save();

                g.TranslateTransform(element.X, element.Y);
                element.Draw(g);

                g.Restore(gstate);
            }
        }
        public virtual void Update(long elapsed)
        {
        }


        public void Add(PinballElement element)
        {
            Elements.Add(element);
        }

        public bool Remove(PinballElement element)
        {
            return Elements.Remove(element);
        }

    }
}
