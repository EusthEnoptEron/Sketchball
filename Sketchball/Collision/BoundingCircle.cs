using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
            rotation.RotateAt((rad / Math.PI * 180f), center.X, center.Y);

            Point[] pts = new Point[2];
            Vector2 p1 = this.position;
            pts[0].X = p1.X;
            pts[0].Y = p1.Y;


            rotation.Transform(pts);
            p1.X = (float)pts[0].X;
            p1.Y = (float)pts[0].Y;
          
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

        public override void drawDEBUG(System.Windows.Media.DrawingContext g, System.Windows.Media.Pen pen)
        {
            Vector2 pos = this.position+this.BoundingContainer.parentElement.getLocation();

            g.DrawEllipse(null, pen, new System.Windows.Point((int)pos.X, (int)pos.Y ), (int)(this.radius), ((int)this.radius));
        }


        public override IBoundingBox Clone()
        {
            //do not forget to assinge BoundingContainer after clone
            BoundingCircle bL = new BoundingCircle(this.radius, new Vector2(this.position.X - this.radius, this.position.Y - this.radius));
            return bL;
        }

        public override void clearRotation()
        {
            this.position = _originalPosition;
        }

        public override void Sync(Matrix matrix)
        {
            Point[] points = new Point[] { new Point(_originalPosition.X, _originalPosition.Y) };
            matrix.Transform(points);
            position = new Vector2((float)points[0].X, (float)points[0].Y);


            var vectors = new Vector[] { new Vector(_originalRadius, 0) };
            matrix.Transform(vectors);
            radius = (int)new Vector2((float)vectors[0].X, (float)vectors[0].Y).Length();
        }


    }
}
