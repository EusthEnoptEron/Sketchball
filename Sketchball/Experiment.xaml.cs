using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Sketchball
{
    /// <summary>
    /// Interaction logic for Experiment.xaml
    /// </summary>
    public partial class Experiment : UserControl
    {
        private System.Drawing.Bitmap Ball = Properties.Resources.BallWithAlpha;
        Image img;
        public Thread thread;
        private CancellationTokenSource running;

        public Experiment()
        {
            InitializeComponent();
            img = new Image();
            img.Source = new BitmapImage(new Uri(@"Resources/SlingshotRight.png", UriKind.Relative));

            thread = new Thread(new ThreadStart(() =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                running = 
                    new CancellationTokenSource();

                
                    while (!running.IsCancellationRequested)
                    {
                        time = watch.Elapsed.TotalSeconds;
                        try
                        {
                            Dispatcher.Invoke(() =>
                            {
                                InvalidateVisual();
                            }, DispatcherPriority.Render, running.Token);
                        }
                        catch (TaskCanceledException) { }

                        Thread.Sleep((int)(1000 / 30f));
                    }
              
            }));
            thread.Start();
                
        }

        
        public void Exit()
        {
            running.Cancel();
        }
        int i = 0;
        double time = 0;
        protected override void OnRender(DrawingContext g)
        {
            //System.Windows.Media.Effects.
            g.DrawRectangle(Brushes.Red, null, new Rect(0, 0, Width, Height));
            g.PushTransform(new ScaleTransform((Math.Sin(time)/2)+1.5, (Math.Sin(time)/2)+1.5));
            //g.PushTransform(new RotateTransform(time*5));
            Pen pen = new Pen(Brushes.Green, 2);
            for (int x = 0; x < Width; x+=20)
            {
                g.DrawLine(pen, new Point(x, 0), new Point(x, Height));   
            }
            for (int y = 0; y < Height; y+=20)
            {
                g.DrawLine(pen, new Point(0, y), new Point(Width, y));
            }

            for (int x = 0; x < Width; x += 50)
            {
                for (int y = 0; y < Height; y += 50)
                {
                    g.DrawImage(img.Source, new Rect(x + Math.Sin(time * 2 + x) * 50, y + Math.Cos(time * 2 + y) * 50, 100, 100));
                }
            }
            i++;   
        }
    }
}
