﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sketchball.Collision;
using System.Runtime.Serialization;
using System.Windows;
using System.Media;
using System.IO;

namespace Sketchball.Elements
{

    [DataContract]
    public abstract class Flipper : AnimatedObject
    {
        private static readonly Size size = new Size(70, 70);
        private static readonly SoundPlayer sound = new SoundPlayer(Properties.Resources.SWormholeExit);

        [DataMember]
        public Keys Trigger { get; set; }

        public double RotationRange = (Math.PI / 180 * 60);
        public bool Animating { get; private set; }


        public Flipper()  : base()
        {
            Animating = false;
        }


        protected override void EnterGame(PinballGameMachine machine)
        {
            machine.Input.KeyDown += OnKeyDown;
            machine.Input.KeyUp += OnKeyUp;
        }


        protected override void LeaveGame(PinballGameMachine machine)
        {
            machine.Input.KeyDown -= OnKeyDown;
            machine.Input.KeyUp -= OnKeyUp;
        }

        protected virtual Vector Origin
        {
            get
            {
                return new Vector(0, this.Height);
            }
        }


        void OnKeyDown(object sender, KeyEventArgs e)
        {
            if ( (e.KeyCode == Trigger) && !Animating)
            {

                var speed = e.KeyCode == Trigger ? 0.05f : 4f;

                Animating = true;

                Action endRot = () => {
                    this.Rotate(-Rotation, Origin, 0.05f, () => { Animating = false; }); 
                };

                this.Rotate(RotationRange, Origin, speed, null);
            }
        }


        protected override Size BaseSize
        {
            get { return size; }
        }

        protected override void Init()
        {
            this.Animating = false;
        }

        void OnKeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Trigger) && Animating)
            {
                var speed = e.KeyCode == Trigger ? 0.1f : 4f;

                this.Rotate(-Rotation, Origin, 0.1f, () => { Animating = false; });
            }
        }

        public override void notifyIntersection(Ball b)
        {
            GameWorld.Sfx.Play(sound);
        }

    }
   
    
}
