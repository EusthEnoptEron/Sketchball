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

namespace Sketchball.Controls
{
    public partial class ElementControl : UserControl
    {
        PinballElement Element;
        Font ElementFont;
        public ElementControl(PinballElement el, Font font)
        {
            ElementFont = font;
            Element = el;
            InitializeComponent();
            Paint += UserControl1_Paint;

            Height = 50;
            Width = 200;
        }

        void UserControl1_Paint(object sender, PaintEventArgs e)
        {
            //Image image = Image.FromFile(@"D:\Studium\Semester 4\Project 1\Graphic\Conseptual\Slingshot.png");
            //e.Graphics.DrawImage(image,0 ,0,Width, Height);
            Element.Width = Element.Height = 50;

            Element.Draw(e.Graphics);
            e.Graphics.DrawString("Ball", ElementFont, Brushes.Black, 60, 10);
        }


       public Image GetImage() {
           return null;
           Bitmap bm = new Bitmap(Element.Width, Element.Height);
         //  Graphics g = bm.




           
           
        }

        
    }
}
