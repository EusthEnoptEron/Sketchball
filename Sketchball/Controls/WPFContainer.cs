using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using SizeChangedEventArgs = System.Windows.SizeChangedEventArgs;


namespace Sketchball.Controls
{
    public class WPFContainer : ElementHost
    {
        private ManagedWPFControl Control;
        private bool updating = false;

        public WPFContainer(ManagedWPFControl control)
        {
            // Bugfix: When setting child <- control, the control will lose its dimensions.
            double width = control.Width;
            double height = control.Height;

            Child = Control = control;

            control.Width = width;
            control.Height = height;

            Control.SizeChanged += OnChildSizeChanged;

            Disposed += (s,e) => {
                Control.Exit();
            };
        }

        private void OnChildSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Dock != DockStyle.Fill)
            {
                updating = true;
                this.Width = (int)Control.Width;
                this.Height = (int)Control.Height;
                updating = false;
            }
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
