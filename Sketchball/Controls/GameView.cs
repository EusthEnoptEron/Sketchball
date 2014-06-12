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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace Sketchball.Controls
{
    /// <summary>
    /// Control that houses a game of pinball and provides a view on it.
    /// </summary>
    class GameView : PinballControl
    {

        /// <summary>
        /// Arbitrarily chosen FPS number that controls the update interval. We only have lose control over the visual update process
        /// i.e. we don't know when the update is done. If we use an update worker, we may be too fast and thus block user input. Furthermore,
        /// we would have to use a Dispatcher, which would make the application more prone to errors.
        /// By using a common timer with a certain tick rate, we may lose some accuracy, but in exchange Windows can take care of it all.
        /// </summary>
        private const int MAX_FPS = 40;

        /// <summary>
        /// The camera being used to look at the scene.
        /// </summary>
        public Camera Camera { get; private set; }

        // The HUD
        private GameHUD HUD;

        // The game scene.
        private GameWorld gameWorld;

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
            gameWorld = new GameWorld(Game);
            HUD = new GameHUD(Game);

            Camera = new GameFieldCamera(gameWorld, HUD);

            // Little hack needed so that we get all input events.
            Focusable = true;
            Loaded += (s, e) => { Focus(); };

            // Init camera
            Camera.Size = new Size(Width, Height);

            // Set up the update timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000 / MAX_FPS;
            timer.Tick += OnTick;
            timer.Start();

            // Wire up a few event listeners
            PreviewKeyDown += HandleKeyDown;
            SizeChanged += ResizeCamera;
            Game.StatusChanged += delegate { Dispatcher.Invoke(delegate { InvalidateVisual(); }, System.Windows.Threading.DispatcherPriority.Render); };

            // Let's draw with high quality
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
                    else if (Game.Status == GameStatus.Paused)
                    {
                        Game.Resume();
                    }
                    break;
            }

        }

        // Draw loop
        private void OnTick(object sender, EventArgs e)
        {
            if (isCancelled)
                timer.Dispose();
            else if(Game.Status == GameStatus.Playing) // Only draw if it's needed
                InvalidateVisual();
        }

        protected override void OnDispose()
        {
            // ElementHost sometimes fails to properly remove all references to a WPF control,
            // which is why we set all our own references to NULL so that at least those aren't kept alive forever.
            timer.Dispose();
            Game = null;
            gameWorld = null;
            HUD = null;
            Camera = null;
        }

        // Draw scene
        protected override void Draw(DrawingContext g)
        {

            if (Game.Status == GameStatus.GameOver || Game.Status == GameStatus.Paused)
            {
                // We'll make a first render pass with a blur shader, and after that we'll draw an overlay on it
                var firstPass = new DrawingVisual();
                firstPass.Effect = new BlurEffect();

                using (var g2 = firstPass.RenderOpen())
                {
                    Camera.Draw(g2);
                }

                g.DrawImage(GetImage(firstPass, (int)Width, (int)Height), new Rect(0, 0, Width, Height));


                if (Game.Status == GameStatus.GameOver)
                {
                    DrawOverlay(g, Colors.DarkRed, "GAME OVER", "Press [SPACE] to try again.");
                }
                else if (Game.Status == GameStatus.Paused)
                {
                    DrawOverlay(g, Colors.DarkBlue, "PAUSED", "Press [ENTER] to resume.");
                }

            }
            else
            {
                // We're playing, so just let the camera draw itself
                Camera.Draw(g);
            }
       }

        
        private void DrawOverlay(DrawingContext g, Color color, string title, string msg)
        {
            var col = Color.FromArgb(40, color.R, color.G, color.B);

            Brush brush = new SolidColorBrush(col);
            Brush solidBrush = new SolidColorBrush(color);

            var caption = Booster.GetText(title, new FontFamily("Impact"), 40, solidBrush);
            var text = Booster.GetText(msg, new FontFamily("Arial"), 13, solidBrush);
            double x = (Width - caption.Width) / 2;


            g.DrawRectangle(brush, null, new Rect(0, 0, Width, Height));
            g.DrawText(caption, new Point(x, (Height - caption.Height) / 2));
            g.DrawText(text, new Point(x, (Height + caption.Height) / 2));
        }

        // Turns a DrawingVisual into an ImageSource. Somewhat bothersome procedure, hence outsourced into its own function.
        private ImageSource GetImage(DrawingVisual visual, int width, int height)
        {
            visual.Clip = new RectangleGeometry(new Rect(0, 0, width, height));
            RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(visual);
            return bmp;
        }
    }
}
