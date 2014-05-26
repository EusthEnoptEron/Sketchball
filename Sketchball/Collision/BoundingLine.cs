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
    /// Line variant of bounding box
    /// </summary>
    public class BoundingLine : BoundingBox
    {
        //target is object space: based on pinball element position
        public Vector target{get; private set;}
        private readonly float pushBackByPointsCoefficient = 1.7f;
        private readonly float pushBackByLineCoefficient = 1.8f;

        private Vector _originalPosition;
        private Vector _originalTarget;

        public BoundingLine(Vector from, Vector target)
        {
            this.Position = from;
            this.target = target;

            _originalPosition = from;
            _originalTarget   = target;
        }


        public override bool Intersect(IBoundingBox bB, out Vector hitPoint)
        {
            return bB.LineIntersect(this, out hitPoint);
        }

        public override bool Intersect(IBoundingBox bB, out Vector hitPoint, Vector velocity)
        {
            return bB.LineIntersect(this, out hitPoint);
        }

        public override Vector Reflect(Vector vecIn, Vector hitPoint, Vector ballpos)
        {

            Vector dLine = this.target - this.Position;
            Vector normal = new Vector(-dLine.Y,dLine.X);     

            if (hitPoint == this.Position + this.BoundingContainer.ParentElement.Location)
            {
                normal = ballpos - (this.Position + this.BoundingContainer.ParentElement.Location);
            }

            if (hitPoint == this.target + this.BoundingContainer.ParentElement.Location)
            {
                normal = ballpos - (this.target + this.BoundingContainer.ParentElement.Location);
            }

            normal.Normalize();

            return ReflectVector(ref vecIn, ref normal);
        }

        public override Vector GetOutOfAreaPush(int diameterBall, Vector hitPoint, Vector velocity, Vector ballPos)
        {
            //check if hit at the end (corner reflection must be handled with less simplification
            if (hitPoint == this.Position + this.BoundingContainer.ParentElement.Location)
            {
                Vector t = ballPos + new Vector(diameterBall / 2f, diameterBall / 2f) - (this.Position + this.BoundingContainer.ParentElement.Location);
                if (t.X == 0 && t.Y == 0)
                {
                    return NormalizeVector(velocity) * (diameterBall / pushBackByPointsCoefficient);
                }
                return (diameterBall / pushBackByPointsCoefficient) * NormalizeVector(t);
            }

            if (hitPoint == this.target + this.BoundingContainer.ParentElement.Location)
            {
                Vector t = NormalizeVector(ballPos + new Vector(diameterBall / 2f, diameterBall / 2f) - (this.target + this.BoundingContainer.ParentElement.Location));
                if (t.X == 0 && t.Y == 0)
                {
                    return NormalizeVector(velocity) * (diameterBall / pushBackByPointsCoefficient);
                }
                return (diameterBall / pushBackByPointsCoefficient) * NormalizeVector(t);
            }

            //now check which normal we have to take (depends on velocity)
            Vector norm = NormalizeVector((this.target - this.Position).Normal());
            Vector dLine = this.target - this.Position;


            double d = Vector.Multiply((velocity), NormalizeVector(dLine));    //distance on dLine from pos to the point where the normal from velocity hits

            Vector q = d * NormalizeVector(dLine);       //point where normal on dline through Velocity point hits
            Vector h = velocity-q;         //horizontal line through velocitiy point and  normal on dline

            //if h and normal have not same sign => take other normal (so we move in right direction)
            if (h.X * norm.X < 0 || h.Y * norm.Y < 0)
            {
                norm = -norm;
            }

            if (d == 0)
            {
                //vertical
                norm = velocity;
                if (velocity.X == 0 && velocity.Y == 0)
                {
                    //Bug velocity 0 with collision 
                    norm.X = 1;
                }
                norm.Normalize();
            }
            return (diameterBall / pushBackByLineCoefficient) * norm;
        }


        public new void move(Vector moveVec)
        {
            base.move(moveVec);
            this.target += moveVec;
        }

        //TODO: UNTESTED
        public override bool LineIntersect(BoundingLine bL, out Vector hitPoint)
        {
            throw new MissingMethodException();
            Vector thisWorldTras = this.BoundingContainer.ParentElement.Location;
            Vector bLWorldTrans = bL.BoundingContainer.ParentElement.Location;

            Vector bLWorldPos = bL.Position + bLWorldTrans;
            Vector bLWorldTar = bL.target + bLWorldTrans;
            Vector thisWorldTar = this.target + thisWorldTras;
            Vector thisWorldPos = this.Position + thisWorldTras;


            double A1 = thisWorldTar.Y - thisWorldPos.Y;
            double B1 = thisWorldTar.X - thisWorldPos.X;
            double C1 = A1 * thisWorldPos.X + B1 * thisWorldPos.Y;

            double A2 = bLWorldTar.Y - bLWorldPos.Y;
            double B2 = bLWorldTar.X - bLWorldPos.X;
            double C2 = A2 * bLWorldPos.X + B2 * bLWorldPos.Y;

            double det = A1 * B1 - A2 * B2;      //determines same delta?

            if (det == 0)       //parallel
            {
                hitPoint = new Vector(0,0);
                return false;
            }
            else
            {
                //set equation equal
                double x = (B2*C1 - B1*C2) / det;
                double y = (A1 * C2 - A2 * C1) / det;

                if (Math.Min(thisWorldPos.X, thisWorldTar.X) < x && x < Math.Max(thisWorldPos.X, thisWorldTar.X))
                {
                    if (Math.Min(thisWorldPos.Y, thisWorldTar.Y) < y && y < Math.Max(thisWorldPos.Y, thisWorldTar.Y))
                    {
                        hitPoint = new Vector(x, y);
                        return true;
                    }
                }
                hitPoint = new Vector(0, 0);
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

        public override void Rotate(float rad, Vector center)
        {
            Matrix rotation = new Matrix();
            rotation.RotateAt((rad/Math.PI*180f), center.X, center.Y);

            Point[] pts = new Point[2];
            Vector p1 = this.Position;
            Vector p2 = this.target;
            pts[0].X = p1.X;
            pts[0].Y = p1.Y;
            pts[1].X = p2.X;
            pts[1].Y = p2.Y;

            rotation.Transform(pts);
            p1.X = (float)pts[0].X;
            p1.Y = (float)pts[0].Y;
            p2.X = (float)pts[1].X;
            p2.Y = (float)pts[1].Y;

            this.Position = p1;
            this.target = p2;     
        }

        public override bool CircleIntersect(BoundingCircle bC, out Vector hitPoint, Vector velocity)
        {
            //strategy: connect center of ball with start of line. calc where the normal from center of ball on line hits (pointNormalDirectionPice). If len from center of ball to this point
            //is smaller then radius then it is a hit. Should pointNormalDirectionPice be smaller then start - radius of ball or bigger then end+ radius of ball => ignore

            hitPoint = new Vector(0, 0);

            Vector bLWorldPos = this.Position + this.BoundingContainer.ParentElement.Location;
            Vector bLWorldTar = this.target + this.BoundingContainer.ParentElement.Location;
            Vector thisWorldPos = bC.Position + bC.BoundingContainer.ParentElement.Location;

            Vector centerOfCircle = thisWorldPos;
            Vector directionLine = bLWorldTar - bLWorldPos;
            Vector normalLine = new Vector(-directionLine.Y, directionLine.X);

            double lenDirectionPiece = Vector.Multiply((centerOfCircle - bLWorldPos), NormalizeVector(directionLine));

            if (lenDirectionPiece <= -bC.radius || lenDirectionPiece >= (directionLine.Length + bC.radius))
            {
                return false;
            }

            Vector pointNormalDirectionPice = bLWorldPos + lenDirectionPiece * NormalizeVector(directionLine);
            Vector normalFromDirLineToCenter = centerOfCircle - pointNormalDirectionPice;

            double diff = normalFromDirLineToCenter.Length;

            if (diff < bC.radius)
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

        public override IBoundingBox Clone()
        {
            BoundingLine bL =  new BoundingLine(new Vector(this.Position.X, this.Position.Y), new Vector(this.target.X, this.target.Y));
            //do not forget to assinge BoundingContainer after clone
            return bL;
        }

        public override void Sync(Matrix matrix)
        {
            Point[] points = new Point[] { new Point(_originalPosition.X, _originalPosition.Y), new Point(_originalTarget.X, _originalTarget.Y) };
            matrix.Transform(points);

            Position = new Vector((float)points[0].X, (float)points[0].Y);
            target   = new Vector((float)points[1].X, (float)points[1].Y);
        }

        public override void DrawDebug(DrawingContext g, System.Windows.Media.Pen pen)
        {
            Vector pos = this.BoundingContainer.ParentElement.Location;
            g.DrawLine(pen, new System.Windows.Point((int)(this.Position.X + pos.X), (int)(this.Position.Y + pos.Y)), new System.Windows.Point((int)(this.target.X + pos.X), (int)(this.target.Y + pos.Y)));
        }
    }
}
