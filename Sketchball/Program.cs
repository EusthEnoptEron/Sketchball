using Sketchball.Controls;
using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

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

            Form f = new Form();

            
           /* Experiment exp = new Experiment();
            var ctrlHost = new WPFContainer(exp);
            //exp.InitializeComponent();

            f.Controls.Add(ctrlHost);
            ctrlHost.Dock = DockStyle.Fill;

            //f.FormClosing += (s, e) => { exp.Exit(); };
            */
            //Application.Run(f);
            
            Application.Run(new SelectionForm());
            //Application.Run(new PlayForm(new PinballMachine()) { Width = 800, Height = 700 });

        }
    }
}