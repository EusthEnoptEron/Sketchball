using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchball.Collision
{
    interface IBoundingBox
    {
        public bool intersec(IBoundingBox bB);
        public Vector2 reflect(Vector2 vecIn);
        public void rotate(float degree, Vector2 center);
        public void move(Vector2 moveVec);
        public bool lineIntersec(BoundingLine bL);
        public bool circleIntersec(BoundingCircle bC);


    }
}
