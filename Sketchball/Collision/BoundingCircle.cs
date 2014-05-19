using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Collision
{
    /// <summary>
    /// Circle variant
    /// </summary>
    public class BoundingCircle : BoundingBox
    {
        public int radius{get; private set;}

        private int _originalRadius;
        private Vector2 _originalPosition;

        /// <summary>
        /// Creates new bounding circle
        /// </summary>
        /// <param name="radius">Radius of the bounding circle</param>
        /// <param name="position">position based on pinballElement</param>
        public BoundingCircle(int radius, Vector2 position)
        {
            this.radius = radius;
            this.position = position+new Vector2(radius,radius);

            _originalPosition = this.position;
            _originalRadius = radius;
        }

        public override bool intersec(IBoundingBox bB,out Vector2 hitPoint, Vector2 velocity )
        {
            return bB.circleIntersec(this,out hitPoint,velocity);
        }

        public override bool intersec(IBoundingBox bB,out Vector2 hitPoint)
        {
            return bB.circleIntersec(this,out hitPoint, new Vector2(0,0));
        }

        public override Vector2 reflect(Vector2 vecIn, Vector2 hitPoint, Vector2 ballpos)
        {
            //circle => position = origin of object space coordinate system
            //=> normal to make reflection is from origin to hitpoint (hitpoint must be conferted to object space first)
            //TODO take position of bounding box into account
           
            Vector2 normal = Vector2.Normalize(hitPoint-(this.BoundingContainer.parentElement.getLocation() +this.position));
            return Vector2.Reflect(vecIn, normal);
        }

        public override Vector2 getOutOfAreaPush(int diameterBall, Vector2 hitPoint, Vector2 velocity, Vector2 ballPos)
        {
            //TODO take bounding box position into account
            return (diameterBall / 1.9f) * Vector2.Normalize(hitPoint - (this.BoundingContainer.parentElement.getLocation() + this.position));
        }

        public override void rotate(float rad, Vector2 center)
        {
            Matrix rotation = new Matrix();
            System.Drawing.PointF ptCenter = new System.Drawing.PointF(center.X, center.Y);
            rotation.RotateAt((float)(rad / Math.PI * 180f), ptCenter);

            System.Drawing.PointF[] pts = new System.Drawing.PointF[2];
            Vector2 p1 = this.position;
            pts[0].X = p1.X;
            pts[0].Y = p1.Y;


            rotation.TransformPoints(pts);
            p1.X = pts[0].X;
            p1.Y = pts[0].Y;
          
            this.position = p1;
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
           // Console.WriteLine(bL.position+" "+bL.target+" "+lenDirectionPiece);
            if (lenDirectionPiece < -this.radius || lenDirectionPiece >= (directionLine.Length()+this.radius))
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

        public override bool circleIntersec(BoundingCircle bC, out Vector2 hitPoint, Vector2 velocity)
        {
            
            Vector2 thisWorldTras = this.BoundingContainer.parentElement.getLocation();
            Vector2 bCWorldTrans = bC.BoundingContainer.parentElement.getLocation();

            if (Vector2.Distance(bC.position + bCWorldTrans, this.position + thisWorldTras) < (this.radius + bC.radius))    
            {
                Vector2 direction = (-(bC.position + bCWorldTrans) + (this.position + thisWorldTras)); ;
                if (velocity != new Vector2(0, 0))
                {
                    if (direction.X == 0 && direction.Y == 0)
                    {
                        direction = -velocity;
                    }
                    else if (direction.X * velocity.X >= 0 && direction.Y* velocity.Y >= 0)
                    {
                        //point in the same direction => reverse direction
                        direction = -direction;
                    }
                    
                }
                if (direction.X == 0 && direction.Y == 0)
                {
                    direction.X = 0;
                    direction.Y = -1;
                }
                direction = Vector2.Normalize(direction);
                //hitPoint = bC.position + bCWorldTrans + Vector2.Normalize(-(bC.position + bCWorldTrans) + (this.position + thisWorldTras)) * bC.radius
                hitPoint = bC.position + bCWorldTrans + direction * bC.radius;
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

        public override IBoundingBox Clone()
        {
            //do not forget to assinge BoundingContainer after clone
            BoundingCircle bL = new BoundingCircle(this.radius, new Vector2(this.position.X - this.radius, this.position.Y - this.radius));
            return bL;
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle((int)(position.X - radius + BoundingContainer.parentElement.X), 
                                 (int)(position.Y - radius + BoundingContainer.parentElement.Y), 
                                 radius*2, radius*2);
        }

        public override void clearRotation()
        {
            this.position = _originalPosition;
        }

        public override void Sync(Matrix matrix)
        {
            PointF[] points = new PointF[] { new PointF(_originalPosition.X, _originalPosition.Y) };
            matrix.TransformPoints(points);
            position = new Vector2(points[0].X, points[0].Y);


            points = new PointF[] { new PointF( _originalRadius, 0 ) };
            matrix.TransformVectors(points);
            radius = (int)new Vector2(points[0].X, points[0].Y).Length();
        }
    }
}
