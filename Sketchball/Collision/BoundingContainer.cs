using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Sketchball.Collision
{
    public class BoundingContainer
    {
        //Vertex are vectors with the position of the pinballelement as center
        public HashSet<Vector2> vertices { get; private set; }
        public List<IBoundingBox> boundingBoxes { get; private set; }

        public BoundingContainer()
        {
            this.vertices = new HashSet<Vector2>();
            this.boundingBoxes = new List<IBoundingBox>();
        }

        public void rotate(float degree, Vector2 center)
        {
            foreach (IBoundingBox b in this.boundingBoxes)
            {
                b.rotate(degree, center);
            }

            Matrix rotation = new Matrix();
            System.Drawing.PointF ptCenter = new System.Drawing.PointF(center.X, center.Y);
            rotation.RotateAt(degree, ptCenter);

            foreach (Vector2 v in this.vertices)
            {
               rotation.        //TODO Problem
            }

        }

        public void move(Vector2 moveVec)
        {
            foreach (IBoundingBox b in this.boundingBoxes)
            {
                b.move(moveVec);
            }

            foreach (Vector2 v in this.vertices)
            {
                v += moveVec;
            }
        }

        public void addBoundingBox(BoundingLine bL)
        {
            //Vertex are vectors with the position of the pinballelement as center
            this.vertices.Add(bL.position);
            this.vertices.Add(bL.target);

            this.boundingBoxes.Add(bL);
        }

        public void addBoundingBox(BoundingCircle bS)
        {
            //Vertex are vectors with the position of the pinballelement as center
            this.vertices.Add(bS.position);
            this.boundingBoxes.Add(bS);
        }

    }
}
