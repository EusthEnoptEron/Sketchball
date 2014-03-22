using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Collision
{
    public abstract class BoundingBox : IBoundingBox
    {
        public BoundingContainer BoundingContainer { get; private set; }

        //position object space: from pinball element pos out
        public Vector2 position{ get; set; }


        public void move(Vector2 moveVec)
        {
            this.position += moveVec;
        }

        public void assigneToContainer(BoundingContainer bc)
        {
            this.BoundingContainer = bc;
        }

        public abstract bool intersec(IBoundingBox bB, out Vector2 hitPoint);

        public abstract Vector2 reflect(Vector2 vecIn, Vector2 hitPoint, Vector2 ballpos);

        public abstract Vector2 getOutOfAreaPush(int diameterBall, Vector2 hitPoint, Vector2 velocity, Vector2 ballPos);

        public abstract void rotate(float degree, Vector2 center);

        public abstract bool lineIntersec(BoundingLine bL, out Vector2 hitPoint);

        public abstract bool circleIntersec(BoundingCircle bC, out Vector2 hitPoint);




        public abstract void drawDEBUG(System.Drawing.Graphics g,System.Drawing.Pen p);
    }
}
