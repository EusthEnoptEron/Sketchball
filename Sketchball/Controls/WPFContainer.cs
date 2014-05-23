using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms.Integration;

namespace Sketchball.Controls
{
    public class WPFContainer : ElementHost
    {
        private UserControl Control;
        public WPFContainer(UserControl control)
        {
            Child = Control = control;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Control.Width = Width;
            Control.Height = Height;
        }

    }
}
