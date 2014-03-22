using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class Line : PinballElement
    {
        public Line() : base()
        {
            Width = 100;
            Height = 0;
            

            this.setLocation(new Vector2(100, 200));
            

            //set up of bounding box
            BoundingLine bL = new BoundingLine(new Vector2(0,0),  new Vector2(this.Width, this.Height));
            this.boundingContainer.addBoundingBox(bL);

        }

        public override void Draw(System.Drawing.Graphics g)
        {

        }
    }
}
