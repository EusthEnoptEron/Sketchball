using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Collision
{
    public class BoundingRaster
    {
        /// <summary>
        /// Defines amount of rows (must be in sync with height and fieldHeight)
        /// </summary>
        public int rows { get; private set; }

        /// <summary>
        /// Defines amount of cols (must be in sync with width and fieldWidth)
        /// </summary>
        public int cols { get; private set; }

        /// <summary>
        /// Defines width of form (must be in sync with cols and fieldWidth)
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// Defines height of form (must be in sync with rows and fieldHeight)
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// List of all animated objects
        /// </summary>
        private LinkedList<IBoundingBox> animatedObjects;

        /// <summary>
        /// Fields on the raster that hold reference to bounding boxes
        /// </summary>
        private BoundingField[,] fields;

        /// <summary>
        /// Width of one field
        /// </summary>
        private int fieldWidth;

        /// <summary>
        /// Height of one field
        /// </summary>
        private int fieldHeight;

        public Vector2 hitPointDebug = new Vector2(0, 0);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cols">Amount of columns</param>
        /// <param name="rows">Amount of rows</param>
        /// <param name="width">Width of the form</param>
        /// <param name="height">Height of the form</param>
        public BoundingRaster(int cols, int rows, int width, int height)
        {
            if ((width / cols) % 1 != 0)
            {
                //not whole number
                throw new ArgumentException("width / cols must be whole number");
            }

            if ((height / rows) % 1 != 0)
            {
                //not whole number
                throw new ArgumentException("width / cols must be whole number");
            }

            this.animatedObjects = new LinkedList<IBoundingBox>();
            this.fields = new BoundingField[cols, rows];

            this.rows = rows;
            this.cols = cols;

            this.width = width;
            this.height = height;

            this.fieldWidth = this.width / this.cols;
            this.fieldHeight = this.height / this.rows;

            for (int x = 0; x < this.cols; x++)
            {
                for (int y = 0; y < this.rows; y++)
                {
                    this.fields[x, y] = new BoundingField(x, y);
                }
            }

        }

        /// <summary>
        /// Goes though the bounding container and takes over his bounding boxes.
        /// Animated Objects do not need to be taken over
        /// </summary>
        /// <param name="bC">Container that holds bounding boxes to add</param>
        public void TakeOverBoundingContainer(BoundingContainer bC) {
            Vector2 worldTrans = bC.parentElement.Location;

            int x;
            int y;

            foreach (IBoundingBox b in bC.boundingBoxes)
            {

                if (b.GetType() == typeof(BoundingCircle))
                {
                    //Strategy: Make a box around the circle
                    BoundingCircle bCir = (BoundingCircle)b;

                    //position of the circle self (which field)
                    x = ((int)(bCir.position.X + worldTrans.X) / fieldWidth);        //TODO check if this is rounded down
                    y = ((int)(bCir.position.Y + worldTrans.Y) / fieldHeight);


                    //amount of fields x and y (rounded up) 
                    //2*cicFieldsX + (1 where the ceneter is) will make the width of the square which includes the circle)
                    //2*cicFieldsY + (1 where the ceneter is) will make the height of the square which includes the circle)
                    int circFieldsX = (int)Math.Ceiling((double)(bCir.radius * 1f / fieldWidth));
                    int circFieldsy = (int)Math.Ceiling((double)(bCir.radius * 1f / fieldHeight));

                    if ((bCir.position.X + worldTrans.X) < 0)
                    {
                        x = (x - 1);
                        if (circFieldsX + x > 0)
                        {
                            circFieldsX++;
                        }
                    }

                    if ((bCir.position.Y + worldTrans.Y) < 0)
                    {
                        y = (y - 1);
                        if (circFieldsy + y > 0)
                        {
                            circFieldsy++;
                        }
                    }

                    //go from the left to the right of the square
                    for (int h = x - circFieldsX; h <= x + circFieldsX; h++)
                    {
                        if (h < 0 || h >= this.cols)
                        {
                            //if h is out of raster skip this
                            continue;
                        }
                        for (int ver = y - circFieldsy; ver <= y + circFieldsy; ver++)
                        {
                            if (ver < 0 || ver >= this.rows)
                            {
                                //if ver is out of raster skip this
                                continue;
                            }
                            //this is a field which should hold a reference to the boundingCircle
                            this.fields[h, ver].addReference(bCir);     //duplicate entries will be neglected
                        }
                    }
                }
                else        //if(b.GetType() == typeof(BoundingCircle))
                {
                    //line
                    BoundingLine bL = (BoundingLine)b;

                    float posX = bL.position.X + worldTrans.X;
                    float posY = bL.position.Y + worldTrans.Y;

                    //position of the line base (which field)
                    x = ((int)posX / fieldWidth);
                    y = ((int)posY / fieldHeight);

                    //add the start
                    if (IsWithinBounds(x, y))
                        this.fields[x, y].addReference(bL);     //duplicate entries will be neglected

                    //define unitvector from position to target
                    Vector2 unitV = bL.target - bL.position;
                    unitV.Normalize();


                    if (unitV.X > 0)
                    {
                        takeOverBoundingLineLeftToRight(unitV, posX, posY,ref x,ref y, bL, worldTrans);
                    }
                    else
                    {
                        takeOverBoundingLineRightToLeft(unitV, posX, posY,ref x,ref y, bL, worldTrans);
                    }

                    //at this point all x fields have been added but there might be some y fields who get touched but are not referenced yet.
                    //that are all fields that are above the last x cross (like directly under the target)


                    int endField = ((int)(bL.target.Y + worldTrans.Y) / fieldHeight);

                    if (unitV.Y > 0)        //heading down
                    {
                        for (int i = y + 1; i <= endField; i++)
                        {
                            if (IsWithinBounds(x, i))
                                this.fields[x, i].addReference(bL);
                        }
                    }
                    else   //heading up
                    {
                        for (int i = y - 1; i >= endField - 1; i--)
                        {
                            if (IsWithinBounds(x, i))
                                this.fields[x, i].addReference(bL);
                        }
                    }
                }       //if(b.GetType() == typeof(BoundingCircle))
            }       //foreach (IBoundingBox b in bC.boundingBoxes)
        }

        /// <summary>
        /// Goes through all elements given and adds their bounding boxes to the raster.
        /// Animated Objects do not need to be taken over
        /// </summary>
        /// <param name="eles">Elements to add</param>
        public void takeOverBoundingBoxes(IEnumerable<PinballElement> eles)
        {

            foreach (PinballElement pE in eles)
            {
                if (pE is AnimatedObject)
                {
                    // Special treatment
                    foreach (BoundingBox bb in pE.boundingContainer.boundingBoxes)
                    {
                        this.addAnimatedObject(bb);
                    }
                }
                else
                {
                    BoundingContainer bC = pE.getBoundingContainer();

                    TakeOverBoundingContainer(bC);
                }
            
            }       //foreach (PinballElement pE in eles)
        }

        /// <summary>
        /// Submethod to TakeOverBoundingContainer - not intended to be called outside of TakeOverBoundingContainer
        /// </summary>
        private void takeOverBoundingLineLeftToRight(Vector2 unitV, float posX, float posY,ref int x,ref int y, BoundingLine bL, Vector2 worldTrans)
        {
            if (unitV.X == 0)
            {
                return;     //just y remains => the whole line is on one column
            }

            float deltaRight = fieldWidth - (posX - fieldWidth * x);     //distance from right end of field to the position
            float factorToNextXCross = deltaRight / unitV.X;

            while (posX < (bL.target.X + worldTrans.X))
            {
                factorToNextXCross = deltaRight / unitV.X;

                //direction left to right
                float nextXCross = posX + deltaRight;
                float nextXCrossY = posY + factorToNextXCross * unitV.Y;      //because factorToNextXCross time unitvector in x direction = new point

                //float newFieldXIdx = ((int)nextXCross / fieldWidth);
                int newFieldYIdx = ((int)(nextXCrossY) / fieldHeight);

                if (unitV.Y > 0)        //heading down
                {
                    if (newFieldYIdx > y)
                    {
                        //we entered new y field(s)
                        for (int i = y; i < newFieldYIdx; i++)
                        {
                            if (IsWithinBounds(x, i))
                                this.fields[x, i].addReference(bL);      //this is a field that the line goes through
                        }
                    }
                }
                else
                {
                    //heading up
                    if (newFieldYIdx < y)
                    {
                        //we entered new y field(s)
                        for (int i = y; i > newFieldYIdx; i--)
                        {
                            if (IsWithinBounds(x, i))
                                this.fields[x, i].addReference(bL);      //this is a field that the line goes through
                        }
                    }
                }

                //update field index
                x++;
                y = newFieldYIdx;

                //update position
                posX = nextXCross;
                posY = nextXCrossY;

                //since we are at the border of a field the deltaRight will allways be the full field width
                deltaRight = fieldWidth;

                if (posX <= bL.target.X + worldTrans.X)
                {
                    if (IsWithinBounds(x, newFieldYIdx))
                        this.fields[x, newFieldYIdx].addReference(bL);       //add the next x field (since the line just gets crossed)
                }
            }

        }

        /// <summary>
        /// Submethod to TakeOverBoundingContainer - not intended to be called outside of TakeOverBoundingContainer
        /// </summary>
        private void takeOverBoundingLineRightToLeft(Vector2 unitV, float posX, float posY,ref int x,ref int y, BoundingLine bL, Vector2 worldTrans)
        {
            if (unitV.X == 0)
            {
                return;     //just y remains => the whole line is on one column
            }

            float deltaLeft = (posX - fieldWidth * x);     //distance from right end of field to the position
            float factorToNextXCross = deltaLeft / -unitV.X;


            while (posX > (bL.target.X + worldTrans.X))
            {
                factorToNextXCross = deltaLeft / -unitV.X;

                //direction left to right
                float nextXCross = posX - deltaLeft;
                float nextXCrossY = posY + factorToNextXCross * unitV.Y;      //because factorToNextXCross time unitvector in x direction = new point

                //float newFieldXIdx = ((int)nextXCross / fieldWidth);
                int newFieldYIdx = ((int)(nextXCrossY-1) / fieldHeight);

                if (unitV.Y > 0)        //heading down
                {
                    if (newFieldYIdx > y)
                    {
                        //we entered new y field(s)
                        for (int i = y; i < newFieldYIdx; i++)
                        {
                            if (IsWithinBounds(x, i))
                                this.fields[x, i].addReference(bL);      //this is a field that the line goes through
                        }
                    }
                }
                else
                {
                    //heading up
                    if (newFieldYIdx < y)
                    {
                        //we entered new y field(s)
                        for (int i = y; i > newFieldYIdx; i--)
                        {
                            if (IsWithinBounds(x, i))
                                this.fields[x, i].addReference(bL);      //this is a field that the line goes through
                        }
                    }
                }

                //update field index
                x--;
                y = newFieldYIdx;

                //update position
                posX = nextXCross;
                posY = nextXCrossY;

                //since we are at the border of a field the deltaRight will allways be the full field width
                deltaLeft = fieldWidth;
                if (x >= 0)
                {
                    if (IsWithinBounds(x, newFieldYIdx))
                        this.fields[x, newFieldYIdx].addReference(bL);       //add the next x field (since the line just gets crossed)
                }

            }
        }

        private bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < cols && y >= 0 && y < rows;
        }

        /// <summary>
        /// This method takes a ball and handles the collision of it with all other bounding boxes in this raster
        /// </summary>
        /// <param name="ball">Ball that causes collisions</param>
        public CollisionResult handleCollision(Ball ball)
        {
            //Collide first with animated object then with object around ball self
            LinkedList<IBoundingBox> history = new LinkedList<IBoundingBox>();
            //Collision logic
            foreach (IBoundingBox b in this.animatedObjects)
            {
                //does any animated object intersec?
                if (history.Contains(b))
                {
                    continue;
                }

                Vector2 hitPoint = new Vector2(0, 0);
                if (b.intersec(ball.getBoundingContainer().getBoundingBoxes()[0], out hitPoint, ball.Velocity))       //specify bounding box of ball
                {
                    history.AddFirst(b);
                    AnimatedObject aniO = ((AnimatedObject)b.BoundingContainer.parentElement);
                    aniO.notifyIntersection(ball);
                    if (!aniO.pureIntersection)
                    {
                       
                        Vector2 rotationCenter = aniO.currentRotationCenter + aniO.Location;

                        Vector2 aniNorm = (hitPoint - rotationCenter).Normal();

                        Vector2 h = -hitPoint + (ball.Location + new Vector2((float)ball.Width / 2, (float)ball.Height / 2));

                        Vector2 turnspeed = aniO.angularVelocity * aniNorm;
                        if (h.X * aniNorm.X < 0 || h.Y * aniNorm.Y < 0)
                        {
                            aniNorm = -aniNorm;
                        }

                        ball.Velocity += -turnspeed;

                        Vector2 newDirection = b.reflect(ball.Velocity, hitPoint, ball.Location + ball.getBoundingContainer().getBoundingBoxes()[0].position);
                        Vector2 outOfAreaPush = b.getOutOfAreaPush((int)ball.Width, hitPoint, newDirection, ball.Location);

                        outOfAreaPush += (aniO.angualrVelocityPerFrame) * aniNorm;        //push with the amout of the turn of animation until next update

                        ball.Location = (hitPoint - new Vector2((float)ball.Width / 2, (float)ball.Height / 2)) + outOfAreaPush;     // + (ball.Width / 1.5f) * Vector2.Normalize(hitPoint - b.BoundingContainer.parentElement.Location))

                        ball.Velocity = b.reflectManipulation(newDirection);
                        this.hitPointDebug = hitPoint;
                    }
                }
            }

            int fieldWidth = this.width / this.cols;
            int fieldHeight = this.height / this.rows;

            int x = (int)(ball.X / fieldWidth);
            int y = (int)(ball.Y / fieldHeight);



            for (int x1 = x - 1; x1 <= x + 1; x1++)
            {
                if (x1 >= 0 && x1 < this.cols)
                {
                    for (int y1 = y - 1; y1 <= y + 1; y1++)
                    {
                        if (y1 >= 0 && y1 < this.rows)
                        {
                            foreach (IBoundingBox b in this.fields[x1, y1].getReferences())
                            {
                                if (history.Contains(b))
                                {
                                    continue;
                                }

                                Vector2 hitPoint = new Vector2(0, 0);
                                if (b.intersec(ball.getBoundingContainer().getBoundingBoxes()[0], out hitPoint, ball.Velocity))       //specify bounding box of ball
                                {
                                    history.AddFirst(b);
                                    //collision
                                    b.BoundingContainer.parentElement.notifyIntersection(ball);
                                    if (!b.BoundingContainer.parentElement.pureIntersection)
                                    {
                                        Vector2 newDirection = b.reflect(ball.Velocity, hitPoint, ball.Location + ball.getBoundingContainer().getBoundingBoxes()[0].position);
                                        Vector2 outOfAreaPush = b.getOutOfAreaPush((int)ball.Width, hitPoint, newDirection, ball.Location);

                                        ball.Location = (hitPoint - new Vector2((float)ball.Width / 2, (float)ball.Height / 2)) + outOfAreaPush;     // + (ball.Width / 1.5f) * Vector2.Normalize(hitPoint - b.BoundingContainer.parentElement.Location))

                                        ball.Velocity = b.reflectManipulation(newDirection);
                                        this.hitPointDebug = hitPoint;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            return new CollisionResult(history);
        }


        /// <summary>
        /// Adds an animated object to the raster (no need to call take over bounding boxes on this.
        /// </summary>
        /// <param name="aO">Element to add</param>
        public void addAnimatedObject(IBoundingBox aO)
        {
            this.animatedObjects.AddLast(aO);
        }

        /// <summary>
        /// Returns all animated objects
        /// </summary>
        /// <returns>List of animated objects</returns>
        public LinkedList<IBoundingBox> getAnimatedObjects()
        {
            return this.animatedObjects;
        }
        /// <summary>
        /// Removes an animated object
        /// </summary>
        /// <param name="aO">The object to be removed</param>
        public void removeAnimatedObject(IBoundingBox aO)
        {
            this.animatedObjects.Remove(aO);
        }

        /// <summary>
        /// Returns the fieldWidth // for test purposes
        /// </summary>
        /// <returns>fieldWidth</returns>
        public int getFieldWidth()
        {
            return this.fieldWidth;
        }

        /// <summary>
        /// Returns the fieldHeight // for test purposes
        /// </summary>
        /// <returns>fieldHeight</returns>
        public int getFieldHeight()
        {
            return this.fieldHeight;
        }

        public BoundingField getBoundingField(int x, int y)
        {
            return this.fields[x, y];
        }


    }
}