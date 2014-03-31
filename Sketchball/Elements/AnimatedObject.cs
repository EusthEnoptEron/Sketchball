using GlideTween;
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
        private Vector2 currentDrawingCenter;
        private Glide Tweener;

        public AnimatedObject()
        {
            this.Tweener = new Glide();
        }

        public void rotate(float rad, Vector2 center, float time)
        {
            float degAbs = rad + this.Rotation;
            this.currentDrawingCenter = center;
            Tweener.Tween(this, new { Rotation=degAbs }, time).Ease(GlideTween.Ease.QuintInOut);
        }

        public void rotate(float rad, Vector2 center, float time, Action endRotation)
        {
            float degAbs = rad + this.Rotation;
            this.currentDrawingCenter = center;
            Tweener.Tween(this, new { Rotation = degAbs }, time).Ease(GlideTween.Ease.QuintInOut).OnComplete(endRotation);
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            if (Rotation != 0)
            {
                g.TranslateTransform(0 + (this.currentDrawingCenter.X ), this.currentDrawingCenter.Y );
                g.RotateTransform((float)(Rotation/(Math.PI)*180f));
                g.TranslateTransform(0 - (this.currentDrawingCenter.X ), -( this.currentDrawingCenter.Y ));
            }
        }

        public override void Update(long delta)
        {
            base.Update(delta);
            Tweener.Update(delta / 1000f);
            this.boundingContainer.rotate(this.Rotation, this.currentDrawingCenter);
        }
    }
}
