﻿using Sketchball.Elements;
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
        private const int MIN_FPS = 10;

        private GameStatus Status = GameStatus.Setup;
        private PinballGameMachine Game;


        /// <summary>
        /// Gets the score of the current game.
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// Gets the number of remaining lives of the current game.
        /// </summary>
        public int Lives { get; private set; }

        private PinballMachine OriginalMachine;

        protected override void ConfigureGDI(Graphics g)
        {
            base.ConfigureGDI(g);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }

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

        void HandleKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (!Game.HasBall())
                {
                    Start();
                }
            }
        }

        void PinballGameControl_HandleCreated(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            
            worker.DoWork += DrawCycle;
            worker.RunWorkerAsync();
        }

        void DrawCycle(object sender, DoWorkEventArgs e)
        {
            DateTime prev = DateTime.Now;
            DateTime now;

            while (true)
            {
                now = DateTime.Now;
                long delta = Math.Min(1000 / MIN_FPS, (long)(now - prev).TotalMilliseconds);

                Update(delta);
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

            Game = new PinballGameMachine(OriginalMachine);
            Game.prepareForLaunch();

            Score = 0;
            Lives = TOTAL_LIVES;

            // Wire up event handlers
            Game.Collision += OnScore;
            Game.GameOver += OnGameOver;

            Game.IntroduceBall();
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
                Game.IntroduceBall();
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
            Game.Update(elapsed);
        }


        protected override void Draw(Graphics g)
        {
            // Draw pinball machine
            Game.Draw(g);

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
