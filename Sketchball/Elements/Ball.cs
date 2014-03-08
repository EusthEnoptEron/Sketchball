using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class Ball : PinballElement, IPhysics
    {

        private Vector2 v;
        private Vector2 v0;
        long t = 0;

        public Vector2 Velocity
        {
            get
            {
                return v;
            }
            set
            {
                v0 = value;
                t = 0;
                v = v0;
            }
        }

        public Ball()
        {
            Velocity = new Vector2();
            Mass = 0.2f;
            Width = 60;
            Height = 60;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            //g.FillEllipse(Brushes.Peru, 0, 0, Width, Height);
            g.DrawImage(Properties.Resources.BallWithAlpha, 0, 0, Width, Height);
        }

        public override void Update(long delta)
        {
            t += delta;
            base.Update(delta);
            v = v0 + (t / 1000f) * World.Acceleration;
            Location += v * (delta / 1000f);
        }

        public float Mass
        {
            get;
            set;
        }
    }
}
