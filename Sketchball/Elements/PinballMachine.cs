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
    public class PinballMachine
    {
        // 500px = 1m
        public const float PIXELS_TO_METERS_RATIO = 500f / 1;

        public ElementCollection Elements {get; private set;}
        public List<Ball> Balls { get; private set;}

        private BoundingRaster boundingRaster;

        public Size Bounds {get; private set;}

        public int Width { get { return Bounds.Width; } }
        public int Height { get { return Bounds.Height; } }

        public float Gravity = 9.81f;

        

        /// <summary>
        /// Tilt of the pinball machine in radians.
        /// </summary>
        public float Angle = (float)(Math.PI / 180 * 0.3f);

        public PinballMachine(Size bounds)
        {
            Elements = new ElementCollection(this);
            this.Balls = new List<Ball>();
            Bounds = bounds;

            this.boundingRaster = new BoundingRaster(Width / 60, Height / 60,Width,Height);
            
        }

        public Vector2 Acceleration
        {
            get
            {
                return new Vector2(0, (float)Math.Sin(Angle) * Gravity * PIXELS_TO_METERS_RATIO);
            }
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

            foreach(Ball b in this.Balls)
            {
                GraphicsState gstate = g.Save();

                g.TranslateTransform(b.X, b.Y);
                b.Draw(g);

                g.Restore(gstate);
            }

            
        }

        public virtual void Update(long elapsed)
        {

        }

        /// <summary>
        /// No more elements can be added after this function call 
        /// </summary>
        public void prepareForLaunch()
        {
            this.boundingRaster = new BoundingRaster(Width / 60, Height / 60, Width, Height);
            this.boundingRaster.takeOverBoundingBoxes(this.Elements);
        }

        public void handleCollision()
        {
            foreach (Ball b in this.Balls)
            {
                this.boundingRaster.handleCollision(b);
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

        public void addBall(Ball b)
        {
            this.Balls.Add(b);
        }

        public void debugDraw(Graphics g)
        {
            g.DrawEllipse(Pens.Orange, this.boundingRaster.hitPointDebug.X, this.boundingRaster.hitPointDebug.Y, 2, 2);
        }

        internal void addAnimatedObject(PinballElement tr)
        {
            foreach (BoundingBox b in tr.boundingContainer.getBoundingBoxes())
            {
                this.boundingRaster.addAnimatedObject(b);
            }
        }
    }
}
