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
        private Stack<IChange> ExecutedChanges = new Stack<IChange>();
        private Queue<IChange> PendingChanges = new Queue<IChange>();

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
        }
    }
}
