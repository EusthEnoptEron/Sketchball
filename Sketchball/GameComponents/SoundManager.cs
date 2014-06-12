using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.GameComponents
{
    /// <summary>
    /// Manager that helps to keep the sound replay sane.
    /// </summary>
    public class SoundManager
    {
        private SoundPlayer currentPlayer;
        private DateTime lastPlay = new DateTime();

        /// <summary>
        /// The minimum interval between to equivalent sounds.
        /// </summary>
        private const int MIN_INTERVAL = 200;

        public void Play(SoundPlayer player)
        {
            DateTime now = DateTime.Now;

            if (currentPlayer != player || (now - lastPlay).TotalMilliseconds > MIN_INTERVAL)
            {
                if (currentPlayer != null)
                    currentPlayer.Stop();
                currentPlayer = player;
                currentPlayer.Play();
                lastPlay = now;
            }

        }

    }
}
