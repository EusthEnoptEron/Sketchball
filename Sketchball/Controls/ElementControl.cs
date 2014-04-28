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

            Height = 50;
            Width = 200;
        }

        void Draw(Graphics g)
        {
            //Image image = Image.FromFile(@"D:\Studium\Semester 4\Project 1\Graphic\Conseptual\Slingshot.png");
            //e.Graphics.DrawImage(image,0 ,0,Width, Height);

            DrawThumb(g, THUMB_WIDTH, THUMB_HEIGHT);

            g.DrawString(Label, ElementFont, Brushes.Black, THUMB_WIDTH + 5, 10);
            g.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
        }

        private void DrawThumb(Graphics g, int width, int height)
        {
            if (Element.Width == 0) Element.Width = width;
            if (Element.Height == 0) Element.Height = height;

            var factor = Math.Min((float)width / Element.Width, (float)height / Element.Height);

            GraphicsState state = g.Save();
            try
            {
                g.ScaleTransform(factor, factor);
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
