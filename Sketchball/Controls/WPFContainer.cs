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
    /// <summary>
    /// Provides an interface between the GDI+ and the WPF world.s
    /// </summary>
    public class WPFContainer : ElementHost
    {
        private ManagedWPFControl control;

        public WPFContainer(ManagedWPFControl control)
        {
            // Bugfix #1: When setting child <- control, the control will lose its dimensions.
            double width = control.Width;
            double height = control.Height;

            Child = this.control = control;

            control.Width = width;
            control.Height = height;
            // --------------------------

            // Bugfix #2: In order to get key events, we _explicitly_ need to focus the child control.
            control.Focusable = true;
            control.PreviewMouseDown += onMouseDown;
          
            AutoSize = true;
            SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                // Workaround to fix... or alleviate a memory leak.
                control.Dispose();
                control.PreviewMouseDown -= onMouseDown;

                var fe = Child as System.Windows.FrameworkElement;
                if (fe != null)
                {
                    // Memory leak workaround: elementHost.Child.SizeChanged -= elementHost.childFrameworkElement_SizeChanged;
                    var handler = (System.Windows.SizeChangedEventHandler)Delegate.CreateDelegate(typeof(System.Windows.SizeChangedEventHandler), this, "childFrameworkElement_SizeChanged");
                    fe.SizeChanged -= handler;
                }

                control = null;
                Child = null;
            }
            base.Dispose(disposing);

        }

        private void onMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!control.IsFocused)
            {
                control.Focus();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.Dock == DockStyle.Fill)
            {
                control.Width = Width;
                control.Height = Height;
            }
        }
    }
}
