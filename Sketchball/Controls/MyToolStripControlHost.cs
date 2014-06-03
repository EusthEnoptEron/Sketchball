using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.Controls
{
    public class MyToolStripControlHost : ToolStripControlHost
    {
        public MyToolStripControlHost() : base(new Control())
        {
        }

        public MyToolStripControlHost(Control c) : base(c) 
        { 
        }
    }
}
