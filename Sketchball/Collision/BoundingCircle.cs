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
        private Vector _originalPosition;

        /// <summary>
        /// Creates new bounding circle
        /// </summary>
        /// <param name="radius">Radius of the bounding circle</param>
        /// <param name="position">position based on pinballElement</param>
        public BoundingCircle(int radius, Vector position)
        {
            this.radius = radius;
            this.Position = position+new Vector(radius,radius);

            _originalPosition = this.Position;
            _originalRadius = radius;
        }

        public override bool Intersect(IBoundingBox bB,out Vector hitPoint, Vector velocity )
        {
            return bB.CircleIntersect(this,out hitPoint,velocity);
        }

        public override bool Intersect(IBoundingBox bB,out Vector hitPoint)
        {
            return bB.CircleIntersect(this,out hitPoint, new Vector(0,0));
        }

        public override Vector Reflect(Vector vecIn, Vector hitPoint, Vector ballpos)
        {
            //circle => position = origin of object space coordinate system
            //=> normal to make reflection is from origin to hitpoint (hitpoint must be conferted to object space first)
            //TODO take position of bounding box into account
           
            Vector normal = hitPoint-(this.BoundingContainer.ParentElement.Location +this.Position);
            if (normal.X == 0 && normal.Y == 0) normal.Y = -1;
            normal.Normalize();
            return ReflectVector(ref vecIn, ref normal);
        }

        public override Vector GetOutOfAreaPush(int diameterBall, Vector hitPoint, Vector velocity, Vector ballPos)
        {
            //TODO take bounding box position into account
            var vector = hitPoint - (this.BoundingContainer.ParentElement.Location + this.Position);
            vector.Normalize();
            return (diameterBall / 1.9f) * vector;
        }

        public override void Rotate(double rad, Vector center)
        {
            Matrix rotation = new Matrix();
            rotation.RotateAt((rad / Math.PI * 180f), center.X, center.Y);

            Point[] pts = new Point[2];
            Vector p1 = this.Position;
            pts[0].X = p1.X;
            pts[0].Y = p1.Y;


            rotation.Transform(pts);
            p1.X = pts[0].X;
            p1.Y = pts[0].Y;
          
            this.Position = p1;
        }

        public override bool LineIntersect(BoundingLine bL, out Vector hitPoint)
        {
            //strategy: connect center of ball with start of line. calc where the normal from center of ball on line hits (pointNormalDirectionPice). If len from center of ball to this point
            //is smaller then radius then it is a hit. Should pointNormalDirectionPice be smaller then start - radius of ball or bigger then end+ radius of ball => ignore

            hitPoint = new Vector(0, 0);

            Vector bLWorldPos = bL.Position + bL.BoundingContainer.ParentElement.Location;
            Vector bLWorldTar = bL.target + bL.BoundingContainer.ParentElement.Location;
            Vector thisWorldPos = this.Position + this.BoundingContainer.ParentElement.Location ;
            
            Vector centerOfCircle = thisWorldPos;
            Vector directionLine = bLWorldTar - bLWorldPos;
            Vector normalLine = new Vector(-directionLine.Y, directionLine.X);

            double lenDirectionPiece = Vector.Multiply((centerOfCircle - bLWorldPos) , NormalizeVector(directionLine));
           // Console.WriteLine(bL.position+" "+bL.target+" "+lenDirectionPiece);
            if (lenDirectionPiece < -this.radius || lenDirectionPiece >= (directionLine.Length+this.radius))
            {
                return false;
            }

            Vector pointNormalDirectionPice = bLWorldPos + lenDirectionPiece * NormalizeVector(directionLine);
            Vector normalFromDirLineToCenter = centerOfCircle - pointNormalDirectionPice;

            double diff = normalFromDirLineToCenter.Length;
           
            if (diff < this.radius)
            {
                if (lenDirectionPiece < 0)
                {
                    hitPoint = bLWorldPos;
                    return true;
                }

                if (lenDirectionPiece > (directionLine.Length))
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

        public override bool CircleIntersect(BoundingCircle bC, out Vector hitPoint, Vector velocity)
        {
            
            Vector thisWorldTras = this.BoundingContainer.ParentElement.Location;
            Vector bCWorldTrans = bC.BoundingContainer.ParentElement.Location;

            if (VectorDistance(bC.Position + bCWorldTrans, this.Position + thisWorldTras) < (this.radius + bC.radius))    
            {
                Vector direction = (-(bC.Position + bCWorldTrans) + (this.Position + thisWorldTras)); ;
                if (velocity != new Vector(0, 0))
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
                direction = NormalizeVector(direction);
                //hitPoint = bC.position + bCWorldTrans + Vector.Normalize(-(bC.position + bCWorldTrans) + (this.position + thisWorldTras)) * bC.radius
                hitPoint = bC.Position + bCWorldTrans + direction * bC.radius;
                return true;
            }

            hitPoint = new Vector(0, 0);
            return false;
        }

        public override void DrawDebug(System.Windows.Media.DrawingContext g, System.Windows.Media.Pen pen)
        {
            Vector pos = this.Position+this.BoundingContainer.ParentElement.Location;

            g.DrawEllipse(null, pen, new System.Windows.Point(pos.X, pos.Y ), (this.radius), (this.radius));
        }


        public override IBoundingBox Clone()
        {
            //do not forget to assinge BoundingContainer after clone
            BoundingCircle bL = new BoundingCircle(this.radius, new Vector(this.Position.X - this.radius, this.Position.Y - this.radius));
            return bL;
        }


        public override void Sync(Matrix matrix)
        {
            Point[] points = new Point[] { new Point(_originalPosition.X, _originalPosition.Y) };
            matrix.Transform(points);
            Position = new Vector(points[0].X, points[0].Y);


            var vectors = new Vector[] { new Vector(_originalRadius, 0) };
            matrix.Transform(vectors);
            radius = (int)new Vector(vectors[0].X, vectors[0].Y).Length;
        }


    }
}
