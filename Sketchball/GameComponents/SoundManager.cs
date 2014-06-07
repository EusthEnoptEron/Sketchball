using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.GameComponents
{
    public class SoundManager
    {
        private SoundPlayer currentPlayer;
        public void Play(SoundPlayer player)
        {
            if(currentPlayer != null)
                currentPlayer.Stop();
            currentPlayer = player;
            currentPlayer.Play();
        }

    }
}
