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
            

            this.setLocation(new Vector2(0, 100));

        }

        protected override void InitBounds()
        {
            BoundingCircle bC = new BoundingCircle(15, new Vector2(0, 0));
            this.boundingContainer.addBoundingBox(bC);
            bC.assigneToContainer(this.boundingContainer);
            this.pureIntersection = true;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.DrawImage(Booster.OptimizeImage(Properties.Resources.WormholeExit,Width,Height), 0, 0, Width, Height);
        }

        


    }
}
