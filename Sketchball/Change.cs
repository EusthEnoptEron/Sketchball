using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball
{
    public interface Change
    {
        void Do();
        void Undo();
    }
}
