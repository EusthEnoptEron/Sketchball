using GlideTween;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sketchball.Elements
{

    /// <summary>
    /// Represents an object that can be animated.
    /// </summary>
    [DataContract]
    public abstract class AnimatedObject : PinballElement, IAnimatedObject
    {
        /// <summary>
        /// Gets the (additional) rotation currently applied to the element.
        /// </summary>
        [Browsable(false)]
        public double Rotation { get; set; }
        
        /// <summary>
        /// Gets the angular velocity per frame.
        /// </summary>
        [Browsable(false)]
        public double AngularVelocityPerFrame { get; private set; }

        /// <summary>
        /// Gets the current angular velocity.
        /// </summary>
        [Browsable(false)]
        public double AngularVelocity { get; private set; }


        /// <summary>
        /// Gets a bool whether or not this element is currently animating.
        /// </summary>
        [Browsable(false)]
        public bool Animating { get; protected set; }


        /// <summary>
        /// Gets the center for the animated rotation.
        /// </summary>
        [Browsable(false)]
        public Vector CurrentRotationCenter { get; private set; }

        protected Glide Tweener;

        public AnimatedObject()
        {
        }

        protected override void Init()
        {
            Tweener = new Glide();
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

            if (Rotation > 0 || Rotation < 0)
            {
                var center = Transform.Transform(CurrentRotationCenter);
                var m = Matrix.Identity * Transform;
                m.RotateAtPrepend(-(Rotation / (Math.PI) * 180f), CurrentRotationCenter.X, CurrentRotationCenter.Y);

                foreach (var box in BoundingContainer.BoundingBoxes) box.Sync(m);
            }
        }

    }
}
