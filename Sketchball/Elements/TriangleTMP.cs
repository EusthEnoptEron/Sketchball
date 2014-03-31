using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    class TriangleTMP: PinballElement
    {
        public TriangleTMP()
        {
            Width = 200;
            Height = 200;


            this.setLocation(new Vector2(0, 100));
            this.bounceFactor = 0.9f;

            //set up of bounding box
            BoundingLine bL1 = new BoundingLine(new Vector2(0, 200), new Vector2(100, 0));
            BoundingLine bL2 = new BoundingLine(new Vector2(100, 0), new Vector2(200, 200));
            BoundingLine bL3 = new BoundingLine(new Vector2(200, 200), new Vector2(0, 200));

            this.boundingContainer.addBoundingBox(bL1);
            this.boundingContainer.addBoundingBox(bL2);
            this.boundingContainer.addBoundingBox(bL3);

            bL1.assigneToContainer(this.boundingContainer);
            bL2.assigneToContainer(this.boundingContainer);
            bL3.assigneToContainer(this.boundingContainer);
        }

        public override void Draw(System.Drawing.Graphics g)
        {

        }

        public override void Update(long delta)
        {
           // this.boundingContainer.rotate(0.5f, new Vector2(100, 100));
        }
    }
}
