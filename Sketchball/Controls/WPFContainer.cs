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
        private ManagedWPFControl Control;
        private bool updating = false;

        public WPFContainer(ManagedWPFControl control)
        {
            Child = Control = control;

            Control.SizeChanged += (s,e) => {
                if (Dock != System.Windows.Forms.DockStyle.Fill)
                {
                    updating = true;
                    this.Width = (int)Control.Width;
                    this.Height = (int)Control.Height;
                    updating = false;
                }
            };
            Disposed += (s,e) => {
                Control.Exit();
            };
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (!updating)
            {
                Control.Width = Width;
                Control.Height = Height;
            }

        }
        

    }
}
