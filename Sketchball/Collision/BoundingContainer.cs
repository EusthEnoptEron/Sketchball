using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using Sketchball.Elements;

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
        public float Rotation { get; set; }

        /// <summary>
        /// All bounding boxes of this container
        /// </summary>
        public List<IBoundingBox> boundingBoxes { get; private set; }

        /// <summary>
        /// Reference to the Pinball element which belongs to this bounding container
        /// </summary>
        public PinballElement parentElement { get; private set; }

        /// <summary>
        /// Construtor to create new bounding container
        /// </summary>
        /// <param name="parent">Parent of this container</param>
        public BoundingContainer(PinballElement parent)
        {
            this.boundingBoxes = new List<IBoundingBox>();
            this.parentElement = parent;
            this.Rotation = 0;
        }

        /// <summary>
        /// Returns a list of all bounding boxes
        /// </summary>
        /// <returns>The list of all bounding boxes of this container</returns>
        public List<IBoundingBox> getBoundingBoxes()
        {
            return this.boundingBoxes;
        }

        //center must be object space orientated!
        /// <summary>
        /// Rotates all of this containers bounding boxes  around the given center
        /// </summary>
        /// <param name="rad">Determines how much that is rotated in rad</param>
        /// <param name="center">Center of rotation</param>
        public void rotate(float rad, Vector2 center)
        {
            foreach (IBoundingBox b in this.boundingBoxes)
            {
                b.rotate(rad - Rotation, center+this.parentElement.getLocation());
            }
            this.Rotation = rad;

        }

        /// <summary>
        /// Moves all bounding boxes
        /// </summary>
        /// <param name="moveVec">Direction and distance to be moved</param>
        public void move(Vector2 moveVec)
        {
            foreach (IBoundingBox b in this.boundingBoxes)
            {
                b.move(moveVec);
            }
        }

        /// <summary>
        /// Adds a bounding box to this container
        /// </summary>
        /// <param name="bL">The bounding box to add</param>
        public void addBoundingBox(IBoundingBox bL)
        {
            this.boundingBoxes.Add(bL);
            bL.assigneToContainer(this);
        }

        /// <summary>
        /// Creates bounding lines for given coordinates that define a polygon
        /// </summary>
        /// <param name="coords">points that define the polygon</param>
        public void AddPolygon(params float[] coords)
        {
            Vector2 prev = new Vector2();

            for (int i = 0; i+1 < coords.Length; i += 2)
            {
                var x = coords[i];
                var y = coords[i + 1];

                var v = new Vector2(x, y);

                if (i > 0)
                {
                    addBoundingBox(new BoundingLine(
                        prev,
                        v
                    ));
                }

                prev = v;
            }
        }

    }
}
