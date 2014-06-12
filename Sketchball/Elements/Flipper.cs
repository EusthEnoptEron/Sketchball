using System;
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
    /// <summary>
    /// Abstract flipper class that handles the rough setup code for flippers.
    /// </summary>
    [DataContract]
    public abstract class Flipper : AnimatedObject
    {
        private static readonly Size size = new Size(70, 70);
        private static readonly SoundPlayer sound = new SoundPlayer(Properties.Resources.SWormholeExit);

        [DataMember]
        public Keys Trigger { get; set; }

        public double RotationRange;

        public Flipper()  : base()
        {
        }

        protected override void Init()
        {
            base.Init();
            this.Animating = false;
            RotationRange = (Math.PI / 180 * 60);
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

        protected virtual Vector origin
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
                Animating = true;

                Action endRot = () => {
                    this.Rotate(-Rotation, origin, 0.05f, () => { Animating = false; }); 
                };

                this.Rotate(RotationRange, origin, 0.05f, null);
            }
        }


        protected override Size BaseSize
        {
            get { return size; }
        }



        void OnKeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Trigger) && Animating)
            {
                var speed = e.KeyCode == Trigger ? 0.1f : 4f;

                this.Rotate(-Rotation, origin, 0.1f, () => { Animating = false; });
            }
        }

        public override void OnIntersection(Ball b)
        {
            GameWorld.Sfx.Play(sound);
        }

    }
   
    
}
