﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using Sketchball.Elements;

namespace Sketchball.Collision
{
    public class BoundingContainer
    {
        public float Rotation { get; set; }
        public List<IBoundingBox> boundingBoxes { get; private set; }
        public PinballElement parentElement { get; private set; }

        public BoundingContainer(PinballElement parent)
        {
            this.boundingBoxes = new List<IBoundingBox>();
            this.parentElement = parent;
            this.Rotation = 0;
        }

        public List<IBoundingBox> getBoundingBoxes()
        {
            return this.boundingBoxes;
        }

        //center must be object space orientated!
        public void rotate(float rad, Vector2 center)
        {
            foreach (IBoundingBox b in this.boundingBoxes)
            {
                b.rotate(rad - Rotation, center+this.parentElement.getLocation());
            }
            this.Rotation = rad;

        }

        public void move(Vector2 moveVec)
        {
            foreach (IBoundingBox b in this.boundingBoxes)
            {
                b.move(moveVec);
            }
        }

        public void addBoundingBox(IBoundingBox bL)
        {
            this.boundingBoxes.Add(bL);
            bL.assigneToContainer(this);
        }

        public void AddPolygon(params float[] coords)
        {
            Vector2 prev = new Vector2();

            for (int i = 0; i+1 < coords.Length; i += 2)
            {
                var x = coords[i];
                var y = coords[i + 1];

                var v = new Vector2(x, y);

                if (i > 0)
                {
                    addBoundingBox(new BoundingLine(
                        prev,
                        v
                    ));
                }

                prev = v;
            }
        }

    }
}
