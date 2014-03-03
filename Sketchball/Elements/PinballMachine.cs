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

        public ElementCollection Elements {get; private set;}
        public Vector2 Gravity { get; set; }
        private Size Bounds;

        public int Width { get { return Bounds.Width; } }
        public int Height { get { return Bounds.Height; } }


        public PinballMachine(Size bounds)
        {
            Elements = new ElementCollection(this);
            Bounds = bounds;
            Gravity = new Vector2(0, 1f);
        }

        internal void Update(long elapsed)
        {
            foreach (PinballElement element in Elements)
            {
                element.Update(elapsed);
            }

            // Find collisions
            // ...
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

        public void Add(PinballElement element)
        {
            Elements.Add(element);
        }

        public bool Rmeove(PinballElement element)
        {
            return Elements.Remove(element);
        }

    }
}
