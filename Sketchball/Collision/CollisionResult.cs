using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Collision
{
    public class CollisionResult : IEnumerable<PinballElement>
    {

        private HashSet<PinballElement> elements = new HashSet<PinballElement>();
    
        public CollisionResult(IEnumerable<IBoundingBox> boundingBoxes)
        {
            foreach (IBoundingBox box in boundingBoxes)
            {
                elements.Add(box.BoundingContainer.ParentElement);
            }
        }

        public static CollisionResult FromBoundingBoxes(IEnumerable<IBoundingBox> boxes)
        {
            return new CollisionResult(boxes);
        }

        public IEnumerator<PinballElement> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get
            {
                return elements.Count;
            }
        }
    }
}
