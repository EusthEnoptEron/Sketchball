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
        //target is object space: based on pinball element position
        public Vector2 target{get; private set;}

        public BoundingLine(Vector2 from, Vector2 target)
        {
            this.position = from;
            this.target = target;
        }

        public override bool intersec(IBoundingBox bB, out Vector2 hitPoint)
        {
            return bB.lineIntersec(this, out hitPoint);
        }

        public override Vector2 reflect(Vector2 vecIn, Vector2 hitPointP)
        {

            Vector2 dLine = this.target - this.position;
            Vector2 normal = new Vector2(-dLine.Y,dLine.X);     //TODO build so that allways the right normalvector is chosen
            normal.Normalize();

            return Vector2.Reflect(vecIn, normal);
        }

        public new void move(Vector2 moveVec)
        {
            base.move(moveVec);
            this.target += moveVec;
        }


        public override bool lineIntersec(BoundingLine bL, out Vector2 hitPoint)
        {
            Vector2 thisWorldTras = this.BoundingContainer.parentElement.getLocation();
            Vector2 bLWorldTrans = bL.BoundingContainer.parentElement.getLocation();

            Vector2 bLWorldPos = bL.position + bLWorldTrans;
            Vector2 bLWorldTar = bL.target + bLWorldTrans;
            Vector2 thisWorldTar = this.target + thisWorldTras;
            Vector2 thisWorldPos = this.position + thisWorldTras;


            float A1 = thisWorldTar.Y - thisWorldPos.Y;
            float B1 = thisWorldTar.X - thisWorldPos.X;
            float C1 = A1 * thisWorldPos.X + B1 * thisWorldPos.Y;

            float A2 = bLWorldTar.Y - bLWorldPos.Y;
            float B2 = bLWorldTar.X - bLWorldPos.X;
            float C2 = A2 * bLWorldPos.X + B2 * bLWorldPos.Y;

            float det = A1 * B1 - A2 * B2;      //determines same delta?

            if (det == 0)       //parallel
            {
                hitPoint = new Vector2(0,0);
                return false;
            }
            else
            {
                //set equation equal
                float x = (B2*C1 - B1*C2)/det;
                float y = (A1 * C2 - A2 * C1) / det;

                if (min(thisWorldPos.X, thisWorldTar.X) < x && x < max(thisWorldPos.X, thisWorldTar.X))
                {
                    if (min(thisWorldPos.Y, thisWorldTar.Y) < y && y < max(thisWorldPos.Y, thisWorldTar.Y))
                    {
                        hitPoint = new Vector2(x, y);
                        return true;
                    }
                }
                hitPoint = new Vector2(0, 0);
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

            //rotation.TransformVectors(this.position);       //TODO
            //rotation.TransformVectors(this.target);       //TODO also has to be moved to center, rotate then move back, since based on position
           
        }

        public override bool circleIntersec(BoundingCircle bC, out Vector2 hitPoint)
        {
            //Strategy: determine point of intersection between the directionline
            //and the line thorugh center of circ. which is normal to the directionline
            //then determine the distance between that point (T) and center of Circle
            //if smaller than radius there is an intersection
            Vector2 thisWorldTras = this.BoundingContainer.parentElement.getLocation();
            Vector2 bCWorldTrans = bC.BoundingContainer.parentElement.getLocation();

            Vector2 thisWorldTar = this.target + thisWorldTras;
            Vector2 thisWorldPos = this.position + thisWorldTras;
            Vector2 bCWorldPos = this.position + thisWorldTras;

            Vector2 centerOfCircle = bC.position + bCWorldTrans;
            Vector2 directionLine = this.target - this.position;
            Vector2 normalLine = new Vector2(-directionLine.Y, directionLine.X);
            
            //direction line as infinte line
            float A1 = thisWorldTar.Y - thisWorldPos.Y;
            float B1 = thisWorldTar.X - thisWorldPos.X;
            float C1 = A1 * thisWorldPos.X + B1 * thisWorldPos.Y;

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

                hitPoint = T;
                return true;
            }
            hitPoint = new Vector2(0, 0);
            return false;
        }

        public override void drawDEBUG(System.Drawing.Graphics g, System.Drawing.Pen p)
        {
            Vector2 pos = this.BoundingContainer.parentElement.getLocation();
            g.DrawLine(p, (int)(this.position.X + pos.X), (int)(this.position.Y + pos.Y), (int)(this.target.X + pos.X), (int)(this.target.Y + pos.Y));
        }
    }
}
