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
            control.PreviewMouseDown += delegate
            {
                if (!control.IsFocused)
                {
                    control.Focus();
                }
            };

            AutoSize = true;
            SetAutoSizeMode(AutoSizeMode.GrowAndShrink);

            Disposed += (s,e) => {
                Control.Exit();
            };
        }
    }
}
