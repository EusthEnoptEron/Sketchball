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
        GlideTween.Glide Rotate(double rad, Vector center, float time);
        GlideTween.Glide Rotate(double rad, Vector center, float time, Action endRotation);

        double Rotation { get; set; }
    }
}
