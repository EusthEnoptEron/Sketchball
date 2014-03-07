using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Collision
{
    public class BoundingLine : BoundingBox
    {
        public Vector2 target{get; private set;}

        public BoundingLine(Vector2 target)
        {
            this.target = target;
        }

        public bool intersec(IBoundingBox bB)
        {
            return bB.lineIntersec(this);
        }

        public Vector2 reflect(Vector2 vecIn)
        {
            Vector2 dLine = this.target - this.position;
            Vector2 normal = new Vector2(-dLine.Y,dLine.X);     //TODO build so that allways the right normalvector is chosen
            normal.Normalize();

            return Vector2.Reflect(vecIn, normal);
        }

        public override void move(Vector2 moveVec)
        {
            base.move(moveVec);
            this.target += moveVec;
        }

  
        public override bool lineIntersec(BoundingLine bL)
        {
            float A1 = this.target.Y - this.position.Y;
            float B1 = this.target.X - this.position.X;
            float C1 = A1 * this.position.X + B1 * this.position.Y;

            float A2 = bL.target.Y - bL.position.Y;
            float B2 = bL.target.X - bL.position.X;
            float C2 = A2 * bL.position.X + B2 * bL.position.Y;

            float det = A1 * B1 - A2 * B2;      //determines same delta?

            if (det == 0)       //parallel
            {
                return false;
            }
            else
            {
                //set equation equal
                float x = (B2*C1 - B1*C2)/det;
                float y = (A1 * C2 - A2 * C1) / det;

                if (min(this.position.X, this.target.X) < x&&x < max(this.position.X, this.target.X))
                {
                    if (min(this.position.Y, this.target.Y) < y && y < max(this.position.Y, this.target.Y))
                    {
                        //in segment
                        return true;
                    }
                }
                return false;
            }
        }

        private float min(float t1, float t2)
        {
            if (t1 < t2)
            {
                return t1;
            }
            return t2;
        }

        private float max(float t1, float t2)
        {
            if (t1 > t2)
            {
                return t1;
            }
            return t2;
        }

        public override void rotate(float degree, Vector2 center)
        {
            Matrix rotation = new Matrix();
            System.Drawing.PointF ptCenter = new System.Drawing.PointF(center.X, center.Y);
            rotation.RotateAt(degree, ptCenter);

            rotation.TransformVectors(this.position);       //TODO
            rotation.TransformVectors(this.target);       //TODO also has to be moved to center, rotate then move back, since based on position
           
        }

        public override bool circleIntersec(BoundingCircle bC)
        {
            //Strategy: determine point of intersection between the directionline
            //and the line thorugh center of circ. which is normal to the directionline
            //then determine the distance between that point (T) and center of Circle
            //if smaller than radius there is an intersection
            Vector2 centerOfCircle = bC.position;
            Vector2 directionLine = this.target - this.position;
            Vector2 normalLine = new Vector2(-directionLine.Y, directionLine.X);
            
            //direction line as infinte line
            float A1 = this.target.Y - this.position.Y;
            float B1 = this.target.X - this.position.X;
            float C1 = A1 * this.position.X + B1 * this.position.Y;

            //line though the center of circle and normal to the direction line
            float A2 = normalLine.Y - centerOfCircle.Y;
            float B2 = normalLine.X - centerOfCircle.X;
            float C2 = A2 * centerOfCircle.X + B2 * centerOfCircle.Y;

            float det = A1 * B1 - A2 * B2;      //determines same delta?

            //set equation equal
            float x = (B2 * C1 - B1 * C2) / det;
            float y = (A1 * C2 - A2 * C1) / det;

            Vector2 T = new Vector2(x, y);      //point on direction line where normal from center of cir. hits

            float diff = Vector2.Distance(centerOfCircle,T);    

            
            if (diff < bC.radius)
            {
                //in this case T lies in the circle
                return true;
            }
            return false;
        }
    }
}
