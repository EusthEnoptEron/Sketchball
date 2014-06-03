using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Sketchball.Controls
{
    /// <summary>
    /// Adds trackbar to toolstrip stuff
    /// </summary>
    [
    ToolStripItemDesignerAvailability
        (ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)
    ]
    public class ToolStripTrackBarItem : MyToolStripControlHost
    {
        public TrackBar Trackbar { get; private set; }

        public ToolStripTrackBarItem()
            : base(new TrackBar())
        {
            Trackbar = (TrackBar)Control;
        }
    }

}
