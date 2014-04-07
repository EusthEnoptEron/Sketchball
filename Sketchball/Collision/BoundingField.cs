﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Collision
{
    public class BoundingField
    {
        private HashSet<IBoundingBox> bBReferences;     //ref on BoundingBoxes of all Pinballelements Boundingboxes that intersect this raster

        public int x { get; private set; }      //idx in raster
        public int y { get; private set; }     //idx in raster

        public BoundingField(int x, int y)
        {
            this.bBReferences = new HashSet<IBoundingBox>();

            this.x = x;
            this.y = y;
        }

        public void addReference(IBoundingBox bB)
        {
            this.bBReferences.Add(bB);
        }

        public void removeReference(IBoundingBox bB)
        {
            this.bBReferences.Remove(bB);
        }

        internal IEnumerable<IBoundingBox> getReferences()
        {
            return this.bBReferences;
        }
    }
}