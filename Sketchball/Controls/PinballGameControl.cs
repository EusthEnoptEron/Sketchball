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
    class PinballGameControl : PinballControl
    {
        /// <summary>
        /// Phases the control can assume
        /// </summary>
        enum GameStatus
        {
            Setup,
            Playing,
            GameOver
        }

        /// <summary>
        /// Total number of lives (<=> balls)
        /// </summary>
        public const int TOTAL_LIVES = 3;

        /// <summary>
        /// The absolute minimum of FPS at any point in time.
        /// </summary>
        private const int MIN_FPS = 10;

        /// <summary>
        /// Gets or sets the current game status.
        /// </summary>
        private GameStatus Status = GameStatus.Setup;
        
        
        /// <summary>
        /// Gets or sets the machine currently displayed.
        /// </summary>
        private PinballGameMachine Machine;

        private Camera Camera = null;

        /// <summary>
        /// Gets the score of the current game.
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// Gets the number of remaining lives of the current game.
        /// </summary>
        public int Lives { get; private set; }

        /// <summary>
        /// Original machine from which the game machines are made.
        /// </summary>
        private PinballMachine OriginalMachine;

        protected override void ConfigureGDI(Graphics g)
        {
            base.ConfigureGDI(g);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }

        /// <summary>
        /// Creates a new PinballGameControl based on a machine template.
        /// </summary>
        /// <param name="machine">Template for the game machine.</param>
        public PinballGameControl(PinballMachine machine)
            : base()
        {
            OriginalMachine = machine;

            // Optimize control for performance
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            
            HandleCreated += PinballGameControl_HandleCreated;
            KeyUp += HandleKeyUp;

            Start();
        }


        /// <summary>
        /// Handles key presses (used to initiate a new game)
        /// </summary>
        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (!Machine.HasBall())
                {
                    Start();
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
                Update(delta);

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
        
        /// <summary>
        /// Starts the game.
        /// </summary>
        public void Start()
        {
            Status = GameStatus.Playing;

            Machine = new PinballGameMachine(OriginalMachine);
            Machine.prepareForLaunch();
            Camera = new PinballMachineCamera(Machine);

            Score = 0;
            Lives = TOTAL_LIVES;

            // Wire up event handlers
            Machine.Collision += OnScore;
            Machine.GameOver += OnGameOver;

            Machine.IntroduceBall();
            Lives--;
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
                Machine.IntroduceBall();
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
        public void Update(long elapsed)
        {            
            // Update elements
            Machine.Update(elapsed);
        }


        protected override void Draw(Graphics g)
        {
            // Draw pinball machine
            Camera.Draw(g);

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

            str = "Lives: ";
            size = g.MeasureString(str, SystemFonts.DefaultFont);
            g.DrawString(str, SystemFonts.DefaultFont, Brushes.Black, Width - 200, 50 + size.Height);
            g.DrawString(Lives.ToString(), SystemFonts.DefaultFont, Brushes.Black, Width - 200 + size.Width, 50 + size.Height);

        }
    }
}
