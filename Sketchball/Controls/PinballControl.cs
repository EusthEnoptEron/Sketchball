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
    public abstract class PinballControl : Control
    {
        //private Bitmap BackgroundBitmap = null;
        //private Graphics BackgroundBuffer = null;
        //private Graphics ForegroundBuffer = null;
        private int fps_debug = 0;
        private DateTime prev = DateTime.MinValue;
        
       
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
            DateTime now = DateTime.Now;
            TimeSpan delta = prev == DateTime.MinValue
                ? new TimeSpan(0)
                : now - prev;
            prev = now;

            ConfigureGDI(e.Graphics);
            Draw(e.Graphics);
            e.Graphics.DrawString("fps" + this.fps_debug.ToString(), new Font("Arial", 12), Brushes.BlueViolet, new PointF(400, 400));
            this.fps_debug = (int)(1 / delta.TotalSeconds);
        }

        /// <summary>
        /// Draws the pinball control.
        /// </summary>
        /// <param name="g"></param>
        protected abstract void Draw(Graphics g);

        protected virtual void ConfigureGDI(Graphics g)
        {


            // Go for quality in BG buffer and for performance in FG buffer
           
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            g.CompositingMode = CompositingMode.SourceOver;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

           
        }


    }
}
