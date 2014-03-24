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
    class PinballGameControl : PinballControl
    {
        enum GameStatus
        {
            Setup,
            Playing,
            GameOver
        }

        public const int TOTAL_LIVES = 3;

        private GameStatus Status = GameStatus.Setup;

        private DateTime prev = DateTime.MinValue;

        /// <summary>
        /// Gets the score of the current game.
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// Gets the number of remaining lives of the current game.
        /// </summary>
        public int Lives { get; private set; }

        private PinballMachine OriginalMachine;


        public PinballGameControl(PinballMachine machine)
            : base()
        {
            OriginalMachine = machine;

            // Optimize control for performance
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);

            HandleCreated += PinballGameControl_HandleCreated;

            Start();
        }

        void PinballGameControl_HandleCreated(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += DrawCycle;
            worker.RunWorkerAsync();
        }

        void DrawCycle(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                IAsyncResult result = BeginInvoke(new Action(
                    () =>
                    {
                        Invalidate();
                    }
                ));
                EndInvoke(result);

                Thread.Sleep(10);
            }
        }
        
        /// <summary>
        /// Starts the game.
        /// </summary>
        public void Start()
        {
            Status = GameStatus.Playing;

            World = (PinballMachine)OriginalMachine.Clone();

            Score = 0;
            Lives = TOTAL_LIVES;

            // Wire up event handlers
            World.Collision += OnScore;
            World.GameOver += OnGameOver;

            World.IntroduceBall();
        }

        /// <summary>
        /// Add a new ball if needed when ball gets lost.
        /// </summary>
        private void OnGameOver()
        {
            if (Lives == 0)
            {
                // GameOver... :(
                Status = GameStatus.GameOver;
            }
            else
            {
                Lives--;
                World.IntroduceBall();
            }
        }

        /// <summary>
        /// Increment score when a collision happened.
        /// </summary>
        /// <param name="sender"></param>
        private void OnScore(PinballElement sender)
        {
            Score += sender.Value;
        }


        /// <summary>
        /// Pauses the game.
        /// </summary>
        public void Pause()
        {

        }


        /// <summary>
        /// Resumes the game if paused.
        /// </summary>
        public void Resume()
        {

        }

        /// <summary>
        /// Updates positions and checks for collisions, etc.
        /// </summary>
        protected new void Update()
        {
            // Update time
            TimeSpan elapsed;
            DateTime now = DateTime.Now;

            if (prev != DateTime.MinValue)
                elapsed = now - prev;
            else 
                elapsed = new TimeSpan();

            prev = now;
            
            // Update elements
            World.Update((long)elapsed.TotalMilliseconds);
        }


        protected override void Draw(Graphics g)
        {
            // Draw pinball machine
            base.Draw(g);

            // Draw HUD
            DrawHUD(g);

            if (Status == GameStatus.GameOver)
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
            g.DrawString(Score.ToString(), SystemFonts.DefaultFont, Brushes.Black, Width - 200 + size.Width, 50);

        }
    }
}
