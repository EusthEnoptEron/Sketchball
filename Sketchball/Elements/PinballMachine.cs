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

        public ElementCollection Contours { get; private set; }
        public ElementCollection Elements {get; private set;}
        public ElementCollection Balls { get; private set; }

        protected StartingRamp Ramp;

        public Size Bounds {get; private set;}

        public int Width { get { return Bounds.Width; } }
        public int Height { get { return Bounds.Height; } }

        public float Gravity = 9.81f;

           
        /// <summary>
        /// Tilt of the pinball machine in radians.
        /// </summary>
        public float Angle = (float)(Math.PI / 180 * 10);

       
        public PinballMachine(Size bounds) : this(bounds.Width, bounds.Height)
        {
        }

        public PinballMachine(int width, int height)
        {
            Contours = new ElementCollection(this);
            Elements = new ElementCollection(this);
            Balls = new ElementCollection(this);

            Bounds = new Size(width, height);

            BuildContours();

            // Set starting ramp
            Ramp = new StartingRamp();
            Ramp.World = this;
            Elements.Add(Ramp);
            
            Ramp.X = Width - Ramp.Width - 5;
            Ramp.Y = Height - Ramp.Height - 5;
        }


        private void BuildContours()
        {
            // Left border
            Contours.Add(new Line(0, 545, 75, 145));
            Contours.Add(new Line(75, 145, 137, 52));

            // Teppen
            Contours.Add(new Line(137, 52, 194, 21));
            Contours.Add(new Line(194, 21, 235, 7.5f));

            var totalWidth = 235 * 2;

            Contours.Add(new Line(235, 7.5f, totalWidth - 194, 21));
            Contours.Add(new Line(totalWidth - 194, 21, totalWidth - 137, 52));
            Contours.Add(new Line(totalWidth - 137, 52, totalWidth - 75, 145));
            Contours.Add(new Line(totalWidth - 75, 145, totalWidth, 545));
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
        public void Draw(Graphics g)
        {
            GraphicsState gsave = g.Save();
            try
            {
                g.IntersectClip(new Rectangle(0, 0, Width, Height));

                g.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);

                // Draw contours
                foreach (PinballElement element in Contours)
                {
                    GraphicsState gstate = g.Save();

                    g.TranslateTransform(element.X, element.Y);
                    element.Draw(g);

                    g.Restore(gstate);
                }


                foreach (PinballElement element in Elements)
                {
                    GraphicsState gstate = g.Save();

                    g.TranslateTransform(element.X, element.Y);
                    element.Draw(g);

                    g.Restore(gstate);
                }
				
 				foreach(Ball b in this.Balls)
            	{
                    GraphicsState gstate = g.Save();

                    g.TranslateTransform(b.X, b.Y);
                    b.Draw(g);

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
            Elements.Add(element);
        }

        public bool Remove(PinballElement element)
        {
            return Elements.Remove(element);
        }


        public object Clone()
        {
            PinballMachine machine = (PinballMachine)this.MemberwiseClone();

            machine.Bounds = new Size(Width, Height);

            // Clone elements
            machine.Elements = new ElementCollection(machine);
            foreach (PinballElement element in Elements)
            {
                machine.Elements.Add((PinballElement)element.Clone());
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
            Elements.Clear();
            Balls.Clear();
        }
       
    }
}
