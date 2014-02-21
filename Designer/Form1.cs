using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PinballSimulator;

namespace Designer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

     

        private void pinball1_Click(object sender, MouseEventArgs e)
        {
            //Point loc = pinball1.PointToClient(MousePosition);

            //Ball ball = new Ball() { Location = loc };
            //ball.X -= ball.Width / 2;
            //ball.Y -= ball.Height / 2;

            //pinball1.Elements.Add(ball);
        }
    }
}
