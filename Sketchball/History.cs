using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball
{
    public class History
    {
        private Stack<Change> ExecutedChanges = new Stack<Change>();
        private Queue<Change> PendingChanges = new Queue<Change>();

        public bool CanUndo()
        {
            return ExecutedChanges.Count > 0;
        }

        public bool CanRedo()
        {
            return PendingChanges.Count > 0;
        }

        public void Undo()
        {
            if (CanUndo())
            {
                Change change = ExecutedChanges.Pop();
                change.Undo();

                PendingChanges.Enqueue(change);
            }
        }

        public void Redo()
        {
            if (CanRedo())
            {
                Change change = PendingChanges.Dequeue();
                change.Do();
            }
        }

        /// <summary>
        /// Adds a new change to the history. It is expected that it was already executed.
        /// </summary>
        /// <param name="change"></param>
        public void Add(Change change)
        {
            PendingChanges.Clear();
            ExecutedChanges.Push(change);
        }
    }
}
