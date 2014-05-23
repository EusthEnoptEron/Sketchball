using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class WormholeExit : PinballElement
    {
        private static Image image = Booster.OptimizeImage(Properties.Resources.WormholeExit, 50);
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

            BoundingCircle bC = new BoundingCircle(15, new Vector2(0, 0));
            this.boundingContainer.addBoundingBox(bC);
            bC.assigneToContainer(this.boundingContainer);
            this.pureIntersection = true;
        }

        protected override void OnDraw(System.Drawing.Graphics g)
        {
            g.DrawImage(image, 0, 0, BaseWidth, BaseHeight);
        }

     
    }
}
