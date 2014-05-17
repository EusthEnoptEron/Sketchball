using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.Controls
{
    /// <summary>
    /// Control that houses a game of pinball.
    /// </summary>
    class GameView : PinballControl
    {

        /// <summary>
        /// The absolute minimum of FPS at any point in time.
        /// </summary>
        private const int MIN_FPS = 10;

        private Camera Camera;
        private GameHUD HUD;

        protected override void ConfigureGDI(Graphics g)
        {
            base.ConfigureGDI(g);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }


        public Game Game;

        /// <summary>
        /// Creates a new PinballGameControl based on a machine template.
        /// </summary>
        /// <param name="machine">Template for the game machine.</param>
        public GameView(Game game)
            : base()
        {
            Game = game;
            Camera = new GameFieldCamera(Game);
            this.MinimumSize = new Size(((GameFieldCamera)(Camera)).getMinSize().Width,((GameFieldCamera)(Camera)).getMinSize().Height);

            HUD = new GameHUD(Game);

            // Init camera
            Camera.Size = Size;

            // Optimize control for performance
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            
            HandleCreated += PinballGameControl_HandleCreated;
            KeyDown += HandleKeyDown;
            Resize += ResizeCamera;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Camera.Size = Size;
            Invalidate();
            base.OnSizeChanged(e);
        }

        private void ResizeCamera(object sender, EventArgs e)
        {
            Camera.Size = Size;
            Invalidate();
        }


        /// <summary>
        /// Handles key presses (used to initiate a new game)
        /// </summary>
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (!Game.IsRunning)
                {
                    Game.Start();
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (Game.Status == GameStatus.Playing)
                {
                    Game.Pause();
                }
                else if (Game.Status == GameStatus.Pause)
                {
                    Game.Resume();
                }
            }
        }


        /// <summary>
        /// Initializes the game loop.
        /// </summary>
        private void PinballGameControl_HandleCreated(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            
            worker.DoWork += DrawCycle;
            worker.RunWorkerAsync();
        }


        /// <summary>
        /// Method that repeatedly draws and updates the scene.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawCycle(object sender, DoWorkEventArgs e)
        {
            DateTime prev = DateTime.Now;
            DateTime now;

            int counter = 1;
            while (true)
            {
                if (!IsHandleCreated) return;
                now = DateTime.Now;

                if (Game.Status == GameStatus.Playing || counter-- > 0)
                {
                    // Make sure that we draw the scene once more after status change
                    if (Game.Status == GameStatus.Playing) counter = 1;


                    // Redraw scene
                    IAsyncResult result = BeginInvoke(new Action(
                        () =>
                        {
                            Invalidate();
                            base.Update();
                        }
                    ));

                    EndInvoke(result);

                    // Give some time for input processing
                    Thread.Sleep(1);
                }
                else
                {
                    Thread.Sleep(10);
                }

                prev = now;
            }
        }
       

        protected override void Draw(Graphics g)
        {
            // Draw pinball machine
            Camera.Draw(g);
           
            g.TranslateTransform(Width - HUD.Width, 0);
            HUD.Draw(g);
            g.TranslateTransform(-(Width - HUD.Width), 0);

            if (Game.Status == GameStatus.GameOver)
            {
                DrawOverlay(g, Color.DarkRed, "YOU LOSE", "Press [SPACE] to try again.");
            }
            else if (Game.Status == GameStatus.Pause)
            {
                DrawOverlay(g, Color.DarkBlue, "PAUSED", "Press [ENTER] to resume.");
            }
        }

        private void DrawOverlay(Graphics g, Color color, string title, string msg)
        {
            using (Brush brush = new SolidBrush(Color.FromArgb(150, color)))
            {
                using (Brush solidBrush = new SolidBrush(color))
                {
                    g.FillRectangle(brush, 0, 0, Width, Height);
                    SizeF size = g.MeasureString(title, new Font("Impact", 40, FontStyle.Regular));

                    g.DrawString(title, new Font("Impact", 40, FontStyle.Regular), solidBrush, new PointF(Width / 2 - size.Width / 2, Height / 2 - size.Height / 2));
                    g.DrawString(msg, new Font("Arial", 13, FontStyle.Regular), solidBrush, new PointF(Width / 2 - size.Width / 2, Height / 2 + size.Height / 2));
                }
            }
        }
    }
}
