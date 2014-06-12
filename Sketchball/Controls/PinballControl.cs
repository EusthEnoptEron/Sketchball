using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace Sketchball.Controls
{
    /// <summary>
    /// Represents a control that houses a pinball machine.
    /// </summary>
    public abstract class PinballControl : ManagedWPFControl
    {
        private int fps_debug = 0;
        private DateTime prev = DateTime.MinValue;
        
       
        protected PinballControl() : base()
        {
            Width = 50;
            Height = 50;
        }

        protected PinballControl(PinballMachine machine)
            : base()
        {
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            //DateTime now = DateTime.Now;
            //TimeSpan delta = prev == DateTime.MinValue
            //    ? new TimeSpan(0)
            //    : now - prev;
            //prev = now;

             Draw(drawingContext);

            //this.fps_debug = (int)(1 / delta.TotalSeconds);
        }

        /// <summary>
        /// Draws the pinball control.
        /// </summary>
        /// <param name="g"></param>
        protected abstract void Draw(DrawingContext g);

    }
}
