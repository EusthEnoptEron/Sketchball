using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Collision
{
    /// <summary>
    /// Has references of all bounding boxes that cross this field
    /// </summary>
    public class BoundingField
    {
        /// <summary>
        /// Ref on BoundingBoxes of all Pinballelements Boundingboxes that intersect this raster
        /// </summary>
        private HashSet<IBoundingBox> bBReferences;  
  
        /// <summary>
        /// idx in raster
        /// </summary>
        public int x { get; private set; }   

        /// <summary>
        /// idx in raster
        /// </summary>
        public int y { get; private set; }  

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">idx x</param>
        /// <param name="y">idx y</param>
        public BoundingField(int x, int y)
        {
            this.bBReferences = new HashSet<IBoundingBox>();

            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Adds a reference to bounding box to this field
        /// </summary>
        /// <param name="bB"></param>
        public void addReference(IBoundingBox bB)
        {
            this.bBReferences.Add(bB);
        }

        /// <summary>
        /// Removes a reference to a bounding box from this field
        /// </summary>
        /// <param name="bB"></param>
        public void removeReference(IBoundingBox bB)
        {
            this.bBReferences.Remove(bB);
        }

        /// <summary>
        /// Gets all references to bounding  boxes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBoundingBox> getReferences()
        {
            return this.bBReferences;
        }
    }
}
