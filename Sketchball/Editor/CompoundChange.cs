using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Editor
{
    public class CompoundChange : IChange
    {
        IEnumerable<IChange> changes;
        public CompoundChange(IEnumerable<IChange> changes)
        {
            this.changes = changes;
        }
        public void Do()
        {
            foreach (var change in changes)
            {
                change.Do();
            }
        }

        public void Undo()
        {
            foreach (var change in changes.Reverse())
            {
                change.Undo();
            }
        }
    }
}
