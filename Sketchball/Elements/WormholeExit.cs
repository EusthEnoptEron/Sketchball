using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class WormholeExit : PinballElement
    {
         public WormholeExit() : base()
        {
            Width = 30;
            Height = 30;
            this.pureIntersection = true;

            this.setLocation(new Vector2(0, 100));

        }

        protected override void InitBounds()
        {
            BoundingCircle bC = new BoundingCircle(15, new Vector2(0, 0));
            this.boundingContainer.addBoundingBox(bC);
            bC.assigneToContainer(this.boundingContainer);
        }

        protected override void OnDraw(System.Drawing.Graphics g)
        {
            g.DrawImage(Properties.Resources.WormholeExit, 0, 0, Width, Height);
        }
    }
}
