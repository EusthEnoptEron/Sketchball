using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Collision
{
    /// <summary>
    /// This class defines the common of all bounding box variants
    /// </summary>
    public abstract class BoundingBox : IBoundingBox
    {
        /// <summary>
        /// Parent container which holds the bounding boxes
        /// </summary>
        public BoundingContainer BoundingContainer { get; private set; }

        //position object space: from pinball element pos out
        public Vector2 position{ get; set; }

        /// <summary>
        /// Move the bounding box in this direction
        /// </summary>
        /// <param name="moveVec">direction and distance to move</param>
        public void move(Vector2 moveVec)
        {
            this.position += moveVec;
        }

        /// <summary>
        /// assigne this bounding box to a container
        /// </summary>
        /// <param name="bc">The container which this bounding box shall be assigned to</param>
        public void assigneToContainer(BoundingContainer bc)
        {
            this.BoundingContainer = bc;
        }

        /// <summary>
        /// Finds out if this bounding box intersects with bB and saves the estimated hitPoint in hitPoint
        /// </summary>
        /// <param name="bB">Bounding box to check intersection</param>
        /// <param name="hitPoint">the point where bB and this bounding box intersected the first time</param>
        /// <returns>true if intersection</returns>
        public abstract bool intersec(IBoundingBox bB, out Vector2 hitPoint);

        /// <summary>
        /// Method that calculated a reflection of a ball
        /// </summary>
        /// <param name="vecIn">Velocity of the ball</param>
        /// <param name="hitPoint">Point where the ball hits this bounding box</param>
        /// <param name="ballpos">Position of the ball</param>
        /// <returns>Reflection vector</returns>
        public abstract Vector2 reflect(Vector2 vecIn, Vector2 hitPoint, Vector2 ballpos);

        /// <summary>
        /// Calculates a push back vector that assures that the ball does not still intersect the same element after reflection
        /// </summary>
        /// <param name="diameterBall">Diameter of the ball</param>
        /// <param name="hitPoint">Point where the ball hit the bounding box</param>
        /// <param name="velocity">Velocity of the ball (after reflection)</param>
        /// <param name="ballPos">Position of the ball</param>
        /// <returns>Push back vector</returns>
        public abstract Vector2 getOutOfAreaPush(int diameterBall, Vector2 hitPoint, Vector2 velocity, Vector2 ballPos);

        /// <summary>
        /// Rotates this bounding box
        /// </summary>
        /// <param name="rad">Amount of rad</param>
        /// <param name="center">Center of rotation</param>
        public abstract void rotate(float rad, Vector2 center);

        /// <summary>
        /// Submethod of intersect => checks for an intersection of this bounding box and a Bounding line
        /// </summary>
        /// <param name="bL">Bounding line which might intersect this bounding box</param>
        /// <param name="hitPoint">Point where this bounding box intersects with bL</param>
        /// <returns>true if intersection</returns>
        public abstract bool lineIntersec(BoundingLine bL, out Vector2 hitPoint);

        /// <summary>
        /// Submethod of intersect => checks for an intersection of this bounding box and a Bounding circle
        /// </summary>
        /// <param name="bC">Bounding circle which might intersect with this bounding box</param>
        /// <param name="hitPoint"></param>
        /// <returns></returns>
        public abstract bool circleIntersec(BoundingCircle bC, out Vector2 hitPoint);

        /// <summary>
        /// Debug method
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        public abstract void drawDEBUG(System.Drawing.Graphics g,System.Drawing.Pen p);
    }
}
