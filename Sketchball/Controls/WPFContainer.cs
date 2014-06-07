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
            // Polyfill #1: When setting child <- control, the control will lose its dimensions.
            double width = control.Width;
            double height = control.Height;

            Child = Control = control;

            control.Width = width;
            control.Height = height;
            // --------------------------

            // Polyfill #2: In order to get key events, we _explicitly_ need to focus the child control.
            control.Focusable = true;
            control.PreviewMouseDown += onMouseDown;
          
            AutoSize = true;
            SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                Control.Exit();
                Control.PreviewMouseDown -= onMouseDown;

                var fe = Child as System.Windows.FrameworkElement;
                if (fe != null)
                {
                    // Memory leak workaround: elementHost.Child.SizeChanged -= elementHost.childFrameworkElement_SizeChanged;
                    var handler = (System.Windows.SizeChangedEventHandler)Delegate.CreateDelegate(typeof(System.Windows.SizeChangedEventHandler), this, "childFrameworkElement_SizeChanged");
                    fe.SizeChanged -= handler;
                }

                Control = null;
                Child = null;
            }
            base.Dispose(disposing);

        }

        private void onMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!Control.IsFocused)
            {
                Control.Focus();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.Dock == DockStyle.Fill)
            {
                Control.Width = Width;
                Control.Height = Height;
            }
        }
    }
}
