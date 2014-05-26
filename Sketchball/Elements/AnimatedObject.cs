using GlideTween;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    [DataContract]
    public abstract class AnimatedObject : PinballElement, IAnimatedObject
    {
        [Browsable(false)]
        public float Rotation { get; set; }
        [Browsable(false)]
        public float angualrVelocityPerFrame { get; private set; }
        [Browsable(false)]
        public float angularVelocity { get; private set; }

        [Browsable(false)]
        public Vector2 currentRotationCenter { get; private set; }
        protected Glide Tweener = new Glide();

        public AnimatedObject()
        {
        }

        public GlideTween.Glide rotate(float rad, Vector2 center, float time)
        {
            return rotate(rad, center, time, null);
        }

        public GlideTween.Glide rotate(float rad, Vector2 center, float time, Action endRotation)
        {
            float degAbs = rad + this.Rotation;
            this.currentRotationCenter = center;
            this.angularVelocity = rad / time;
            
            Tweener.Cancel();
            return Tweener.Tween(this, new { Rotation = degAbs }, time).OnComplete(endRotation);
        }

        protected override void OnDraw(System.Windows.Media.DrawingContext g)
        {
            if (Rotation != 0)
            {
                g.PushTransform(new System.Windows.Media.RotateTransform(-(Rotation / (Math.PI) * 180f), currentRotationCenter.X, currentRotationCenter.Y));
            }
        }

        public override void Draw(System.Windows.Media.DrawingContext g)
        {
            base.Draw(g);

            if (Rotation != 0)
            {
                g.Pop();
            }
        }

        public override void Update(long delta)
        {
            if (delta != 0)
            {
                this.angualrVelocityPerFrame = this.angularVelocity * (delta / 1000f);
            }
            else
            {
                this.angualrVelocityPerFrame = 0;
            }
            base.Update(delta);
            Tweener.Update(delta / 1000f);
           
            this.boundingContainer.Rotate(-this.Rotation, this.currentRotationCenter);
        }


    }
}
