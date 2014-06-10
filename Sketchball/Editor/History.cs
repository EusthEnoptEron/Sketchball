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
        public delegate void OnChange();
        public event OnChange Change;

        private const int DEFAULT_CAPACITY = 50;

        private Stack<IChange> ExecutedChanges;
        private Stack<IChange> PendingChanges;
        private int Capacity;

        /// <summary>
        /// *Signed* distance from the current point to the last "save point".
        /// </summary>
        private int _dirty = 0;

        public History() : this(DEFAULT_CAPACITY)
        {
        }

        /// <summary>
        /// Creates a new history with a certain capacity. Warning: capacity is not implemented yet.
        /// </summary>
        /// <param name="capacity"></param>
        public History(int capacity)
        {
            ExecutedChanges = new Stack<IChange>(capacity);
            PendingChanges = new Stack<IChange>(capacity);
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

                PendingChanges.Push(change);

                _dirty -= 1;

                RaiseChangeEvent();
            }
        }

        /// <summary>
        /// Redoes a change if possible.
        /// </summary>
        public void Redo()
        {
            if (CanRedo())
            {
                IChange change = PendingChanges.Pop();
                change.Do();

                ExecutedChanges.Push(change);

                _dirty += 1;

                RaiseChangeEvent();
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

            RaiseChangeEvent();
        }


        /// <summary>
        /// Clears the history list.
        /// </summary>
        public void Clear()
        {
            PendingChanges.Clear();
            ExecutedChanges.Clear();
            _dirty = 0;

            RaiseChangeEvent();
        }

        /// <summary>
        /// Gets if the history has changed since the last call to ClearStatus().
        /// </summary>
        /// <returns>Whether or not the history has changed.</returns>
        public bool HasChanged()
        {
            return _dirty != 0;
        }

        /// <summary>
        /// Resets the "dirty" flag.
        /// </summary>
        public void ClearStatus()
        {
            _dirty = 0;
        }

        private void RaiseChangeEvent()
        {
            var handlers = Change;
            if (handlers != null)
            {
                handlers();
            }
        }

        /// <summary>
        /// Adds a change and immediately executes it.
        /// </summary>
        /// <param name="change"></param>
        public void AddAndDo(IChange change)
        {
            this.Add(change);
            change.Do();
        }
    }
}
