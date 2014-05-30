using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sketchball.Elements;
using System.Drawing.Drawing2D;
using System.Windows.Media;
using System.Windows;

using Graphics = System.Drawing.Graphics;
using Image = System.Drawing.Image;
using Color = System.Drawing.Color;
using Brush = System.Drawing.Brush;
using Font = System.Drawing.Font;

namespace Sketchball.Controls
{
    /// <summary>
    /// A control that is used to create new PinballElements. Its tasks are:
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
        private Image thumb;


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
            Width = 200;

            // Add event listeners
            MouseEnter += (s, e) => { BackColor = System.Drawing.SystemColors.Highlight; ForeColor = Color.White; };
            MouseLeave += (s, e) => { BackColor = System.Drawing.SystemColors.Control; ForeColor = Color.Black; };

            thumb = GetImage();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        
        /// <summary>
        /// Draws the control.
        /// </summary>
        /// <param name="g"></param>
        private void Draw(Graphics g)
        {
            Brush bgBrush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Gray, Color.LightGray);
            g.FillRectangle(bgBrush, 0, 0, THUMB_WIDTH, THUMB_HEIGHT);

            g.DrawImage(thumb, 0, 0);
            g.DrawString(Label, ElementFont, new System.Drawing.SolidBrush(ForeColor), THUMB_WIDTH + 5, 10);
        }

        /// <summary>
        /// Draws the thumbnail part of the control.
        /// </summary>
        /// <param name="g">Graphics object to draw to</param>
        /// <param name="width">Max width of the thumb</param>
        /// <param name="height">Max height of the thumb</param>
        private void DrawThumb(DrawingContext g, int width, int height)
        {
            // Prevent division by zero
            if (Element.Width == 0) Element.Width = width;
            if (Element.Height == 0) Element.Height = height;

            // Calculate ratios
            var heightRatio = height / Element.Height;
            var widthRatio = width / Element.Width;
            var ratio = Math.Min(heightRatio, widthRatio);

            g.PushClip(new RectangleGeometry(new Rect(0, 0, width, height)));
            
            // Scale to the right dimension
            g.PushTransform(new ScaleTransform(ratio, ratio));
                
            // Move to center
            if (heightRatio < widthRatio)
                g.PushTransform( new TranslateTransform(width - (Element.Width * ratio), 0));
            else
                g.PushTransform( new TranslateTransform(0, height - (Element.Height * ratio)));

            // Draw
            Element.Draw(g);

            g.Pop(); // Translate
            g.Pop(); // Scale
            g.Pop(); // Clip
        }

        /// <summary>
        /// Returns the thumbnail image for use outside of the control. 
        /// </summary>
        /// <param name="width">Required width of the bitmap</param>
        /// <param name="height">Required height of the bitmap</param>
        /// <returns>The bitmap depicting the thumbnail.</returns>
        public Image GetImage(int width = THUMB_WIDTH, int height = THUMB_HEIGHT) {
            var drawing = new DrawingGroup();
            drawing.SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.HighQuality);
            using (var context = drawing.Open())
            {
                DrawThumb(context, width, height);
            }

            return Booster.DrawingToBitmap(drawing, width, height);
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
