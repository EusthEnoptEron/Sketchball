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
        protected GameWorld World;

        //private Bitmap BackgroundBitmap = null;
        //private Graphics BackgroundBuffer = null;
        //private Graphics ForegroundBuffer = null;

        protected PinballControl() : base()
        {
            World = new GameWorld(new Size(500, 500));
            Paint += PinballControl_Paint;
        }

        private void PinballControl_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        /// <summary>
        /// Draws the pinball control.
        /// </summary>
        /// <param name="g"></param>
        protected virtual void Draw(Graphics g)
        {
            ConfigureGDI(g);
            World.Draw(g);
        }

        protected virtual void ConfigureGDI(Graphics g)
        {
            // Go for quality in BG buffer and for performance in FG buffer.
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
        }


    }
}
