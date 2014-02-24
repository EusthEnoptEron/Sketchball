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

        private Bitmap BackgroundBitmap = null;
        private Graphics BackgroundBuffer = null;
        private Graphics ForegroundBuffer = null;

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
        protected void Draw(Graphics g)
        {

            //Brush brush = new HatchBrush(HatchStyle.WideDownwardDiagonal, Color.Gray, Color.LightGray);
            Brush brush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Gray, Color.DarkGray);
            g.FillRectangle(brush, 0, 0, base.Width, base.Height);

            World.Draw(g);
        }


        private void PrepareBuffers()
        {
            if (BackgroundBitmap != null) BackgroundBitmap.Dispose();

            BackgroundBitmap = new Bitmap(Width, Height);
            BackgroundBuffer = Graphics.FromImage(BackgroundBitmap);
            ForegroundBuffer = CreateGraphics();


            ConfigureBuffers(BackgroundBuffer, ForegroundBuffer);
        }

        protected virtual void ConfigureBuffers(Graphics bg, Graphics fg)
        {
            // Go for quality in BG buffer and for performance in FG buffer.
            bg.CompositingQuality = CompositingQuality.HighQuality;
            bg.SmoothingMode = SmoothingMode.AntiAlias;
        }


    }
}
