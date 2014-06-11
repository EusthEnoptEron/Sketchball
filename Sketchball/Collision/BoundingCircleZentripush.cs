using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchball.Collision
{
    public class BoundingCircleZentripush : BoundingCircle
    {
        private int p;
        private Vector vector;

        public BoundingCircleZentripush(int radius, Vector position):base(radius,position)
        {
            
        }



        public override Vector ReflectManipulation(Vector newDirection,Vector hitpoint, int energy = 0)
        {
            Vector a = VectorExtensions.AsNormalized(hitpoint - (this.BoundingContainer.ParentElement.Location+ this.Position + new Vector(this.radius, this.radius))) * 200;
            return newDirection + BounceFactor * BoundingContainer.ParentElement.BounceFactor * a;
        }
    }
}
