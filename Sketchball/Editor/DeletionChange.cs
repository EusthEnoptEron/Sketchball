using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Editor
{
    /// <summary>
    /// Represents change that handles deletion of elements,
    /// </summary>
    public class DeletionChange : IChange
    {
        private PinballElement element;
        private ElementCollection collection;

        /// <summary>
        /// Creates a new deletion.
        /// </summary>
        /// <param name="collection">Collection from which the element should disappear.</param>
        /// <param name="element">Element to disappear.</param>
        public DeletionChange(ElementCollection collection, PinballElement element)
        {
            this.collection = collection;
            this.element = element;
        }

        public void Undo()
        {
            collection.Add(element);
        }

        public void Do()
        {
            collection.Remove(element);

        }
    }
}
