using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.Editor
{
    /// <summary>
    /// Special panel that will NOT scroll into view when focused. Required to make a fluent transition between the GDI+ and the WPF world.
    /// </summary>
    public class NoScrollOnFocusPanel : Panel
    {
        protected override System.Drawing.Point ScrollToControl(Control activeControl)
        {
            return DisplayRectangle.Location;
        }
    }
}
