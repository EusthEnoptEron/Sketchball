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

namespace Sketchball.Elements
{

    [DataContract]
    public abstract class Flipper : AnimatedObject
    {
        private static readonly Size size = new Size(70, 70);

        [DataMember]
        public Keys Trigger { get; set; }
        protected Keys DebugTrigger;

        public double RotationRange = (Math.PI / 180 * 60);
        private bool Animating = false;
        private static readonly SoundPlayer player = new SoundPlayer(Properties.Resources.SWormholeExit);


        public Flipper()  : base()
        {
        }

 
        public override void Update(long delta)
        {
            base.Update(delta);
        }

        protected override void EnterMachine(PinballGameMachine machine)
        {
            machine.Input.KeyDown += OnKeyDown;
            machine.Input.KeyUp += OnKeyUp;
        }


        protected override void LeaveMachine(PinballGameMachine machine)
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
            if ( (e.KeyCode == Trigger ||e.KeyCode == DebugTrigger) && !Animating)
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
        }

        void OnKeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Trigger || e.KeyCode == DebugTrigger) && Animating)
            {
                var speed = e.KeyCode == Trigger ? 0.1f : 4f;

                this.Rotate(-Rotation, Origin, 0.1f, () => { Animating = false; });
            }
        }

        public override void notifyIntersection(Ball b)
        {
            player.Play();
        }

    }
   
    
}
