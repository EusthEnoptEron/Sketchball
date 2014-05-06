using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sketchball.Elements;
using System.Drawing.Drawing2D;

namespace Sketchball.Controls
{
    /// <summary>
    /// A control that is used to create new PinballElements. It's tasks are:
    /// 
    /// 1. Provide a control that shows a preview of an element and labels it
    /// 2. Provide a way to get a bitmap object of that preview
    /// 3. Create a new instance of an element on demand (we'll handle the drag&drop outside)
    /// </summary>
    public partial class ElementControl : UserControl
    {
        private const int THUMB_WIDTH = 50;
        private const int THUMB_HEIGHT = 50;


        PinballElement Element;
        Font ElementFont;
        string Label;

        /// <summary>
        /// Creates a new ElementControl that can create clones of el.
        /// </summary>
        /// <param name="el">Element that should be cloned.</param>
        /// <param name="label">Label for the element.</param>
        /// <param name="font">Font used for the label.</param>
        public ElementControl(PinballElement el, string label, Font font)
        {
            InitializeComponent();

            // Assign instance vars
            ElementFont = font;
            Element = el;
            Label = label;
            Height = THUMB_HEIGHT;
            Width = 260;

            // Add event listeners
            Paint += (s, e) => { Draw(e.Graphics); };
            MouseEnter += (s, e) => { BackColor = SystemColors.Highlight; ForeColor = Color.White; };
            MouseLeave += (s, e) => { BackColor = SystemColors.Control; ForeColor = Color.Black; };
        }

        
        /// <summary>
        /// Draws the control.
        /// </summary>
        /// <param name="g"></param>
        private void Draw(Graphics g)
        {
            Brush bgBrush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Gray, Color.LightGray);
            g.FillRectangle(bgBrush, 0, 0, THUMB_WIDTH, THUMB_HEIGHT);
            DrawThumb(g, THUMB_WIDTH, THUMB_HEIGHT);

            g.DrawString(Label, ElementFont, new SolidBrush(ForeColor), THUMB_WIDTH + 5, 10);
        }

        /// <summary>
        /// Draws the thumbnail part of the control.
        /// </summary>
        /// <param name="g">Graphics object to draw to</param>
        /// <param name="width">Max width of the thumb</param>
        /// <param name="height">Max height of the thumb</param>
        private void DrawThumb(Graphics g, int width, int height)
        {
            // Prevent division by zero
            if (Element.Width == 0) Element.Width = width;
            if (Element.Height == 0) Element.Height = height;

            // Calculate ratios
            var heightRatio = (float)height / Element.Height;
            var widthRatio = (float)width / Element.Width;
            var ratio = Math.Min(heightRatio, widthRatio);

            GraphicsState state = g.Save();
            try
            {
                g.IntersectClip(new Rectangle(0, 0, width, height));

                // Scale to the right dimension
                g.ScaleTransform(ratio, ratio);
                
                // Move to center
                if (heightRatio < widthRatio)
                    g.TranslateTransform(width - (Element.Width * ratio), 0);
                else
                    g.TranslateTransform(0, height - (Element.Height * ratio));

                // Draw
                Element.Draw(g);
            }
            finally
            {
                g.Restore(state);
            }
        }

        /// <summary>
        /// Returns the thumbnail image for use outside of the control. 
        /// </summary>
        /// <param name="width">Required width of the bitmap</param>
        /// <param name="height">Required height of the bitmap</param>
        /// <returns>The bitmap depicting the thumbnail.</returns>
        public Image GetImage(int width = THUMB_WIDTH, int height = THUMB_HEIGHT) {
           Bitmap bm = new Bitmap(width, height);

           using (Graphics g = Graphics.FromImage(bm))
           {
               DrawThumb(g, width, height);
           }

           return bm;
        }

        /// <summary>
        /// Creates a new instance of the element linked to this control.
        /// </summary>
        /// <returns></returns>
        public PinballElement GetInstance()
        {
            return (PinballElement)Element.Clone();
        }
    }
}
