using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.Controls
{
    class PinballGameControl : PinballControl
    {
        public const int TOTAL_LIVES = 3;


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
            : base((PinballMachine)machine.Clone())
        {
            OriginalMachine = machine;

            // Optimize control for performance
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        
        /// <summary>
        /// Starts the game.
        /// </summary>
        public void Start()
        {
            
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
        /// Resets the game.
        /// TODO: Definition of "reset"
        /// </summary>
        public void Reset()
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
            base.Draw(g);
        }
    }
}
