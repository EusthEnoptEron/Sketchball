﻿using GlideTween;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public abstract class AnimatedObject : PinballElement, IAnimatedObject
    {
        public float Rotation { get; set; }
        public float angualrVelocityPerFrame { get; private set; }
        public float angularVelocity { get; private set; }

        public Vector2 currentRotationCenter { get; private set; }
        private Glide Tweener;

        public AnimatedObject()
        {
            this.Tweener = new Glide();
        }

        public void rotate(float rad, Vector2 center, float time)
        {
            float degAbs = rad + this.Rotation;
            this.currentRotationCenter = center;
            Tweener.Tween(this, new { Rotation=degAbs }, time).Ease(GlideTween.Ease.QuintInOut);
        }

        public void rotate(float rad, Vector2 center, float time, Action endRotation)
        {
            float degAbs = rad + this.Rotation;
            this.currentRotationCenter = center;
            this.angularVelocity = rad / time;
            Tweener.Tween(this, new { Rotation = degAbs }, time).OnComplete(endRotation);
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            if (Rotation != 0)
            {
                g.TranslateTransform(0 + (this.currentRotationCenter.X ), this.currentRotationCenter.Y );
                g.RotateTransform((float)-(Rotation/(Math.PI)*180f));
                g.TranslateTransform(0 - (this.currentRotationCenter.X ), -( this.currentRotationCenter.Y ));
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
           
            this.boundingContainer.rotate(-this.Rotation, this.currentRotationCenter);
        }
    }
}