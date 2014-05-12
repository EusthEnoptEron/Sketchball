using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Sketchball.Collision;
using System.Runtime.Serialization;

namespace Sketchball.Elements
{

    [DataContract]
    public abstract class Flipper : AnimatedObject
    {
        [DataMember]
        public Keys Trigger { get; set; }
        protected Keys DebugTrigger;

        public float RotationRange = (float)(Math.PI / 180 * 60);
        private bool Animating = false;

        public Flipper()  : base()
        {

            Width = 70;
            Height = 70;


  //          this.rotate((float)Math.PI*2, Origin, 0);

            // 0, Height / 10 * 9, Width , Height / 10 * 2 )

            this.setLocation(new Vector2(0, 0));
        }

 
        public override void Update(long delta)
        {
            base.Update(delta);
        }

        protected override void EnterMachine(PinballGameMachine machine)
        {
            machine.Input.KeyDown += OnKeyDown;
        }


        protected override void LeaveMachine(PinballGameMachine machine)
        {
            machine.Input.KeyDown -= OnKeyDown;
        }

        protected virtual Vector2 Origin
        {
            get
            {
                return new Vector2(0, this.Height);
            }
        }


        void OnKeyDown(object sender, KeyEventArgs e)
        {
            if ( (e.KeyCode == Trigger ||e.KeyCode == DebugTrigger) && !Animating)
            {

                var speed = e.KeyCode == Trigger ? 0.1f : 4f;

                Animating = true;

                Action endRot = () => {
                    this.rotate(-Rotation, Origin, 0.1f, () => { Animating = false; }); 
                };

                this.rotate(RotationRange, Origin, speed, endRot);
            }
        }

    }
   
    
}
