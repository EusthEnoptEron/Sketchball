using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    class Bouncer : PinballElement
    {
        public Bouncer():base()
        {
            Width = 100;
            Height = 100;
            
        
            this.setLocation(new Vector2(300, 300));
            

            //set up of bounding box
            BoundingCircle bC = new BoundingCircle(50, new Vector2(0, 0));
            this.boundingContainer.addBoundingBox(bC);
 
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            
        }
    }
}
