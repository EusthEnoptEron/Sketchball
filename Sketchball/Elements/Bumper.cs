﻿using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    class Bumper : PinballElement
    {
        public Bumper():base()
        {
            Width = 200;
            Height = 200;
            
        
            this.setLocation(new Vector2(300, 300));
            

            //set up of bounding box
            BoundingCircle bC = new BoundingCircle(100, new Vector2(0, 0));
            this.boundingContainer.addBoundingBox(bC);
            bC.assigneToContainer(this.boundingContainer);
 
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            
        }
    }
}