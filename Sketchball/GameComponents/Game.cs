﻿using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.GameComponents
{
    /// <summary>
    /// Phases the control can assume
    /// </summary>
    public enum GameStatus
    {
        Setup,
        Playing,
        GameOver,
        Pause
    }

    /// <summary>
    /// Control that houses a game of pinball.
    /// </summary>
    public class Game : IDisposable
    {
        public delegate void ScoreChangedHandler(Game sender, int score);
        public delegate void LivesChangedHandler(Game sender, int lives);

        /// <summary>
        /// Occurs when the score has changed.
        /// </summary>
        public event ScoreChangedHandler ScoreChanged;

        /// <summary>
        /// Occurs when the number of lives has changed.
        /// </summary>
        public event LivesChangedHandler LivesChanged;

        /// <summary>
        /// Occurs when the current game is over.
        /// </summary>
        public event EventHandler<int> GameOver;

        /// <summary>
        /// Total number of lives (<=> balls)
        /// </summary>
        public const int TOTAL_LIVES = 3;


        private GameStatus _status = GameStatus.Setup;

        /// <summary>
        /// Gets the current game status.
        /// </summary>
        public GameStatus Status
        {
            get
            {
                return _status;
            }
            private set
            {
                _status = value;
                lock (this)
                {
                    Monitor.PulseAll(this);
                }
            }
        }


        /// <summary>
        /// Gets or sets the machine currently displayed.
        /// </summary>
        public PinballGameMachine Machine { get; private set;}


        private int _score = 0;
        /// <summary>
        /// Gets the score of the current game.
        /// </summary>
        public int Score { 
            get {
                return _score;
            }
            private set
            {
                _score = value;
                RaiseScoreChanged();
            }
        }


        private int _lives = 0;
        /// <summary>
        /// Gets the number of remaining lives of the current game.
        /// </summary>
        public int Lives {
            get
            {
                return _lives;
            }
            private set
            {
                _lives = value;
                RaiseLivesChanged();
            }
        }

        
        /// <summary>
        /// Original machine from which the game machines are made.
        /// </summary>
        private PinballMachine OriginalMachine;



        private Thread UpdateLoop = null;
        private volatile bool Disposed;
        private const int FPS = 120;
        private string userName;

        public Game(PinballMachine machine, string userName) {
            Status = GameStatus.Setup;
           
            OriginalMachine = machine;
            this.userName = userName;

            UpdateLoop = new Thread(new ThreadStart(BeginUpdate));
            UpdateLoop.Name = "Updater";
            UpdateLoop.Start();
                       
            Start();
        }

        public HighscoreList Highscores
        {
            get
            {
                return OriginalMachine.Highscores;
            }
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void Start()
        {
            lock (this)
            {
                if (Machine != null)
                {
                    Machine.Dispose();
                }

                Machine = new PinballGameMachine(OriginalMachine);
                Machine.prepareForLaunch();


                Status = GameStatus.Playing;
                Machine.Input.Enabled = true;

                Score = 0;
                Lives = TOTAL_LIVES;

                // Wire up event handlers
                Machine.Collision += OnScore;
                Machine.GameOver += OnGameOver;

                Machine.IntroduceBall();
                Lives--;
            }
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
                OriginalMachine.Highscores.Add(new HighscoreEntry(
                    userName, Score, DateTime.UtcNow    
                ));


                var handlers = GameOver;
                if (handlers != null)
                {
                    handlers(this, Score);
                }
            }
            else
            {
                Lives--;
                Machine.IntroduceBall();
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
            if (Status == GameStatus.Playing)
            {
                Machine.Input.Enabled = false;
                Status = GameStatus.Pause;
            }
        }


        
        /// <summary>
        /// Resumes the game if paused.
        /// </summary>
        public void Resume()
        {
            if (Status == GameStatus.Pause)
            {
                Machine.Input.Enabled = true;
                Status = GameStatus.Playing;
            }
        }

        
        /// <summary>
        /// Updates positions and checks for collisions, etc.
        /// </summary>
        private void Update(long elapsed)
        {            
            // Update elements
            Machine.Update(elapsed);
        }


        /// <summary>
        /// Gets if the game is currently running.
        /// </summary>
        public bool IsRunning {
            get {
                return Machine.HasBall();
            }
        }


        private void RaiseScoreChanged()
        {
            var handlers = ScoreChanged;
            if (handlers != null)
                handlers(this, Score);
        }

        private void RaiseLivesChanged()
        {
            var handlers = LivesChanged;
            if (handlers != null)
                handlers(this, Lives);
        }

        /// <summary>
        /// Update-Loop (has its own thread)
        /// </summary>
        private void BeginUpdate()
        {
           
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();

            int timePerPass = 1000 / FPS;
            
            long i = 0;
            long sleepTimes = 0;
            double averageSleepTime = 0;

            while (!Disposed)
            {

                stopWatch.Restart();

                lock(this) {
                    while (Status != GameStatus.Playing)
                    {
                        Monitor.Wait(this);
                        if (Disposed) return;
                    }

                    this.Update((int)(timePerPass * 1));
                }
                stopWatch.Stop();
                int sleepTime = Math.Max(0, timePerPass - (int)stopWatch.ElapsedMilliseconds);
                Thread.Sleep(sleepTime);
            }
        }

        public void Dispose()
        {
           // MessageBox.Show(MaxTime.ToString()+" "+sleepTime*1f/this.tims);
            Disposed = true;

            lock (this)
            {
                Monitor.PulseAll(this);
            }
            Machine.Dispose();
        }
    }
}
