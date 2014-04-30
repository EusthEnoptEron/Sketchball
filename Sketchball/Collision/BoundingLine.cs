using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Collision
{
    /// <summary>
    /// Line variant of bounding box
    /// </summary>
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

        public override bool intersec(IBoundingBox bB, out Vector2 hitPoint, Vector2 velocity)
        {
            return bB.lineIntersec(this, out hitPoint);
        }

        public override Vector2 reflect(Vector2 vecIn, Vector2 hitPoint, Vector2 ballpos)
        {

            Vector2 dLine = this.target - this.position;
            Vector2 normal = new Vector2(-dLine.Y,dLine.X);     

            if (hitPoint == this.position + this.BoundingContainer.parentElement.getLocation())
            {
                normal = ballpos - (this.position + this.BoundingContainer.parentElement.getLocation());
            }

            if (hitPoint == this.target + this.BoundingContainer.parentElement.getLocation())
            {
                normal = ballpos - (this.target + this.BoundingContainer.parentElement.getLocation());
            }

            normal.Normalize();

            return Vector2.Reflect(vecIn, normal);
        }

        public override Vector2 getOutOfAreaPush(int diameterBall, Vector2 hitPoint, Vector2 velocity, Vector2 ballPos)
        {
            //check if hit at the end (corner reflection must be handled with less simplification
            if (hitPoint == this.position + this.BoundingContainer.parentElement.getLocation())
            {
                Vector2 t = ballPos + new Vector2(diameterBall / 2f, diameterBall / 2f) - (this.position + this.BoundingContainer.parentElement.getLocation());
                if (t.X == 0 && t.Y == 0)
                {
                    return -Vector2.Normalize(velocity) * (diameterBall / 1.7f);
                }
                return (diameterBall / 1.7f) * t;
            }

            if (hitPoint == this.target + this.BoundingContainer.parentElement.getLocation())
            {
                Vector2 t = Vector2.Normalize(ballPos + new Vector2(diameterBall / 2f, diameterBall / 2f) - (this.target + this.BoundingContainer.parentElement.getLocation()));
                if (t.X == 0 && t.Y == 0)
                {
                    return -Vector2.Normalize(velocity) * (diameterBall / 1.7f);
                }
                return (diameterBall / 1.7f) * t;
            }

            //now check which normal we have to take (depends on velocity)
            Vector2 norm = Vector2.Normalize((this.target - this.position).Normal());
            Vector2 dLine = this.target - this.position;

            float d = Vector2.Dot((velocity), Vector2.Normalize(dLine));    //distance on dLine from pos to the point where the normal from velocity hits
          
            Vector2 q = d * Vector2.Normalize(dLine);       //point where normal on dline through Velocity point hits
            Vector2 h = velocity-q;         //horizontal line through velocitiy point and  normal on dline

            //if h and normal have not same sign => take other normal (so we move in right direction)
            if (h.X * norm.X < 0 || h.Y * norm.Y < 0)
            {
                norm = -norm;
            }

            if (d == 0)
            {
                //vertical
                norm = -velocity;
                norm.Normalize();
            }
            return (diameterBall / 1.9f) * norm;
        }


        public new void move(Vector2 moveVec)
        {
            base.move(moveVec);
            this.target += moveVec;
        }

        //TODO: UNTESTED
        public override bool lineIntersec(BoundingLine bL, out Vector2 hitPoint)
        {
            throw new MissingMethodException();
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

        public override void rotate(float rad, Vector2 center)
        {
            Matrix rotation = new Matrix();
            System.Drawing.PointF ptCenter = new System.Drawing.PointF(center.X, center.Y);
            rotation.RotateAt((float)(rad/Math.PI*180f), ptCenter);

            System.Drawing.PointF[] pts = new System.Drawing.PointF[2];
            Vector2 p1 = this.position + this.BoundingContainer.parentElement.getLocation();
            Vector2 p2 = this.target + this.BoundingContainer.parentElement.getLocation();
            pts[0].X = p1.X;
            pts[0].Y = p1.Y;
            pts[1].X = p2.X;
            pts[1].Y = p2.Y;

            rotation.TransformPoints(pts);
            p1.X = pts[0].X - this.BoundingContainer.parentElement.getLocation().X;
            p1.Y = pts[0].Y - this.BoundingContainer.parentElement.getLocation().Y;
            p2.X = pts[1].X - this.BoundingContainer.parentElement.getLocation().X;
            p2.Y = pts[1].Y- this.BoundingContainer.parentElement.getLocation().Y;

            this.position = p1;
            this.target = p2;     
        }

        public override bool circleIntersec(BoundingCircle bC, out Vector2 hitPoint, Vector2 velocity)
        {
            //strategy: connect center of ball with start of line. calc where the normal from center of ball on line hits (pointNormalDirectionPice). If len from center of ball to this point
            //is smaller then radius then it is a hit. Should pointNormalDirectionPice be smaller then start - radius of ball or bigger then end+ radius of ball => ignore

            hitPoint = new Vector2(0, 0);

            Vector2 bLWorldPos = this.position + this.BoundingContainer.parentElement.getLocation();
            Vector2 bLWorldTar = this.target + this.BoundingContainer.parentElement.getLocation();
            Vector2 thisWorldPos = bC.position + bC.BoundingContainer.parentElement.getLocation();

            Vector2 centerOfCircle = thisWorldPos;
            Vector2 directionLine = bLWorldTar - bLWorldPos;
            Vector2 normalLine = new Vector2(-directionLine.Y, directionLine.X);

            float lenDirectionPiece = Vector2.Dot((centerOfCircle - bLWorldPos), Vector2.Normalize(directionLine));

            if (lenDirectionPiece <= -bC.radius || lenDirectionPiece >= (directionLine.Length() + bC.radius))
            {
                return false;
            }

            Vector2 pointNormalDirectionPice = bLWorldPos + lenDirectionPiece * Vector2.Normalize(directionLine);
            Vector2 normalFromDirLineToCenter = centerOfCircle - pointNormalDirectionPice;

            float diff = normalFromDirLineToCenter.Length();

            if (diff < bC.radius)
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

        public override void drawDEBUG(System.Drawing.Graphics g, System.Drawing.Pen p)
        {
            Vector2 pos = this.BoundingContainer.parentElement.getLocation();
            g.DrawLine(p, (int)(this.position.X + pos.X), (int)(this.position.Y + pos.Y), (int)(this.target.X + pos.X), (int)(this.target.Y + pos.Y));
        }
    }
}
