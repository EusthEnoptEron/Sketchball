﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.GameComponents
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class HighscoreList : ICollection<HighscoreEntry>
    {
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


        public void Add(HighscoreEntry item)
        {
            innerList.Add(item);

            while (Count > MAX_ENTRIES)
            {
                innerList.Remove(innerList.Last());
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
