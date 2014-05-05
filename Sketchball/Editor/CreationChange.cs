using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Editor
{

    /// <summary>
    /// Change that handles the creation of an element.
    /// </summary>
    public class CreationChange : IChange
    {
        private PinballElement element;
        private ElementCollection collection;

        public CreationChange(ElementCollection collection, PinballElement element)
        {
            this.collection = collection;
            this.element = element;
        }

        public void Do()
        {
            collection.Add(element);
        }

        public void Undo()
        {
            collection.Remove(element);
        }
    }
}
