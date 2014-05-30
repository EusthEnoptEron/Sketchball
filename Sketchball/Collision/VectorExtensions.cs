using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchball.Collision
{
    public static class VectorExtensions
    {
        public static Vector Normal(this Vector v)
        {
            return new Vector(v.Y, -v.X);
        }

        public static Vector AsNormalized(this Vector v)
        {
            var vector = new Vector(v.X, v.Y);
            vector.Normalize();
            return vector;
        }
    }
}
