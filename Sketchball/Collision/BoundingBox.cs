using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Collision
{
    public abstract class BoundingBox : IBoundingBox
    {
        public Vector2 position{ get; set; }

        public void move(Vector2 moveVec)
        {
            this.position += moveVec;
        }

    
        public abstract bool intersec(IBoundingBox bB);

        public abstract Vector2 reflect(Vector2 vecIn);

        public abstract void rotate(float degree, Vector2 center);

        public abstract bool lineIntersec(BoundingLine bL);

        public abstract List<Vector2> getVertices();

        public abstract bool circleIntersec(BoundingCircle bC);
    }
}
