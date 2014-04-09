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

            // Fill entire space
            gameView.Dock = DockStyle.Fill;

            Controls.Add(gameView);

            this.selectionForm = selectionForm;
        }

        private void PlayForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            game.Dispose();
            if (selectionForm != null)
            {
                selectionForm.Show();
            }
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
                this.Close();
                //selectionForm.OpenEditor(originalMachine);
            }
        }
    }
}
