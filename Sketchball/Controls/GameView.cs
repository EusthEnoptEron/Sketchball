using Sketchball.Elements;
using Sketchball.GameComponents;
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
        private const int MAX_FPS = 80;

        public Camera Camera{get; private set;}
        private GameHUD HUD;

        public Game Game;
        private System.Windows.Forms.Timer timer;

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

            Focusable = true;
            Loaded += (s, e) => { Focus(); };

            // Init camera
            Camera.Size = new Size(Width, Height);

            // Optimize control for performance
           // this.Effect = new System.Windows.Media.Effects.BlurEffect();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000 / MAX_FPS;
            timer.Tick += OnDraw;
            timer.Start();

            PreviewKeyDown += HandleKeyDown;
            
            SizeChanged += ResizeCamera;

            SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.HighQuality);
        }

        private void ResizeCamera(object sender, System.Windows.SizeChangedEventArgs e)
        {
            Camera.Size = new Size((int)Width, (int)Height);
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

        private void OnDraw(object sender, EventArgs e)
        {
            if (isCancelled) 
                timer.Dispose();
            else
                InvalidateVisual();
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
                DrawOverlay(g, Colors.DarkRed, "GAME OVER", "Press [SPACE] to try again.");
            }
            else if (Game.Status == GameStatus.Pause)
            {
                DrawOverlay(g, Colors.DarkBlue, "PAUSED", "Press [ENTER] to resume.");
            }
            
        }

        private void DrawOverlay(DrawingContext g, Color color, string title, string msg)
        {
            var col = Color.FromArgb(150, color.R, color.G, color.B);

            Brush brush = new SolidColorBrush(col);
            Brush solidBrush = new SolidColorBrush(color);

            var caption = Booster.GetText(title, new FontFamily("Impact"), 40, solidBrush);
            var text = Booster.GetText(msg, new FontFamily("Arial"), 13, solidBrush);
            double x = (Width - caption.Width) / 2;


            g.DrawRectangle(brush, null, new Rect(0, 0, Width, Height));
            g.DrawText(caption, new Point( x, (Height - caption.Height) / 2 ));
            g.DrawText(text, new Point(x, (Height + caption.Height) / 2));
        }


    }
}
