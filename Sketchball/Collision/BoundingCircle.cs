using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Collision
{
    class BoundingCircle : BoundingBox
    {
        public int radius{get; private set;}

        public BoundingCircle(int radius, Vector2 center)
        {
            this.radius = radius;
            this.position = center;
        }

        public override bool intersec(IBoundingBox bB)
        {
            return bB.circleIntersec(this);
        }

        public override Vector2 reflect(Vector2 vecIn)
        {
            throw new NotImplementedException();
        }

        public override void rotate(float degree, Vector2 center)
        {
  
            Matrix rotation = new Matrix();
            System.Drawing.PointF ptCenter = new System.Drawing.PointF(center.X, center.Y);
            rotation.RotateAt(degree, ptCenter);

            rotation.TransformVectors(this.position);       //TODO
          
        }

        public override bool lineIntersec(BoundingLine bL)
        {
            //Strategy: determine point of intersection between the directionline
            //and the line thorugh center of circ. which is normal to the directionline
            //then determine the distance between that point (T) and center of Circle
            //if smaller than radius there is an intersection
            Vector2 centerOfCircle = this.position;
            Vector2 directionLine = bL.target - bL.position;
            Vector2 normalLine = new Vector2(-directionLine.Y, directionLine.X);

            //direction line as infinte line
            float A1 = bL.target.Y - bL.position.Y;
            float B1 = bL.target.X - bL.position.X;
            float C1 = A1 * bL.position.X + B1 * bL.position.Y;

            //line though the center of circle and normal to the direction line
            float A2 = normalLine.Y - centerOfCircle.Y;
            float B2 = normalLine.X - centerOfCircle.X;
            float C2 = A2 * centerOfCircle.X + B2 * centerOfCircle.Y;

            float det = A1 * B1 - A2 * B2;      //determines same delta?

            //set equation equal
            float x = (B2 * C1 - B1 * C2) / det;
            float y = (A1 * C2 - A2 * C1) / det;

            Vector2 T = new Vector2(x, y);      //point on direction line where normal from center of cir. hits

            float diff = Vector2.Distance(centerOfCircle, T);


            if (diff < this.radius)
            {
                //in this case T lies in the circle
                return true;
            }
            return false;
        }

        public override bool circleIntersec(BoundingCircle bC)
        {
            if (Vector2.Distance(bC.position, this.position) < (this.radius + bC.radius))       //TODO Distance => can be negativ? check!
            {
                return true;
            }
            return false;
        }

    }
}
