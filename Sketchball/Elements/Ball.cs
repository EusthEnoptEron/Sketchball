using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sketchball.Elements
{
    public class Ball : PinballElement
    {
        public static readonly Size Size = new Size(20, 20);
        private static System.Windows.Media.ImageSource imageS = Booster.OptimizeWpfImage("BallWithAlpha.png");
        private readonly float friction = 0.9999f;

        protected override Size BaseSize
        {
            get { return Size; }
        }
       

        public Vector Velocity
        {
            get;
            set;
        }

        public Ball() : base()
        {
            Velocity = new Vector(0,0);
        }

        protected override void Init()
        {
            BoundingCircle bC = new BoundingCircle(10, new Vector(0, 0));
            this.boundingContainer.AddBoundingBox(bC);
        }

        protected override void OnDraw(DrawingContext g)
        {
            g.DrawImage(imageS, new Rect(0, 0, BaseWidth, BaseHeight));
        }

        public override void Update(long delta)
        {
            base.Update(delta);
            Velocity += World.Acceleration * (delta / 1000f);
            Velocity = new Vector(Velocity.X * (this.friction - 0.00000001f * Velocity.X * Velocity.X), Velocity.Y * (this.friction - 0.00000001f * Velocity.Y * Velocity.Y));

            double prev = Location.Y;
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
