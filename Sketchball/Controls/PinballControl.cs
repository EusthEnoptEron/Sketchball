using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.Controls
{
    abstract class PinballControl : Control
    {
        //private Bitmap BackgroundBitmap = null;
        //private Graphics BackgroundBuffer = null;
        //private Graphics ForegroundBuffer = null;

        protected PinballControl() : base()
        {
            Paint += PinballControl_Paint;
        }

        protected PinballControl(PinballMachine machine)
            : base()
        {
            Paint += PinballControl_Paint;
        }

        private void PinballControl_Paint(object sender, PaintEventArgs e)
        {
            ConfigureGDI(e.Graphics);
            Draw(e.Graphics);
        }

        /// <summary>
        /// Draws the pinball control.
        /// </summary>
        /// <param name="g"></param>
        protected abstract void Draw(Graphics g);

        protected virtual void ConfigureGDI(Graphics g)
        {
            // Go for quality in BG buffer and for performance in FG buffer.
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            
        }


    }
}
