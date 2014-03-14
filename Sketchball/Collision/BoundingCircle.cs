using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Collision
{
    public class BoundingCircle : BoundingBox
    {
        public int radius{get; private set;}
        private Vector2 tmp = new Vector2(0, 0);
      
        /// <summary>
        /// creates new Bounding Circle
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="center">position based on pinballElement</param>
        public BoundingCircle(int radius, Vector2 center)
        {
            this.radius = radius;
            this.position = center+new Vector2(radius,radius);
        }

        public override bool intersec(IBoundingBox bB,out Vector2 hitPoint)
        {
            return bB.circleIntersec(this,out hitPoint);
        }

        public override Vector2 reflect(Vector2 vecIn, Vector2 hitPoint)
        {
            //circle => position = origin of object space coordinate system
            //=> normal to make reflection is from origin to hitpoint (hitpoint must be conferted to object space first)
            Vector2 normal = Vector2.Normalize(hitPoint-(this.position -new Vector2(this.radius,this.radius)));
    //TODO correct
            return Vector2.Reflect(vecIn, normal);
        }

        public override void rotate(float degree, Vector2 center)
        {
            //ignore (as position is allways 0 in object space and parent position allready rotated         
        }

        public override bool lineIntersec(BoundingLine bL, out Vector2 hitPoint)
        {
            //Strategy: determine point of intersection between the directionline
            //and the line thorugh center of circ. which is normal to the directionline
            //then determine the distance between that point (T) and center of Circle
            //if smaller than radius there is an intersection

            Vector2 bLWorldPos = bL.position + bL.BoundingContainer.parentElement.getLocation();
            Vector2 bLWorldTar = bL.target + bL.BoundingContainer.parentElement.getLocation();
            Vector2 thisWorldPos = this.position + this.BoundingContainer.parentElement.getLocation() - new Vector2(this.radius, this.radius);


            Vector2 centerOfCircle = thisWorldPos;
            Vector2 directionLine = bLWorldTar - bLWorldPos;
            Vector2 normalLine = new Vector2(-directionLine.Y, directionLine.X);

            //direction line as infinte line
            float A1 = bLWorldTar.Y - bLWorldPos.Y;
            float B1 = bLWorldTar.X - bLWorldPos.X;
            float C1 = A1 * bLWorldPos.X + B1 * bLWorldPos.Y;

            //line though the center of circle and normal to the direction line
            float A2 = normalLine.Y - centerOfCircle.Y;
            float B2 = normalLine.X - centerOfCircle.X;
            float C2 = A2 * centerOfCircle.X + B2 * centerOfCircle.Y;

            float det = A1 * B1 - A2 * B2;      //determines same delta?

            //set equation equal
            float x = (B2 * C1 - B1 * C2) / det;
            float y = (A1 * C2 - A2 * C1) / det;

            Vector2 T = new Vector2(x, y);      //point on direction line where normal from center of cir. hits
            this.tmp = T;

            float diff = Vector2.Distance(centerOfCircle, T);
           
           
          
            if (diff < this.radius)
            {
                //in this case T lies in the circle
                hitPoint = T;
                return true;
            }
            hitPoint = new Vector2(0,0);
            return false;
        }

        public override bool circleIntersec(BoundingCircle bC, out Vector2 hitPoint)
        {
            Vector2 thisWorldTras = this.BoundingContainer.parentElement.getLocation();
            Vector2 bCWorldTrans = bC.BoundingContainer.parentElement.getLocation();

            if (Vector2.Distance(bC.position + bCWorldTrans, this.position + thisWorldTras) < (this.radius + bC.radius))       //TODO Distance => can be negativ? check!
            {
                hitPoint = bC.position + bCWorldTrans + Vector2.Normalize(-(bC.position + bCWorldTrans) + (this.position + thisWorldTras)) * bC.radius;
                return true;
            }
            hitPoint = new Vector2(0, 0);
            return false;
        }


        public override void drawDEBUG(System.Drawing.Graphics g, Pen p)
        {
            Vector2 pos = this.position+this.BoundingContainer.parentElement.getLocation()-new Vector2(this.radius,this.radius);

            g.DrawEllipse(p, (int)pos.X, (int)pos.Y, (int)(this.radius * 2), ((int)this.radius * 2));
            g.DrawEllipse(p, (int)this.tmp.X, (int)this.tmp.X, (int)(5), ((int)5));
        }
    }
}
