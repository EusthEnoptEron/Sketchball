using Sketchball.Collision;
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
    public class PinballMachine : ICloneable, IDisposable
    {
        // 500px = 1m
        public const float PIXELS_TO_METERS_RATIO = 500f / 1;

        public ElementCollection DynamicElements { get; private set; }
        public ElementCollection StaticElements { get; private set; }

        public ElementCollection Balls { get; private set; }

        public IEnumerable<PinballElement> Elements
        {
            get
            {
                foreach (PinballElement el in StaticElements)
                    yield return el;
                foreach (PinballElement el in DynamicElements)
                    yield return el;
                foreach (PinballElement el in Balls)
                    yield return el;
            }
        }

        public IMachineLayout Layout { get; private set; }

        protected StartingRamp Ramp;

        public int Width { get { return Layout.Width; } }
        public int Height { get { return Layout.Height; } }

        public float Gravity = 9.81f;

           
        /// <summary>
        /// Tilt of the pinball machine in radians.
        /// </summary>
        public float Angle = (float)(Math.PI / 180 * 10);

       
        public PinballMachine() : this(new DefaultLayout())
        {
        }

        public PinballMachine(IMachineLayout layout)
        {
            Layout = layout;

            StaticElements = new ElementCollection(this);
            DynamicElements = new ElementCollection(this);
            Balls = new ElementCollection(this);

            Layout.Apply(this);

            Ramp = (StartingRamp)StaticElements.FirstOrDefault((e) => { return e is StartingRamp; });
        }


        /// <summary>
        /// Gets the calculated acceleration based on the gravity and the angle.
        /// </summary>
        public Vector2 Acceleration
        {
            get
            {
                return new Vector2(0, (float)Math.Sin(Angle) * Gravity * PIXELS_TO_METERS_RATIO);
            }
        }

        /// <summary>
        /// Draws the pinball elements and all its components.
        /// </summary>
        /// <param name="g"></param>
        public virtual void Draw(Graphics g)
        {
            GraphicsState gsave = g.Save();
            try
            {
                g.IntersectClip(new Rectangle(0, 0, Width, Height));

               // g.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);

                // Draw contours


                foreach (PinballElement element in Elements)
                {
                    GraphicsState gstate = g.Save();

                    g.TranslateTransform(element.X, element.Y);
                    element.Draw(g);

                    g.Restore(gstate);
                }

            }
            finally
            {
                g.Restore(gsave);
            }
        }


        public void Add(PinballElement element)
        {
            DynamicElements.Add(element);
        }

        public bool Remove(PinballElement element)
        {
            return DynamicElements.Remove(element);
        }


        public object Clone()
        {
            PinballMachine machine = new PinballMachine(Layout);

            machine.Angle = Angle;
            machine.Gravity = Gravity;

            // Clone elements
            machine.DynamicElements = new ElementCollection(machine);
            foreach (PinballElement element in DynamicElements)
            {
                machine.DynamicElements.Add((PinballElement)element.Clone());
            }

            return machine;
        }

        public void addBall(Ball b)
        {
            this.Balls.Add(b);
        }


        /// <summary>
        /// Disposes the pinball machine and frees all ressources used by it.
        /// </summary>
        public void Dispose()
        {
            DynamicElements.Clear();
            Balls.Clear();
        }
       
    }
}
