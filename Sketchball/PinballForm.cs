using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball
{
    public abstract class PinballForm : Form
    {
        protected PinballForm()
        {
            Load += InitPinballForm;
        }

        void InitPinballForm(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Fullscreen)
            {
                EnterFullscreen();
            }

            KeyPreview = true;
            KeyDown += PinballForm_KeyDown;
        }

        private void PinballForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Properties.Settings.Default.Fullscreen = !Properties.Settings.Default.Fullscreen;
                Properties.Settings.Default.Save();

                if (Properties.Settings.Default.Fullscreen)
                {
                    EnterFullscreen();
                }
                else
                {
                    LeaveFullscreen();
                }
            }
        }

        protected void EnterFullscreen()
        {
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        protected void LeaveFullscreen()
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;
        }
    }
}
