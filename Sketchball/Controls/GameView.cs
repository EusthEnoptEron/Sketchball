using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace Sketchball.Controls
{
    /// <summary>
    /// Control that houses a game of pinball.
    /// </summary>
    class GameView : PinballControl
    {

        /// <summary>
        /// The absolute maximum of FPS at any point in time.
        /// </summary>
        private const int MAX_FPS = 40;

        public Camera Camera{get; private set;}
        private GameHUD HUD;

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

            HUD = new GameHUD(Game);

            // Init camera
            Camera.Size = new System.Drawing.Size((int)Width, (int)Height);

            // Optimize control for performance
           // this.Effect = new System.Windows.Media.Effects.BlurEffect();

            PinballGameControl_HandleCreated(this, null);
            KeyDown += HandleKeyDown;
            SizeChanged += ResizeCamera;

            SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.HighQuality);
        }

        private void ResizeCamera(object sender, System.Windows.SizeChangedEventArgs e)
        {
            Camera.Size = new System.Drawing.Size((int)Width, (int)Height);
        }


        /// <summary>
        /// Handles key presses (used to initiate a new game)
        /// </summary>
        private void HandleKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    if ((!Game.IsRunning) || Game.Status == GameStatus.GameOver)
                    {
                        Game.Start();
                    }
                    break;
                
                case Key.Enter:
                    if (Game.Status == GameStatus.Playing)
                    {
                        Game.Pause();
                    }
                    else if (Game.Status == GameStatus.Pause)
                    {
                        Game.Resume();
                    }
                    break;

                case Key.Add:
                   // this.Camera.zoom(1+this.zoomfactor);
                    break;

                case Key.OemMinus:
                case Key.Subtract:
                   // this.Camera.zoom(1-this.zoomfactor);
                    break;

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
            int msPerFrame = 1000 / MAX_FPS;
            Stopwatch watch = new Stopwatch();
 
            try
            {
                while (!CancelToken.IsCancellationRequested)
                {
                    watch.Restart();
                   
                    Dispatcher.Invoke(() =>
                    {
                        InvalidateVisual();
                    }, System.Windows.Threading.DispatcherPriority.Render);

                    Thread.Sleep(Math.Max(10, msPerFrame - (int)watch.ElapsedMilliseconds));
                }
            }
            catch (TaskCanceledException) { }
        
        }

        protected override void Draw(DrawingContext g)
        {
            // Draw pinball machine
            Camera.Draw(g);

            g.PushTransform(new TranslateTransform(Width - HUD.Width, 0));
            HUD.Draw(g);
            g.Pop();

            if (Game.Status == GameStatus.GameOver)
            {
                DrawOverlay(g, Colors.DarkRed, "YOU LOSE", "Press [SPACE] to try again.");
            }
            else if (Game.Status == GameStatus.Pause)
            {
                DrawOverlay(g, Colors.DarkBlue, "PAUSED", "Press [ENTER] to resume.");
            }
            
        }

        private void DrawOverlay(DrawingContext g, Color color, string title, string msg)
        {
            var col = Color.FromArgb(color.R, color.G, color.B, 150);

            Brush brush = new SolidColorBrush(col);
            Brush solidBrush = new SolidColorBrush(color);

            g.DrawRectangle(brush, null, new Rect(0, 0, Width, Height));
/*
            SizeF size = g.MeasureString(title, new Font("Impact", 40, System.Drawing.FontStyle.Regular));

            g.DrawString(title, new Font("Impact", 40, System.Drawing.FontStyle.Regular), solidBrush, new PointF((int)Width / 2 - size.Width / 2, (int)Height / 2 - size.Height / 2));
            g.DrawString(msg, new Font("Arial", 13, System.Drawing.FontStyle.Regular), solidBrush, new PointF((int)Width / 2 - size.Width / 2, (int)Height / 2 + size.Height / 2));
*/             
        }


    }
}
