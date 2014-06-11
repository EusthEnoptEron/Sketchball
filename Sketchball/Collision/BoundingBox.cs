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
    /// This class defines the common of all bounding box variants
    /// </summary>
    public abstract class BoundingBox : IBoundingBox
    {
        /// <summary>
        /// Parent container which holds the bounding boxes
        /// </summary>
        public BoundingContainer BoundingContainer { get; private set; }

        public BoundingBox()
        {
            this.BounceFactor = 0.9f;
        }
        /// <summary>
        /// Defines how hard something is reflected
        /// </summary>
        public float BounceFactor { get; set; }

        //position object space: from pinball element pos out
        public Vector Position{ get; set; }

        /// <summary>
        /// Move the bounding box in this direction
        /// </summary>
        /// <param name="moveVec">direction and distance to move</param>
        public void move(Vector moveVec)
        {
            this.Position += moveVec;
        }

        /// <summary>
        /// assigne this bounding box to a container
        /// </summary>
        /// <param name="bc">The container which this bounding box shall be assigned to</param>
        public void AssignToContainer(BoundingContainer bc)
        {
            this.BoundingContainer = bc;
        }

        /// <summary>
        /// Finds out if this bounding box intersects with bB and saves the estimated hitPoint in hitPoint
        /// </summary>
        /// <param name="bB">Bounding box to check intersection</param>
        /// <param name="hitPoint">the point where bB and this bounding box intersected the first time</param>
        /// <returns>true if intersection</returns>
        public abstract bool Intersect(IBoundingBox bB, out Vector hitPoint);

        /// <summary>
        /// Finds out if this bounding box intersects with bB and saves the estimated hitPoint in hitPoint
        /// </summary>
        /// <param name="bB">Bounding box to check intersection</param>
        /// <param name="hitPoint">the point where bB and this bounding box intersected the first time</param>
        /// <param name="velocity">Speed of the object intersecting</param>
        /// <returns>true if intersection</returns>
        public abstract bool Intersect(IBoundingBox bB, out Vector hitPoint, Vector velocity);

        /// <summary>
        /// Method that calculated a reflection of a ball
        /// </summary>
        /// <param name="vecIn">Velocity of the ball</param>
        /// <param name="hitPoint">Point where the ball hits this bounding box</param>
        /// <param name="ballpos">Position of the ball</param>
        /// <returns>Reflection vector</returns>
        public abstract Vector Reflect(Vector vecIn, Vector hitPoint, Vector ballpos);

        /// <summary>
        /// Calculates a push back vector that assures that the ball does not still intersect the same element after reflection
        /// </summary>
        /// <param name="diameterBall">Diameter of the ball</param>
        /// <param name="hitPoint">Point where the ball hit the bounding box</param>
        /// <param name="velocity">Velocity of the ball (after reflection)</param>
        /// <param name="ballPos">Position of the ball</param>
        /// <returns>Push back vector</returns>
        public abstract Vector GetOutOfAreaPush(int diameterBall, Vector hitPoint, Vector velocity, Vector ballPos);

        /// <summary>
        /// Rotates this bounding box
        /// </summary>
        /// <param name="rad">Amount of rad</param>
        /// <param name="center">Center of rotation</param>
        public abstract void Rotate(double rad, Vector center);

        /// <summary>
        /// Submethod of intersect => checks for an intersection of this bounding box and a Bounding line
        /// </summary>
        /// <param name="bL">Bounding line which might intersect this bounding box</param>
        /// <param name="hitPoint">Point where this bounding box intersects with bL</param>
        /// <returns>true if intersection</returns>
        public abstract bool LineIntersect(BoundingLine bL, out Vector hitPoint);

        /// <summary>
        /// Submethod of intersect => checks for an intersection of this bounding box and a Bounding circle
        /// </summary>
        /// <param name="bC">Bounding circle which might intersect with this bounding box</param>
        /// <param name="hitPoint">Point where this bounding box intersects with bC</param>
        /// <param name="velocity">Speed of the object intersecting</param>
        /// <returns></returns>
        public abstract bool CircleIntersect(BoundingCircle bC, out Vector hitPoint, Vector velocity);
        public virtual Vector ReflectManipulation(Vector newDirection,Vector hitpoint, int energy = 0)
        {
            return newDirection * BounceFactor * BoundingContainer.ParentElement.BounceFactor;
        }

        public abstract IBoundingBox Clone();
        public abstract void Sync(Matrix matrix);
        public abstract void DrawDebug(System.Windows.Media.DrawingContext g, System.Windows.Media.Pen pen);

        protected Vector ReflectVector(ref Vector vector, ref Vector normal)
        {
            double dot = Vector.Multiply(vector, normal);
            return new Vector(vector.X - ((2 * dot) * normal.X), vector.Y - ((2 * dot) * normal.Y));
        }

        protected double VectorDistance(Vector value1, Vector value2)
        {
            return Math.Sqrt((value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y));
        }

        protected Vector NormalizeVector(Vector v)
        {
            var vector = new Vector(v.X, v.Y);
            vector.Normalize();

            return vector;
        }
    }
}
