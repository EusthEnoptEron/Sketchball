using Sketchball.Controls;
using Sketchball.Elements;
using Sketchball.GameComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vector = System.Windows.Vector;

namespace Sketchball
{
    
    /// <summary>
    /// Form that houses the actual game of pinball.
    /// </summary>
    public partial class PlayForm : Form
    {
        private Game game;
        private GameView gameView;
        private WPFContainer gameContainer;
        private SelectionForm selectionForm = null;
        private PinballMachine originalMachine = null;

        private string fileName = null;


        public PlayForm(PinballMachine pbm) : this(pbm, null)
        {
        }

        public PlayForm(PinballMachine pbm, SelectionForm selectionForm)
        {
            InitializeComponent();

            // Initialize game
            originalMachine = pbm;
            game = new Game(pbm, Environment.UserName);
            gameView = new GameView(game);
            gameContainer = new WPFContainer(gameView);
            game.GameOver += onGameOver;
            //this.MinimumSize = gameView.MinimumSize;
            gameView.MouseUp += onMouseUp;
            gameView.MouseMove += onMouseMove;
            // Fill entire space
            gameContainer.Dock = DockStyle.Fill;

            mainContainer.ContentPanel.Controls.Add(gameContainer);

            this.selectionForm = selectionForm;
        
            debugModeButton.Checked =  Properties.Settings.Default.Debug;
            
        }

        private void onMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isFullScreen())
            {
                var pos = e.GetPosition(gameView);
                mainContainer.TopToolStripPanelVisible = pos.Y < 50;
            }
        }

        /// <summary>
        /// If activated, the game will track the high score and keep the file updated.
        /// </summary>
        /// <param name="fileName"></param>
        public void ActivateScoreTracking(string fileName)
        {
            var info = new FileInfo(fileName);
            if (!info.IsReadOnly && info.Directory.Exists)
            {
                this.fileName = info.FullName;
            }
            else
            {
                throw new AccessViolationException("File not writable.");
            }

        }

        public void DeactivateScoreTracking()
        {
            fileName = null;
        }


        #region Event Handler

        private void onGameOver(object sender, int score)
        {
            if (fileName != null)
            {
                // Let's track the changes!
                try
                {
                    originalMachine.Save(fileName);
                }
                catch (SerializationException)
                {
                    MessageBox.Show("Could not save your score", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void onMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
            if (Properties.Settings.Default.Debug)
            {
                game.Machine.IntroduceBall();
                game.Machine.Balls.Last().Location = new Vector(e.GetPosition((System.Windows.IInputElement)sender).X, e.GetPosition((System.Windows.IInputElement)sender).Y);
            }
            
        }

        private void onPauseClicked(object sender, EventArgs e)
        {
            if(game.Status == GameStatus.Playing)
            {
                game.Pause();
            }
        }

        private void onResumeClicked(object sender, EventArgs e)
        {
            if (game.Status == GameStatus.Paused)
            {
                game.Resume();
            }
        }

        private void onFileMenuOpening(object sender, EventArgs e)
        {
            // Make sure that the edit button is only visible when the editor
            // isn't already open
            if (selectionForm == null)
            {
                editToolStripMenuItem.Visible = false;
                closeMachineButton.Visible = false;
            }

            // Make sure the Resume / Pause buttons are correctly configured
            if (game.Status == GameStatus.Paused)
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

        private void onResetClicked(object sender, EventArgs e)
        {
            var statusChanged = game.Status != GameStatus.Paused;
            game.Pause();

            var result = MessageBox.Show("Are you sure that you want to reset the game?", "Reset",
                             MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
                game.Start();
            else if(statusChanged)
                game.Resume();
        }

        private void onExitClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void onCloseMachine(object sender, EventArgs e)
        {
            selectionForm.CloseManagedForm();
        }

        private void onEditClicked(object sender, EventArgs e)
        {
            game.Pause();

            var result = MessageBox.Show("Are you sure that you want to open the editor? Your game will be lost!", "Switch into editor",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                selectionForm.OpenEditor(originalMachine);
            }
        }

        private void onDebugModeChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Debug = debugModeButton.Checked;
            Properties.Settings.Default.Save();
        }

        #endregion


        private void enterFullscreen()
        {
            SuspendLayout();
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            mainContainer.TopToolStripPanelVisible = false;
            ResumeLayout();
        }

        private void leaveFullscreen()
        {
            SuspendLayout();
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            WindowState = FormWindowState.Normal;

            mainContainer.TopToolStripPanelVisible = true;
            ResumeLayout();
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            game.Dispose();
            gameView.MouseUp -= onMouseUp;

            base.Dispose(disposing);
        }

        private void onSwitchFullscreen(object sender, EventArgs e)
        {
            if (isFullScreen())
            {
                leaveFullscreen();
            }
            else
            {
                enterFullscreen();
            }
        }

        private bool isFullScreen()
        {
            return FormBorderStyle == System.Windows.Forms.FormBorderStyle.None;
        }
    }
}
