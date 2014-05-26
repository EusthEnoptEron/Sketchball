using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;
using System.Windows;
namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingRaster_LineTakeOverBoundingBoxes
    {
        [TestMethod]
        public void TakeOverBoundingContainerWithLineSmallHorizontal()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            Vector position1 = new Vector(0, 0);
            Vector target1 = new Vector(50,0);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Line parent1 = new Line();
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bL1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 5 && x >= 0 && y <= 0 && y >= 0)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bL1, b);
                            found = true;
                        }
                        if (!found)
                        {
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            if (bL1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithLineSmallHorizontalReverse()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            Vector position1 = new Vector(50, 0);
            Vector target1 =  new Vector(0, 0);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Line parent1 = new Line();
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bL1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 5 && x >= 0 && y <= 0 && y >= 0)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bL1, b);
                            found = true;
                        }
                        if (!found)
                        {
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            if (bL1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithLineSmallVertical()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            Vector position1 = new Vector(0, 0);
            Vector target1 = new Vector(0, 50);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Line parent1 = new Line();
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bL1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 0 && x >= 0 && y <= 5 && y >= 0)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bL1, b);
                            found = true;
                        }
                        if (!found)
                        {
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            if (bL1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithLineSmallVerticalReverse()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            Vector position1 = new Vector(0, 50);
            Vector target1 = new Vector(0, 0);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Line parent1 = new Line();
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bL1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 0 && x >= 0 && y <= 5 && y >= 0)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bL1, b);
                            found = true;
                        }
                        if (!found)
                        {
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            if (bL1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }


        [TestMethod]
        public void TakeOverBoundingContainerWithLineSmallDiagonal()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            Vector position1 = new Vector(0, 0);
            Vector target1 = new Vector(50, 50);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Line parent1 = new Line();
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bL1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if ((x == y) && x >= 0 && x <= 5)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bL1, b);
                            found = true;
                        }
                        if (!found)
                        {
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            if (bL1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithLineSmallDiagonalReverse()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            Vector position1 = new Vector(55, 55);
            Vector target1 = new Vector(0, 0);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Line parent1 = new Line();
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bL1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if ((x == y) && x >= 0 && x <= 5)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bL1, b);
                            found = true;
                        }
                        if (!found)
                        {
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            if (bL1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithLineTotalOutside()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            Vector position1 = new Vector(-20, -20);
            Vector target1 = new Vector(-50, -50);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Line parent1 = new Line();
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bL1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                   
                    foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                    {
                        if (bL1.Equals(b))
                        {
                            Assert.Fail();
                        }
                    }
                    
                }
            }
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithLineSmallDiagonalTopOut()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            Vector position1 = new Vector(0, 20);
            Vector target1 = new Vector(0, -50);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Line parent1 = new Line();
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bL1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 0 && x >= 0 && y <= 2 && y >= 0)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bL1, b);
                            found = true;
                        }
                        if (!found)
                        {
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            if (bL1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }
    }
}
