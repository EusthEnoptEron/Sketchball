using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sketchball.Collision
{
    public interface IBoundingBox
    {
        /// <summary>
        /// Parent bounding container
        /// </summary>
        BoundingContainer BoundingContainer { get; }

        /// <summary>
        /// Defines how hard something is reflected
        /// </summary>
        float BounceFactor { get; set; }

        /// <summary>
        /// Position of the bounding box
        /// </summary>
        Vector Position { get; set; }


        /// <summary>
        /// Method to assigne this bounding box to a container
        /// </summary>
        /// <param name="bc">Container to add this bounding box</param>
        void AssignToContainer(BoundingContainer bc);


        /// <summary>
        /// Checks for an intersection of this element with bB and saves the estimated hitpoint to hitPoint)
        /// </summary>
        /// <param name="bB">Bounding box to check for intersection with this</param>
        /// <param name="hitPoint">Point where those two boxes might have intersected the first time</param>
        /// <returns>true if intersection</returns>
        bool Intersect(IBoundingBox bB, out Vector hitPoint);
       
        /// <summary>
        /// Checks for an intersection of this element with bB and saves the estimated hitpoint to hitPoint)
        /// </summary>
        /// <param name="bB">Bounding box to check for intersection with this</param>
        /// <param name="hitPoint">Point where those two boxes might have intersected the first time</param>
        /// <param name="velocity">Velocity of an object intersecting</param>
        /// <returns>true if intersection</returns>
        bool Intersect(IBoundingBox bB, out Vector hitPoint, Vector velocity);

        /// <summary>
        /// Intersec for lines (more specific version)
        /// </summary>
        /// <param name="bL">Bounding box to check for intersection with this</param>
        /// <param name="hitPoint">Point where those two boxes might have intersected the first time</param>
        /// <returns>true if intersection</returns>
        bool LineIntersect(BoundingLine bL, out Vector hitPoint);

        /// <summary>
        /// Intersec for circles (more specific version)
        /// </summary>
        /// <param name="bL">Bounding box to check for intersection with this</param>
        /// <param name="hitPoint">Point where those two boxes might have intersected the first time</param>
        /// <param name="velocity">Speed of the object intersecting</param>
        /// <returns>true if intersection</returns>
        bool CircleIntersect(BoundingCircle bC, out Vector hitPoint, Vector velocity);

        /// <summary>
        /// Calculates reflection of an round object on this bounding box
        /// </summary>
        /// <param name="vecIn">Velocity of the object that hits this</param>
        /// <param name="hitPoint">Point of first intersection</param>
        /// <param name="ballpos">Position of the object</param>
        /// <returns>Velocity after reflection</returns>
        Vector Reflect(Vector vecIn, Vector hitPoint, Vector ballpos);

        /// <summary>
        /// Calculates the vector to push an element out of area
        /// </summary>
        /// <param name="diameterBall">Diameter of the ball</param>
        /// <param name="hitPoint">Point of estimated first intersection</param>
        /// <param name="velocity">Velocity of ball</param>
        /// <param name="ballPos">Position of ball</param>
        /// <returns>Vector that represents push of object out of crictical zone</returns>
        Vector GetOutOfAreaPush(int diameterBall, Vector hitPoint, Vector velocity,Vector ballPos);
        
        /// <summary>
        /// Rotation for this bounding box
        /// </summary>
        /// <param name="rad">Amount of roation in rad</param>
        /// <param name="center">Center of rotation relative to the parent coordinates</param>
        void Rotate(double rad, Vector center);

        Vector ReflectManipulation(Vector newDirection, Vector hitpoint, int energy = 0);

        IBoundingBox Clone();

        void Sync(Matrix matrix);

        void DrawDebug(System.Windows.Media.DrawingContext g, System.Windows.Media.Pen pen);
    }
}
