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
    public class WormholeExit : PinballElement
    {
        private static ImageSource imageS = Booster.OptimizeWpfImage("WormholeExit.png");

        private static readonly Size size = new Size(30, 30);

        protected override Size BaseSize
        {
            get { return size; }
        }


        public WormholeExit()
        {
        }

        protected override void Init()
        {
            this.pureIntersection = true;

            BoundingCircle bC = new BoundingCircle(15, new Vector(0, 0));
            this.boundingContainer.AddBoundingBox(bC);
            bC.AssignToContainer(this.boundingContainer);
            this.pureIntersection = true;
        }



        protected override void OnDraw(DrawingContext g)
        {
            g.DrawImage(imageS, new Rect(0, 0, BaseWidth, BaseHeight));
        }
    }
}
