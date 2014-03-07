using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using Sketchball.Elements;

namespace Sketchball.Collision
{
    public class BoundingContainer
    {

        public List<IBoundingBox> boundingBoxes { get; private set; }

        public BoundingContainer()
        {
            this.boundingBoxes = new List<IBoundingBox>();
        }

        public List<IBoundingBox> getBoundingBoxes()
        {
            return this.boundingBoxes;
        }

        public void rotate(float degree, Vector2 center)
        {
            foreach (IBoundingBox b in this.boundingBoxes)
            {
                b.rotate(degree, center);
            }

        }

        public void move(Vector2 moveVec)
        {
            foreach (IBoundingBox b in this.boundingBoxes)
            {
                b.move(moveVec);
            }
        }

        public void addBoundingBox(IBoundingBox bL)
        {
            this.boundingBoxes.Add(bL);
        }



    }
}
