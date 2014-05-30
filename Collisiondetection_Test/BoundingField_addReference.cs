using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;
using System.Collections.Generic;
using System.Windows;

namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingField_addReference
    {
        [TestMethod]
        public void addReferenceTest()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);


            //Creation
            Bumper parent = new Bumper();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            bCont.AddBoundingBox(bC1);

            BoundingField bf = new BoundingField(0, 0);

            //Operation
            bf.addReference(bC1);


            int hit = 0;
            //Assertion
            foreach (IBoundingBox b in bf.getReferences())
            {
                if (b.Equals(bC1))
                {
                    hit++;
                }
            }

            if (hit == 1)
            {
                Assert.AreEqual(1, hit);
            }
        }

        [TestMethod]
        public void addReferenceTwiceTest()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);


            //Creation
            Bumper parent = new Bumper();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            bCont.AddBoundingBox(bC1);

            BoundingField bf = new BoundingField(0, 0);

            //Operation
            bf.addReference(bC1);
            bf.addReference(bC1);


            int hit = 0;
            //Assertion
            foreach (IBoundingBox b in bf.getReferences())
            {
                if (b.Equals(bC1))
                {
                    hit++;
                }
            }

            if (hit == 1)
            {
                Assert.AreEqual(1, hit);
            }
        }

        [TestMethod]
        public void addReferenceTwoBBTest()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);


            //Creation
            Bumper parent = new Bumper();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius1, center1);
            bCont.AddBoundingBox(bC1);
            bCont.AddBoundingBox(bC2);

            BoundingField bf = new BoundingField(0, 0);

            //Operation
            bf.addReference(bC1);
            bf.addReference(bC2);


            int hit1 = 0;
            int hit2 = 0;
            //Assertion
            foreach (IBoundingBox b in bf.getReferences())
            {
                if (b.Equals(bC1))
                {
                    hit1++;
                }
                if (b.Equals(bC2))
                {
                    hit2++;
                }
            }

            if (hit1 != 1)
            {
                Assert.Fail();
            }

            if (hit2 != 1)
            {
                Assert.Fail();
            }
        }
    }
}
