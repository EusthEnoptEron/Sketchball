using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.GameComponents
{
    /// <summary>
    /// Represents an entry in the highscore list
    /// </summary>
    [DataContract]
    public class HighscoreEntry : IComparable<HighscoreEntry>
    {
        /// <summary>
        /// Gets the player associated with this highscore entry.
        /// </summary>
        [DataMember]
        public string Player { get; private set; }

        /// <summary>
        /// Gets the score made by this player.
        /// </summary>
        [DataMember]
        public int Score { get; private set; }

        /// <summary>
        /// Gets the date when the score was achieved.
        /// </summary>
        [DataMember]
        public DateTime Date { get; private set; }

        public HighscoreEntry(string player, int score, DateTime time)
        {
            Player = player;
            Score = score;
            Date = time;
        }

        public int CompareTo(HighscoreEntry other)
        {
            return this.Score - other.Score;
        }
    }
}
