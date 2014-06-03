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
    public abstract class PinballControl : ManagedWPFControl
    {
        //private Bitmap BackgroundBitmap = null;
        //private Graphics BackgroundBuffer = null;
        //private Graphics ForegroundBuffer = null;
        private int fps_debug = 0;
        private DateTime prev = DateTime.MinValue;
        
       
        protected PinballControl() : base()
        {
            Width = 50;
            Height = 50;

            // Make sure that the control receives keyboard events!
            // @see http://social.msdn.microsoft.com/Forums/vstudio/en-US/110ac6b2-949a-470d-8f47-acc804976994/wpf-canvas-usercontrol-embedded-inside-windows-form-doesnt-receive-keyboard-events?forum=wpf
            Focusable = true;
           
            MouseDown += delegate {
                this.Focus();
            };
        }

        protected PinballControl(PinballMachine machine)
            : base()
        {
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var text = new FormattedText("fps" + this.fps_debug.ToString(),
              CultureInfo.GetCultureInfo("en-us"),
              System.Windows.FlowDirection.LeftToRight,
              new Typeface("Arial"),
              36, System.Windows.Media.Brushes.BlueViolet);

            base.OnRender(drawingContext);
            DateTime now = DateTime.Now;
            TimeSpan delta = prev == DateTime.MinValue
                ? new TimeSpan(0)
                : now - prev;
            prev = now;

            Draw(drawingContext);
            drawingContext.DrawText(text, new System.Windows.Point(400, 400));


            this.fps_debug = (int)(1 / delta.TotalSeconds);
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
           // e.Graphics.DrawString("fps" + this.fps_debug.ToString(), new Font("Arial", 12), Brushes.BlueViolet, new PointF(400, 400));
            this.fps_debug = (int)(1 / delta.TotalSeconds);
        }

        /// <summary>
        /// Draws the pinball control.
        /// </summary>
        /// <param name="g"></param>
        protected virtual void Draw(Graphics g) { }
        protected abstract void Draw(DrawingContext g);

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
