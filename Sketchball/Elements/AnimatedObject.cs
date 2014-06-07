using GlideTween;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchball.Elements
{
    [DataContract]
    public abstract class AnimatedObject : PinballElement, IAnimatedObject
    {
        [Browsable(false)]
        public double Rotation { get; set; }
        
        [Browsable(false)]
        public double AngularVelocityPerFrame { get; private set; }

        [Browsable(false)]
        public double AngularVelocity { get; private set; }

        [Browsable(false)]
        public bool Animating { get; private set; }

        [Browsable(false)]
        public Vector CurrentRotationCenter { get; private set; }

        protected Glide Tweener = new Glide();

        public AnimatedObject()
        {
        }

        public GlideTween.Glide Rotate(double rad, Vector center, float time)
        {
            return Rotate(rad, center, time, null);
        }

        public GlideTween.Glide Rotate(double rad, Vector center, float time, Action endRotation)
        {
            double degAbs = rad + this.Rotation;
            this.CurrentRotationCenter = center;
            this.AngularVelocity = rad / time;
            
            Tweener.Cancel();

            return Tweener.Tween(this, new { Rotation = degAbs }, time).OnComplete(endRotation + resetVelocity);
        }

        private void resetVelocity()
        {
            this.AngularVelocity = 0;
        }

        protected override void OnDraw(System.Windows.Media.DrawingContext g)
        {
            if (Rotation != 0)
            {
                g.PushTransform(new System.Windows.Media.RotateTransform(-(Rotation / (Math.PI) * 180f), CurrentRotationCenter.X, CurrentRotationCenter.Y));
            }
        }

        protected override void OnDrawn(System.Windows.Media.DrawingContext g)
        {
            if (Rotation != 0)
            {
                g.Pop();
            }
        }

        public override void Update(double delta)
        {
            if (delta != 0)
            {
                this.AngularVelocityPerFrame = this.AngularVelocity * delta;
            }
            else
            {
                this.AngularVelocityPerFrame = 0;
            }
            base.Update(delta);
            Tweener.Update((float)delta);
           
            this.BoundingContainer.Rotate(-this.Rotation, this.CurrentRotationCenter);
        
        }

    }
}
