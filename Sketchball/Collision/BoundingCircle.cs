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

        public override Vector2 reflect(Vector2 vecIn, Vector2 hitPoint, Vector2 ballpos)
        {
            //circle => position = origin of object space coordinate system
            //=> normal to make reflection is from origin to hitpoint (hitpoint must be conferted to object space first)
            Vector2 normal = Vector2.Normalize(hitPoint-(this.BoundingContainer.parentElement.getLocation() + new Vector2(this.radius,this.radius)));
            return Vector2.Reflect(vecIn, normal);
        }

        public override Vector2 getOutOfAreaPush(int diameterBall, Vector2 hitPoint, Vector2 velocity, Vector2 ballPos)
        {
            return (diameterBall / 1.9f) * Vector2.Normalize(hitPoint - (this.BoundingContainer.parentElement.getLocation() + new Vector2(this.radius, this.radius)));
        }

        public override void rotate(float degree, Vector2 center)
        {
            //ignore (as position is allways 0 in object space and parent position allready rotated         
        }

        public override bool lineIntersec(BoundingLine bL, out Vector2 hitPoint)
        {
            //strategy: connect center of ball with start of line. calc where the normal from center of ball on line hits (pointNormalDirectionPice). If len from center of ball to this point
            //is smaller then radius then it is a hit. Should pointNormalDirectionPice be smaller then start - radius of ball or bigger then end+ radius of ball => ignore

            hitPoint = new Vector2(0, 0);

            Vector2 bLWorldPos = bL.position + bL.BoundingContainer.parentElement.getLocation();
            Vector2 bLWorldTar = bL.target + bL.BoundingContainer.parentElement.getLocation();
            Vector2 thisWorldPos = this.position + this.BoundingContainer.parentElement.getLocation() ;
            
            Vector2 centerOfCircle = thisWorldPos;
            Vector2 directionLine = bLWorldTar - bLWorldPos;
            Vector2 normalLine = new Vector2(-directionLine.Y, directionLine.X);

            float lenDirectionPiece = Vector2.Dot((centerOfCircle - bLWorldPos) , Vector2.Normalize(directionLine));

            if (lenDirectionPiece < -this.radius || lenDirectionPiece > (directionLine.Length()+this.radius))
            {
                return false;
            }

            Vector2 pointNormalDirectionPice = bLWorldPos+ lenDirectionPiece * Vector2.Normalize(directionLine);
            Vector2 normalFromDirLineToCenter = centerOfCircle - pointNormalDirectionPice;

            float diff = normalFromDirLineToCenter.Length();
           
            if (diff < this.radius)
            {
                if (lenDirectionPiece < 0)
                {
                    hitPoint = bLWorldPos;
                    return true;
                }

                if (lenDirectionPiece > (directionLine.Length()))
                {
                    hitPoint = bLWorldTar;
                    return true;
                }
                //in this case T lies in the circle
                hitPoint = pointNormalDirectionPice;
                return true;
            }
          
            return false;
           
        }


        public override bool circleIntersec(BoundingCircle bC, out Vector2 hitPoint)
        {
            
            Vector2 thisWorldTras = this.BoundingContainer.parentElement.getLocation();
            Vector2 bCWorldTrans = bC.BoundingContainer.parentElement.getLocation();

            if (Vector2.Distance(bC.position + bCWorldTrans, this.position + thisWorldTras) < (this.radius + bC.radius))    
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
            
        }
    }
}
