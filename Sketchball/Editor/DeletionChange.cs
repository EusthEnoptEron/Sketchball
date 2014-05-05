using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Editor
{
    /// <summary>
    /// Change that handles the deletion of an element.
    /// </summary>
    public class DeletionChange : IChange
    {
        private PinballElement element;
        private ElementCollection collection;

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
