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
    public class PinballMachine : ICloneable
    {
        public delegate void CollisionEventHandler(PinballElement sender);
        public delegate void GameOverEventHandler();

        public event CollisionEventHandler Collision;
        public event GameOverEventHandler GameOver;

        // 500px = 1m
        public const float PIXELS_TO_METERS_RATIO = 500f / 1;

        public ElementCollection Elements {get; private set;}
        public ElementCollection Balls { get; private set; }
        private List<PinballElement> FallenBalls = new List<PinballElement>();

        private StartingRamp Ramp;


        public Size Bounds {get; private set;}

        public int Width { get { return Bounds.Width; } }
        public int Height { get { return Bounds.Height; } }

        public float Gravity = 9.81f;

           
        /// <summary>
        /// Tilt of the pinball machine in radians.
        /// </summary>
        public float Angle = (float)(Math.PI / 180 * 30);

        internal InputManager Input { get; private set; }

        public PinballMachine(Size bounds) : this(bounds.Width, bounds.Height)
        {
        }

        public PinballMachine(int width, int height)
        {
            Input = new InputManager();
            Elements = new ElementCollection(this);
            Balls = new ElementCollection(this);

            Bounds = new Size(width, height);

            // Set starting ramp
            Ramp = new StartingRamp();
            Ramp.World = this;
            Elements.Add(Ramp);
            
            Ramp.X = Width - Ramp.Width;
            Ramp.Y = Height - Ramp.Height;
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


        /// <summary>
        /// Updates the internal physical environment.
        /// </summary>
        /// <param name="elapsed"></param>
        public virtual void Update(long elapsed)
        {
            foreach (PinballElement element in Elements)
            {
                element.Update(elapsed);

                if(element is Ball)
                    KeepContained((Ball)element);
            }


            if (FallenBalls.Count > 0)
            {
                foreach (PinballElement element in FallenBalls)
                {
                    Remove(element);
                }
                FallenBalls.Clear();

                GameOver();
            }
           
        }

        private void KeepContained(Ball element)
        {
            Ball el = element;

            if (element.Y > Height)
            {
                //element.Y = Height - element.Height;

                // el.Velocity = new Vector2(el.Velocity.X * .6f, -el.Velocity.Y * .6f);
                FallenBalls.Add(element);
            }
            if (element.X < 0 || element.X + element.Width > Width)
            {
                element.X = Math.Max(0, Math.Min(Width - element.Width, element.X));
                el.Velocity = new Vector2(-el.Velocity.X * .6f, el.Velocity.Y);
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

        internal bool HasBall()
        {
            //throw new NotImplementedException();
            return Elements.FirstOrDefault((el) => { return el is Ball; }) != null;
        }

        internal void IntroduceBall()
        {
            Ball ball = new Ball();

            Ramp.IntroduceBall(ball);
            Elements.Add(ball);
        }
    }
}
