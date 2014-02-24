using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    [Serializable]
    public class GameWorld : IList<PinballElement>
    {
        private List<PinballElement> Elements = new List<PinballElement>();
        public Vector2 Gravity { get; set; }
        private Size Boundaries;

        public int Width { get { return Boundaries.Width; } }
        public int Height { get { return Boundaries.Height; } }


        public GameWorld(Size boundaries)
        {
            Boundaries = boundaries;
            Gravity = new Vector2(0, 1f);
        }

        internal void Update(long elapsed)
        {
            foreach(PinballElement element in Elements) {
                element.Update(elapsed);
            }

            // Find collisions
            // ...
        }

        private void ClaimElement(PinballElement element)
        {
            if (element.World != null && element.World != this)
            {
                element.World.Remove(element);
            }
            element.World = this;
        }


        private void ReleaseElement(PinballElement element)
        {
            if(element.World == this)
                element.World = null;
        }


        public void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.Black, 0, 0, Width, Height);
            foreach (PinballElement element in Elements)
            {
                GraphicsState gstate = g.Save();

                g.TranslateTransform(element.X, element.Y);
                element.Draw(g);

                g.Restore(gstate);
            }
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
