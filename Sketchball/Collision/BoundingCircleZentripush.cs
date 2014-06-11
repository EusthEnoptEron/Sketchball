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
  

        public BoundingCircleZentripush(int radius, Vector position):base(radius,position)
        {
            
        }

        public override IBoundingBox Clone()
        {
            //do not forget to assinge BoundingContainer after clone
            BoundingCircleZentripush bL = new BoundingCircleZentripush(this.radius, new Vector(this.Position.X - this.radius, this.Position.Y - this.radius));
            return bL;
        }

        public override Vector ReflectManipulation(Vector newDirection,Vector hitpoint, int energy = 0)
        {
            Vector a = VectorExtensions.AsNormalized(hitpoint - (this.BoundingContainer.ParentElement.Location+ this.Position + new Vector(this.radius, this.radius))) * 200;
            return newDirection + BounceFactor * BoundingContainer.ParentElement.BounceFactor * a;
        }
    }
}
