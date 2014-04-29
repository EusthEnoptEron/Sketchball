using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{

    [DataContract(IsReference=true)]
    public class ElementCollection : ICollection<PinballElement>
    {
        [DataMember]
        public PinballMachine Owner { get; private set;}

        [DataMember]
        private List<PinballElement> Elements = new List<PinballElement>();

        public ElementCollection(PinballMachine parent)
        {
            Owner = parent;
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
            return Elements.IndexOf(item);
        }

        public void Insert(int index, PinballElement item)
        {
            ClaimElement(item);
            Elements.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            if (Elements[index] != null)
            {
                ReleaseElement(Elements[index]);
            }
            Elements.RemoveAt(index);
        }

        public PinballElement this[int index]
        {
            get
            {
                return Elements[index];
            }
            set
            {
                if (Elements[index] != null)
                {
                    ReleaseElement(Elements[index]);
                }
                ClaimElement(value);
                Elements[index] = value;
            }
        }

        public void Add(PinballElement item)
        {
            ClaimElement(item);
            Elements.Add(item);
        }

        public void Clear()
        {
            foreach (PinballElement element in Elements)
            {
                ReleaseElement(element);
            }

            Elements.Clear();
        }

        public bool Contains(PinballElement item)
        {
            return Elements.Contains(item);
        }

        public void CopyTo(PinballElement[] array, int arrayIndex)
        {
            Elements.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Elements.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(PinballElement item)
        {
            ReleaseElement(item);
            return Elements.Remove(item);
        }

        public IEnumerator<PinballElement> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Elements.GetEnumerator();
        }
    }

#endregion

}
