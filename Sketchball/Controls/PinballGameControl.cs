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

        private DateTime prev = DateTime.MinValue;

        public PinballGameControl()
            : base()
        {
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
        /// Updates positions and checks for collisions, etc.
        /// </summary>
        private void Update()
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

    }
}
