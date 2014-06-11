using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using Sketchball.Elements;
using System.Drawing;
using System.Windows;

namespace Sketchball.Collision
{
    /// <summary>
    /// Holds boundingboxes of a pinball element
    /// </summary>
    public class BoundingContainer
    {
        /// <summary>
        /// Amount that this bounding container is rotated
        /// </summary>
        public double Rotation { get; set; }

        /// <summary>
        /// All bounding boxes of this container
        /// </summary>
        public List<IBoundingBox> BoundingBoxes { get; private set; }

        /// <summary>
        /// Reference to the Pinball element which belongs to this bounding container
        /// </summary>
        public PinballElement ParentElement { get; private set; }

        /// <summary>
        /// Construtor to create new bounding container
        /// </summary>
        /// <param name="parent">Parent of this container</param>
        public BoundingContainer(PinballElement parent)
        {
            this.BoundingBoxes = new List<IBoundingBox>();
            this.ParentElement = parent;
            this.Rotation = 0;
        }

        //center must be object space orientated!
        /// <summary>
        /// Rotates all of this containers bounding boxes  around the given center
        /// </summary>
        /// <param name="rad">Determines how much that is rotated in rad</param>
        /// <param name="center">Center of rotation</param>
        public void Rotate(double rad, Vector center)
        {
            foreach (IBoundingBox b in this.BoundingBoxes)
            {
                b.Rotate(rad - Rotation, center);
            }
            this.Rotation = rad;
        }

        /// <summary>
        /// Moves all bounding boxes
        /// </summary>
        /// <param name="moveVec">Direction and distance to be moved</param>
        public void move(Vector moveVec)
        {
            throw new NotImplementedException("Move is not supported anymore. Use a matrix if needed");
           /* foreach (IBoundingBox b in this.boundingBoxes)
            {
                b.move(moveVec);
            }*/
        }

        /// <summary>
        /// Synchronize points with the assigned parent element.
        /// </summary>
        public void Sync()
        {
            foreach (IBoundingBox b in this.BoundingBoxes)
            {
                b.Sync(ParentElement.Transform);
            }
        }

        /// <summary>
        /// Adds a bounding box to this container
        /// </summary>
        /// <param name="bL">The bounding box to add</param>
        public void AddBoundingBox(IBoundingBox bL)
        {
            this.BoundingBoxes.Add(bL);
            bL.AssignToContainer(this);
        }

        /// <summary>
        /// Creates bounding lines for given coordinates that define a polygon
        /// </summary>
        /// <param name="coords">points that define the polygon</param>
        public void AddPolyline(params float[] coords)
        {
            Vector prev = new Vector();

            for (int i = 0; i+1 < coords.Length; i += 2)
            {
                var x = coords[i];
                var y = coords[i + 1];

                var v = new Vector(x, y);

                if (i > 0)
                {
                    AddBoundingBox(new BoundingLine(
                        prev,
                        v
                    ));
                }

                prev = v;
            }
        }

        public void AddPolyline(Vector[] coords)
        {
            Vector prev = new Vector();

            for (int i = 0; i + 1 < coords.Length; i += 2)
            {
                var v = coords[i];

                if (i > 0)
                {
                    AddBoundingBox(new BoundingLine(
                        prev,
                        v
                    ));
                }

                prev = v;
            }
        }

        /// <summary>
        /// Checks if two bounding container intersect each other.
        /// </summary>
        /// <param name="bC"></param>
        /// <returns></returns>
        public bool Intersects(BoundingContainer bC)
        {
            Vector dummy = new Vector();
            foreach (var b1 in BoundingBoxes)
            {
                foreach (var b2 in bC.BoundingBoxes)
                {
                    if (b2.Intersect(b1, out dummy)) return true;
                }
            }
            return false;
        }


    }
}
