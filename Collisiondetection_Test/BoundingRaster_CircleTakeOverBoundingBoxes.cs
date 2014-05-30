using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;
using System.Windows;


namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingRaster_CircleTakeOverBoundingBoxes
    {
        [TestMethod]
        public void creation()
        {
            //preconfig
            int cols = 20;
            int rows = 20;
            int width = 400;
            int height = 600;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            //operation
            int realCols = br.Cols;
            int realRows = br.Rows;
            int realWidth = br.Width;
            int realHeight = br.height;

            double realFieldHeight = br.FieldHeight;
            double realFieldWidth = br.FieldWidth;

            //assertion
            Assert.IsNotNull(br);
            Assert.AreEqual(cols, realCols);
            Assert.AreEqual(rows, realRows);
            Assert.AreEqual(width, realWidth);
            Assert.AreEqual(height, realHeight);
            Assert.AreEqual(expectedFieldHeight, realFieldHeight, 0.1);
            Assert.AreEqual(expectedFieldWidth, realFieldWidth, 0.1);
           
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithCircleSmallNormal1()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            int radius1 = 4;
            Vector position1 = new Vector(0, 0);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Bumper parent1 = new Bumper();
            BoundingCircle bC1 = new BoundingCircle(radius1, position1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bC1);
            parent1.Location = (new Vector(0,0));
           
            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 1 && y <= 1)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bC1, b);
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
                            if (bC1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
            
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithCircleSmallNormal2()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            int radius1 = 5;
            Vector position1 = new Vector(50, 50);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Bumper parent1 = new Bumper();
            BoundingCircle bC1 = new BoundingCircle(radius1, position1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bC1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 6 && x >= 4 && y <= 6 && y >= 4)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bC1, b);
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
                            if (bC1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
           
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithCircleSmallTopLeft()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            int radius1 = 5;
            Vector position1 = new Vector(0, 0);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Bumper parent1 = new Bumper();
            BoundingCircle bC1 = new BoundingCircle(radius1, position1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bC1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 1 && x >= 0 && y <= 1 && y >= 0)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bC1, b);
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
                            if (bC1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
           
        }


        [TestMethod]
        public void TakeOverBoundingContainerWithCircleSmallBotRight()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            int radius1 = 5;
            Vector position1 = new Vector(100, 100);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Bumper parent1 = new Bumper();
            BoundingCircle bC1 = new BoundingCircle(radius1, position1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bC1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 9 && x >= 9 && y <= 9 && y >= 9)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bC1, b);
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
                            if (bC1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
           
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithCircleBigCenter()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            int radius1 = 50;
            Vector position1 = new Vector(0, 0);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Bumper parent1 = new Bumper();
            BoundingCircle bC1 = new BoundingCircle(radius1, position1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bC1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 9 && x >= 0 && y <= 9 && y >= 0)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bC1, b);
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
                            if (bC1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithCircleBigRightBot()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            int radius1 = 50;
            Vector position1 = new Vector(50, 50);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Bumper parent1 = new Bumper();
            BoundingCircle bC1 = new BoundingCircle(radius1, position1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bC1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 9 && x >= 5 && y <= 9 && y >= 5)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bC1, b);
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
                            if (bC1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithCircleBigLeftOutside()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            int radius1 = 50;
            Vector position1 = new Vector(-50, 0);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Bumper parent1 = new Bumper();
            BoundingCircle bC1 = new BoundingCircle(radius1, position1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bC1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 5 && x >= 0 && y <= 9 && y >= 0)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bC1, b);
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
                            if (bC1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithCircleBigFarLeftOutside()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            int radius1 = 50;
            Vector position1 = new Vector(-75, 0);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Bumper parent1 = new Bumper();
            BoundingCircle bC1 = new BoundingCircle(radius1, position1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bC1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 3 && x >= 0 && y <= 9 && y >= 0)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bC1, b);
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
                            if (bC1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithCircleBigFarTopOutside()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            int radius1 = 50;
            Vector position1 = new Vector(0, -75);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Bumper parent1 = new Bumper();
            BoundingCircle bC1 = new BoundingCircle(radius1, position1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bC1);
            parent1.Location = (new Vector(0, 0));

            //operation
            br.TakeOverBoundingContainer(bCont1);

            //assertion
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x <= 9 && x >= 0 && y <= 3 && y >= 0)
                    {
                        bool found = false;
                        foreach (IBoundingBox b in br.GetBoundingField(x, y).getReferences())
                        {
                            Assert.AreEqual(bC1, b);
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
                            if (bC1.Equals(b))
                            {
                                Assert.Fail();
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TakeOverBoundingContainerWithCircleBigTotalOutside()
        {
            //preconfig
            int cols = 10;
            int rows = 10;
            int width = 100;
            int height = 100;

            int expectedFieldHeight = height / rows;
            int expectedFieldWidth = width / cols;

            int radius1 = 5;
            Vector position1 = new Vector(-20, -20);

            //creation
            BoundingRaster br = new BoundingRaster(cols, rows, width, height);

            Bumper parent1 = new Bumper();
            BoundingCircle bC1 = new BoundingCircle(radius1, position1);
            BoundingContainer bCont1 = new BoundingContainer(parent1);
            bCont1.AddBoundingBox(bC1);
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
                        if (bC1.Equals(b))
                        {
                            Assert.Fail();
                        }
                    }
                    
                }
            }
        }
    }
}
