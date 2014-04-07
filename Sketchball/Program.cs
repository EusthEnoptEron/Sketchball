using Sketchball.Controls;
using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Form f = new Form();
            //PinballControl2 pinball = new PinballControl2();

            //f.Controls.Add(pinball);
            //pinball.Dock = DockStyle.Fill;
            //f.Width = 500;
            //f.Height = 500;


            Application.Run(new SelectionForm());
            //Application.Run(new PlayForm(new PinballMachine(new Size(470, 600))) { Width = 800, Height = 700 });

        }
    }
}