﻿using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sketchball.Elements
{
    public class Ball : PinballElement
    {
        public static readonly Size Size = new Size(20, 20);
        private readonly float friction = 0.9999f;

        long timeElapsed = 0;
        double distanceTraveled = 0;
        private const int SAMPLING_TIME = 2000;
        private const int SAMPLING_THRESHOLD = 150;

        protected override Size BaseSize
        {
            get { return Size; }
        }
       

        public Vector Velocity
        {
            get;
            set;
        }

        public Ball() : base()
        {
            Velocity = new Vector(0,0);
        }

        protected override void Init()
        {
            BoundingCircle bC = new BoundingCircle(10, new Vector(0, 0));
            this.BoundingContainer.AddBoundingBox(bC);
        }

        protected override void InitResources()
        {
            Image = Booster.OptimizeWpfImage("BallWithAlpha.png");
        }

        public override void Update(long delta)
        {
            base.Update(delta);
            Velocity += World.Acceleration * (delta / 1000f);
            Velocity = new Vector(Velocity.X * (this.friction - 0.00000001f * Velocity.X * Velocity.X), Velocity.Y * (this.friction - 0.00000001f * Velocity.Y * Velocity.Y));

            Location += Velocity * (delta / 1000f);

            preventDrain(delta);
        }

        private void preventDrain(long delta)
        {
            // Update metrics
            timeElapsed += delta;
            distanceTraveled += (Velocity * (delta / 1000f)).Length;

            if (timeElapsed > SAMPLING_TIME)
            {
                // -> Let's evaluate the results
                // Just make sure that distanceTraveled is calculated down to the sampling time (e.g. in case there was a freeze)
                distanceTraveled = SAMPLING_TIME / (double)timeElapsed * distanceTraveled;

                if (distanceTraveled < SAMPLING_THRESHOLD && !World.Layout.Ramp.Contains(this))
                {
                    World.Layout.Ramp.IntroduceBall(this);
                }


                distanceTraveled = 0;
                timeElapsed = 0;
            }

        }

        public float Mass
        {
            get;
            set;
        }

        //not sure if I can do this?
        public void setParent(PinballMachine pM)
        {
            this.World = pM;
        }



    }
}
