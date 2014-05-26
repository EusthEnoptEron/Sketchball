using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchball.Elements
{
    interface IAnimatedObject
    {
        GlideTween.Glide Rotate(float rad, Vector center, float time);
        GlideTween.Glide Rotate(float rad, Vector center, float time, Action endRotation);

        float Rotation { get; set; }
    }
}
