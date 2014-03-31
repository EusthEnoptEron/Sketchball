using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchball.Collision
{
    public interface IBoundingBox
    {
        void assigneToContainer(BoundingContainer bc);
        BoundingContainer BoundingContainer { get; }
        Vector2 position { get; set; }
        bool intersec(IBoundingBox bB, out Vector2 hitPoint);
        Vector2 reflect(Vector2 vecIn, Vector2 hitPoint, Vector2 ballpos);
        Vector2 getOutOfAreaPush(int diameterBall, Vector2 hitPoint, Vector2 velocity,Vector2 ballPos);
        void rotate(float rad, Vector2 center);
        void move(Vector2 moveVec);
        bool lineIntersec(BoundingLine bL, out Vector2 hitPoint);
        bool circleIntersec(BoundingCircle bC, out Vector2 hitPoint);
    
        void drawDEBUG(System.Drawing.Graphics g, System.Drawing.Pen p);
    }
}
