using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class Bumper : PinballElement
    {
        public Bumper():base()
        {
            Width = 200;
            Height = 200;
            
        
            this.setLocation(new Vector2(300, 300));
 
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.DrawImage(Properties.Resources.Bumper, 0, 0, Width, Height);
        }

        protected override void InitBounds()
        {
            BoundingCircle bC = new BoundingCircle(Width / 2, new Vector2(0, 0));
            this.boundingContainer.addBoundingBox(bC);
            bC.assigneToContainer(this.boundingContainer);
        }
    }
}
