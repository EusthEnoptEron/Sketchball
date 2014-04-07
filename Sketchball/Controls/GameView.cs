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


        protected override void ConfigureGDI(Graphics g)
        {
            base.ConfigureGDI(g);
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

            // Optimize control for performance
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            
            HandleCreated += PinballGameControl_HandleCreated;
            KeyUp += HandleKeyUp;
        }


        /// <summary>
        /// Handles key presses (used to initiate a new game)
        /// </summary>
        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (!Game.IsRunning)
                {
                    Game.Start();
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

            while (true)
            {
                now = DateTime.Now;

                // Calculate delta since last update
                // (make sure it reaches at least MIN_FPS)
                long delta = Math.Min(
                    1000 / MIN_FPS,
                    (long)(now - prev).TotalMilliseconds
                );

                // Update scene
                if(Game.Status != GameStatus.Pause)
                    Game.Update(delta);

                // Redraw scene
                IAsyncResult result = BeginInvoke(new Action(
                    () =>
                    {
                        Invalidate();
                        base.Update();
                    }
                ));
                EndInvoke(result);

                Thread.Sleep(10);
                prev = now;
            }
        }
       

        protected override void Draw(Graphics g)
        {
            // Draw pinball machine
            Camera.Draw(g, Bounds);

            // Draw HUD
            DrawHUD(g);

            if (Game.Status == GameStatus.GameOver)
            {
                DrawOverlay(g);
            }
        }

        private void DrawOverlay(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color.FromArgb(150, Color.DarkRed)))
            {
                g.FillRectangle(brush, 0, 0, Width, Height);
                SizeF size = g.MeasureString("YOU LOSE", new Font("Impact", 40, FontStyle.Regular));

                g.DrawString("YOU LOSE", new Font("Impact", 40, FontStyle.Regular), Brushes.DarkRed, new PointF(Width / 2 - size.Width / 2, Height / 2 - size.Height / 2));
                g.DrawString("Press [SPACE] to try again.", new Font("Arial", 13, FontStyle.Regular), Brushes.DarkRed, new PointF(Width / 2 - size.Width / 2, Height / 2 + size.Height / 2));
            }
        }

        private void DrawHUD(Graphics g)
        {
            string str = "Score: ";
            SizeF size = g.MeasureString(str, SystemFonts.DefaultFont);
            g.DrawString(str, SystemFonts.DefaultFont, Brushes.Black, Width - 200, 50);
            g.DrawString(Game.Score.ToString(), SystemFonts.DefaultFont, Brushes.Black, Width - 200 + size.Width, 50);

            str = "Lives: ";
            size = g.MeasureString(str, SystemFonts.DefaultFont);
            g.DrawString(str, SystemFonts.DefaultFont, Brushes.Black, Width - 200, 50 + size.Height);
            g.DrawString(Game.Lives.ToString(), SystemFonts.DefaultFont, Brushes.Black, Width - 200 + size.Width, 50 + size.Height);

        }
    }
}
