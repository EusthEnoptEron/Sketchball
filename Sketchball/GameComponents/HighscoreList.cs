using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.GameComponents
{
    /// <summary>
    /// Represents a descending list of highscore entries with a max entry count of 30.
    /// </summary>
    [DataContract]
    public class HighscoreList : ICollection<HighscoreEntry>
    {
        [DataContract]
        private class DescendedComparer : IComparer<HighscoreEntry>
        {
            public int Compare(HighscoreEntry x, HighscoreEntry y)
            {
                // use the default comparer to do the original comparison for datetimes
                return x.CompareTo(y) * -1;
            }
        }

        private const int MAX_ENTRIES = 30;

        [DataMember]
        private SortedSet<HighscoreEntry> innerList = new SortedSet<HighscoreEntry>(new DescendedComparer());

        public HighscoreList()
        {
        }

        /// <summary>
        /// Adds a highscore entry to the list. A score of 0 is ignored.
        /// </summary>
        /// <param name="item"></param>
        public void Add(HighscoreEntry item)
        {
            // Don't insert losers.
            if (item.Score > 0)
            {
                innerList.Add(item);

                while (Count > MAX_ENTRIES)
                {
                    innerList.Remove(innerList.Last());
                }
            }
        }

        public void Clear()
        {
            innerList.Clear();
        }

        public bool Contains(HighscoreEntry item)
        {
            return innerList.Contains(item);
        }

        public void CopyTo(HighscoreEntry[] array, int arrayIndex)
        {
            innerList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return innerList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(HighscoreEntry item)
        {
            return innerList.Remove(item);
        }

        public IEnumerator<HighscoreEntry> GetEnumerator()
        {
            return innerList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return innerList.GetEnumerator();
        }
    }
}
