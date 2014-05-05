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
    public partial class ElementControl : UserControl
    {
        private const int THUMB_WIDTH = 50;
        private const int THUMB_HEIGHT = 50;


        PinballElement Element;
        Font ElementFont;
        string Label;

        public ElementControl(PinballElement el, string label, Font font)
        {
            ElementFont = font;
            Element = el;
            Label = label;
            InitializeComponent();


            Paint += (s, e) => { Draw(e.Graphics); };
            //MouseDown += (s, e) => { BackColor = SystemColors.Highlight; };
            MouseEnter += (s, e) => { BackColor = SystemColors.Highlight; ForeColor = Color.White; };
            MouseLeave += (s, e) => { BackColor = SystemColors.Control; ForeColor = Color.Black; };

            Height = THUMB_HEIGHT;
            Width = 260;
        }

        void Draw(Graphics g)
        {
            Brush bgBrush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Gray, Color.LightGray);
            g.FillRectangle(bgBrush, 0, 0, THUMB_WIDTH, THUMB_HEIGHT);
            DrawThumb(g, THUMB_WIDTH, THUMB_HEIGHT);

            g.DrawString(Label, ElementFont, new SolidBrush(ForeColor), THUMB_WIDTH + 5, 10);
        }

        private void DrawThumb(Graphics g, int width, int height)
        {
            if (Element.Width == 0) Element.Width = width;
            if (Element.Height == 0) Element.Height = height;

            var heightRatio = (float)height / Element.Height;
            var widthRatio = (float)width / Element.Width;
            var ratio = Math.Min(heightRatio, widthRatio);

            GraphicsState state = g.Save();
            try
            {
                
                g.ScaleTransform(ratio, ratio);
                
                if (heightRatio < widthRatio)
                {
                    g.TranslateTransform(width - (Element.Width * ratio), 0);
                }
                else
                {
                    g.TranslateTransform(0, height - (Element.Height * ratio));
                }

                Element.Draw(g);
            }
            finally
            {
                g.Restore(state);
            }
        }

        public Image GetImage(int width = THUMB_WIDTH, int height = THUMB_HEIGHT) {
           Bitmap bm = new Bitmap(width, height);

           using (Graphics g = Graphics.FromImage(bm))
           {
               DrawThumb(g, width, height);
           }

           return bm;
        }

        public PinballElement GetInstance()
        {
            return (PinballElement)Element.Clone();
        }
    }
}
