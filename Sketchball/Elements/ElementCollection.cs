using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{

    /// <summary>
    /// Collection that maintains a list of elements and makes sure the bidirectional connection stays intact.
    /// </summary>
    [DataContract(IsReference=true)]
    public class ElementCollection : ICollection<PinballElement>
    {
        /// <summary>
        /// Gets the owner of the element list.
        /// </summary>
        [DataMember]
        public PinballMachine Owner { get; private set;}

        [DataMember]
        private List<PinballElement> elements = new List<PinballElement>();

        /// <summary>
        /// Creates a new element collection that belongs to owner.
        /// </summary>
        /// <param name="parent"></param>
        public ElementCollection(PinballMachine parent)
        {
            Owner = parent;
        }

        /// <summary>
        /// Moves an element to the tail of the collection, i.e. causing it to be drawn last.
        /// </summary>
        /// <param name="element"></param>
        public void MoveToTail(PinballElement element)
        {
            if (elements.Remove(element))
            {
                elements.Add(element);
            }
        }


        /// <summary>
        /// Moves an element to the head of the collection.
        /// </summary>
        /// <param name="element"></param>
        public void MoveToHead(PinballElement element)
        {
            if (elements.Remove(element))
            {
                elements.Insert(0, element);
            }
        }

        private void ClaimElement(PinballElement element)
        {
            if (element.World != null && element.World != Owner)
            {
                element.World.DynamicElements.Remove(element);
            }
            element.World = Owner;
        }


        private void ReleaseElement(PinballElement element)
        {
            if (element.World == Owner)
                element.World = null;
        }

#region LIST IMPLEMENTATION
        public int IndexOf(PinballElement item)
        {
            return elements.IndexOf(item);
        }

        public void Insert(int index, PinballElement item)
        {
            ClaimElement(item);
            elements.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            if (elements[index] != null)
            {
                ReleaseElement(elements[index]);
            }
            elements.RemoveAt(index);
        }

        public PinballElement this[int index]
        {
            get
            {
                return elements[index];
            }
            set
            {
                lock (this)
                {
                    if (elements[index] != null)
                    {
                        ReleaseElement(elements[index]);
                    }
                    ClaimElement(value);
                    elements[index] = value;
                }
            }
        }

        public void Add(PinballElement item)
        {
            lock(this) {
                ClaimElement(item);
                elements.Add(item);
            }
        }

        public void Clear()
        {
            lock (this)
            {
                foreach (PinballElement element in elements)
                {
                    ReleaseElement(element);
                }

                elements.Clear();
            }
        }

        public bool Contains(PinballElement item)
        {
            return elements.Contains(item);
        }

        public void CopyTo(PinballElement[] array, int arrayIndex)
        {
            elements.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return elements.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(PinballElement item)
        {
            lock (this)
            {
                ReleaseElement(item);
                return elements.Remove(item);
            }
        }

        public IEnumerator<PinballElement> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return elements.GetEnumerator();
        }
    }

#endregion

}
