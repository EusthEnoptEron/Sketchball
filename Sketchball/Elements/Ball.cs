using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class Ball : PinballElement
    {
        private static readonly Size size = new Size(20, 20);
        private static Image image = Booster.OptimizeImage(Properties.Resources.BallWithAlpha, size.Width, size.Height);
       
        protected override Size BaseSize
        {
            get { return size; }
        }

        public Vector2 Velocity
        {
            get;
            set;
        }

        public Ball() : base()
        {
            Velocity = new Vector2(0,0);
        }

        protected override void Init()
        {
            BoundingCircle bC = new BoundingCircle(10, new Vector2(0, 0));
            this.boundingContainer.addBoundingBox(bC);
        }

        protected override void OnDraw(System.Drawing.Graphics g)
        {
            //g.FillEllipse(Brushes.Peru, 0, 0, Width, Height);
            g.DrawImage(image, 0, 0, BaseWidth, BaseHeight);
        }
        public override void Update(long delta)
        {
            base.Update(delta);
            Velocity += World.Acceleration * (delta / 1000f);
            float prev = Location.Y;
            Location += Velocity * (delta / 1000f);
        }

        public float Mass
        {
            get;
            set;
        }

        //not sure if I can do this?
        public void setParent(PinballMachine pM)
        {
            this.World = pM;
        }

    }
}
