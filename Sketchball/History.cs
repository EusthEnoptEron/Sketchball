using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball
{
    /// <summary>
    /// A history that can be used to keep track of changes. Provides methods to redo and undo.
    /// </summary>
    public class History
    {
        private const int DEFAULT_CAPACITY = 50;

        private Stack<IChange> ExecutedChanges;
        private Queue<IChange> PendingChanges;
        private int Capacity;

        private int _dirty = 0;

        public History() : this(DEFAULT_CAPACITY)
        {
        }

        public History(int capacity)
        {
            ExecutedChanges = new Stack<IChange>(capacity);
            PendingChanges = new Queue<IChange>(capacity);
            Capacity = capacity;
        }

        /// <summary>
        /// Checks if there is an element in the undo queue.
        /// </summary>
        /// <returns></returns>
        public bool CanUndo()
        {
            return ExecutedChanges.Count > 0;
        }

        /// <summary>
        /// Checks if there is an element in the redo queue.
        /// </summary>
        /// <returns></returns>
        public bool CanRedo()
        {
            return PendingChanges.Count > 0;
        }

        /// <summary>
        /// Undoes a change if possible.
        /// </summary>
        public void Undo()
        {
            if (CanUndo())
            {
                IChange change = ExecutedChanges.Pop();
                change.Undo();

                PendingChanges.Enqueue(change);

                _dirty -= 1;
            }
        }

        /// <summary>
        /// Redoes a change if possible.
        /// </summary>
        public void Redo()
        {
            if (CanRedo())
            {
                IChange change = PendingChanges.Dequeue();
                change.Do();

                _dirty += 1;
            }
        }

        /// <summary>
        /// Adds a new change to the history. It is expected that it was already executed.
        /// </summary>
        /// <param name="change"></param>
        public void Add(IChange change)
        {
            PendingChanges.Clear();
            ExecutedChanges.Push(change);

            // x < 0 => clean state not reachable anymore.
            if (_dirty < 0) _dirty = Capacity * 2;
            else _dirty += 1;
        }


        /// <summary>
        /// Clears the history list.
        /// </summary>
        public void Clear()
        {
            PendingChanges.Clear();
            ExecutedChanges.Clear();
            _dirty = 0;
        }

        /// <summary>
        /// Gets if the history has changed since the last call to ClearStatus().
        /// </summary>
        /// <returns>Whether or not the history has changed.</returns>
        public bool HasChanged()
        {
            return _dirty != 0;
        }

        public void ClearStatus()
        {
            _dirty = 0;
        }
    }
}
