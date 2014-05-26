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
       
        protected override Size BaseSize
        {
            get { return Size; }
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

        protected override void OnDraw(DrawingContext g)
        {
            g.DrawImage(imageS, new Rect(0, 0, BaseWidth, BaseHeight));
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
