using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball
{
    /// <summary>
    /// Interface for changes that can be kept track of in the history.
    /// </summary>
    public interface IChange
    {
        /// <summary>
        /// Executes the change.
        /// </summary>
        void Do();

        /// <summary>
        /// Undoes the change.
        /// </summary>
        void Undo();
    }
}
