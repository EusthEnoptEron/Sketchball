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
        public Vector2 Velocity
        {
            get;
            set;
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
            base.Update(delta);
            Velocity += World.Acceleration * (delta / 1000f);
            Location += Velocity * (delta / 1000f);
        }

        public float Mass
        {
            get;
            set;
        }
    }
}
