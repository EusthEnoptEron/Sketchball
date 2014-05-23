using Sketchball.Controls;
using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball
{
    /// <summary>
    /// Form that houses the actual game of pinball.
    /// </summary>
    public partial class PlayForm : Form
    {
        private Game game;
        private GameView gameView;
        private SelectionForm selectionForm = null;
        private PinballMachine originalMachine = null;
        public PlayForm(PinballMachine pbm) : this(pbm, null)
        {
        }

        public PlayForm(PinballMachine pbm, SelectionForm selectionForm)
        {
            InitializeComponent();

            // Initialize game
            originalMachine = pbm;
            game = new Game(pbm);
            gameView = new GameView(game);
            this.MinimumSize = gameView.MinimumSize;
            gameView.MouseUp += OnMouseUp;

            // Fill entire space
            gameView.Dock = DockStyle.Fill;
            gameView.Anchor = AnchorStyles.Top | AnchorStyles.Left;
           
            Controls.Add(gameView);

            this.selectionForm = selectionForm;

            debugModeButton.Checked = Properties.Settings.Default.Debug;
            this.gameView.Camera.backgroundManager.backgroundChanged +=backgroundManager_backgroundChanged;
        }

        ~PlayForm()
        {
            this.gameView.Camera.backgroundManager.backgroundChanged -= backgroundManager_backgroundChanged;
        }

        private void backgroundManager_backgroundChanged(object sender, EventArgs e)
        {
            int width = (this).Width;
            int height = this.Height;
            Bitmap inUse = this.gameView.Camera.backgroundManager.Background_TableBackground;

            if (width > inUse.Width || height > inUse.Height)
            {
                float ratW = width * 1f / inUse.Width;
                float ratH = height * 1f / inUse.Height;

                if (ratW > ratH)
                {
                    width = (int)(inUse.Width * ratW);
                    height = (int)(inUse.Height * ratW);
                }
                else
                {
                    width = (int)(inUse.Width * ratH);
                    height = (int)(inUse.Height * ratH);
                }
            }
            else
            {
                width = inUse.Width;
                height = inUse.Height;
            }


            Bitmap m = Booster.OptimizeImage(inUse, width, height);
            this.BackgroundImage = m;
        }

        protected override void OnPaintBackground(PaintEventArgs e) 
        {
            base.OnPaintBackground(e);
            var rc = new Rectangle((this.ClientSize.Width - this.BackgroundImage.Width)/2, (this.ClientSize.Height - this.BackgroundImage.Height)/2, this.BackgroundImage.Width, this.BackgroundImage.Height);
            e.Graphics.DrawImage(this.BackgroundImage, rc);
        }


        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(game.Status == GameStatus.Playing)
            {
                game.Pause();
            }
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (game.Status == GameStatus.Pause)
            {
                game.Resume();
            }
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            // Make sure that the edit button is only visible when the editor
            // isn't already open
            if (selectionForm == null)
            {
                editToolStripMenuItem.Visible = false;
            }

            // Make sure the Resume / Pause buttons are correctly configured
            if (game.Status == GameStatus.Pause)
            {
                resumeToolStripMenuItem.Visible = true;
                pauseToolStripMenuItem.Visible = false;
            }
            else
            {
                pauseToolStripMenuItem.Visible = true;
                resumeToolStripMenuItem.Visible = false;

                if (game.Status == GameStatus.Playing)
                {
                    pauseToolStripMenuItem.Enabled = true;
                }
                else
                {
                    pauseToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var statusChanged = game.Status != GameStatus.Pause;
            game.Pause();

            var result = MessageBox.Show("Are you sure that you want to reset the game?", "Reset",
                             MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
                game.Start();
            else if(statusChanged)
                game.Resume();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.Pause();

            var result = MessageBox.Show("Are you sure that you want to open the editor? Your game will be lost!", "Switch into editor",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                selectionForm.OpenEditor(originalMachine);
            }
        }

        private void debugModeButton_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Debug = debugModeButton.Checked;
            Properties.Settings.Default.Save();
        }


        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (Properties.Settings.Default.Debug && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                game.Machine.IntroduceBall();
                game.Machine.Balls.Last().Location = new Vector2(e.X * 0.4f, e.Y * 0.4f);
            }
        }

        private void PlayForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            game.Dispose();
        }

        private void PlayForm_ResizeEnd(object sender, EventArgs e)
        {
            int width = ((Control)sender).Width;
            int height = ((Control)sender).Height;
            Bitmap inUse = this.gameView.Camera.backgroundManager.Background_TableBackground;
            Image i = Properties.Resources.TableBackground;

            if ((height <= i.Height && width <= i.Width))
            {
                return;
            }

            float ratW = width * 1f / inUse.Width;
            float ratH = height * 1f / inUse.Height;

            if (ratW > ratH)
            {
                height = (int)((this.BackgroundImage.Height * 1f / this.BackgroundImage.Width) * width);
            }
            else
            {
                width = (int)((this.BackgroundImage.Width * 1f / this.BackgroundImage.Height) * height);
            }

            Bitmap m = Booster.OptimizeImage(inUse, width, height);
            this.BackgroundImage = m;
            
        }

    }
}
