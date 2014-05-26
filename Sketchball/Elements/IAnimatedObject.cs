using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    interface IAnimatedObject
    {
        GlideTween.Glide Rotate(float rad, Vector2 center, float time);
        GlideTween.Glide Rotate(float rad, Vector2 center, float time, Action endRotation);

        float Rotation { get; set; }
    }
}
