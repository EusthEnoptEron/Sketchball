﻿using GlideTween;
using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    [DataContract]
    public abstract class PinballElement : ICloneable
    {
        [DataMember]
        public int Width = 100;
        [DataMember]
        public int Height = 100;

        public float X { get { return Location.X; } set { Location.X = value; } }
        public float Y { get { return Location.Y; } set { Location.Y = value; } }

        [DataMember]
        public Vector2 Location = new Vector2();

        private PinballMachine _machine = null;

        public bool pureIntersection{ get; protected set;}

        [DataMember]
        public PinballMachine World { 
            get {
                return _machine;
            }
 			internal set
            {
                if (_machine != null && _machine is PinballGameMachine)
                {
                    LeaveMachine((PinballGameMachine)_machine);
                }
                _machine = value;

                if (_machine != null && _machine is PinballGameMachine)
                {
                    EnterMachine((PinballGameMachine)_machine);
                }
            }
        }



        //Collision detection stuff
        private BoundingContainer _boundingContainer = null;

        /// <summary>
        /// Lazy-loading bounding container.
        /// </summary>
        public BoundingContainer boundingContainer
        {
            get
            {
                if (_boundingContainer == null)
                {
                    InitBoundingContainer();
                }
                return _boundingContainer;
            }
            private set
            {
                _boundingContainer = boundingContainer;
            }
        }

        public PinballElement() : this(0, 0)
        {
            pureIntersection = false;
        }

        public PinballElement(float X, float Y)
        {
            pureIntersection = false;
            this.X = X;
            this.Y = Y;
        }

        private void InitBoundingContainer()
        {
            _boundingContainer = new BoundingContainer(this);
            InitBounds();
        }

        protected abstract void InitBounds();

        [DataMember]
        public int Value { get; protected set; }

        public virtual void Update(long delta) {}

        public abstract void Draw(Graphics g);

        public virtual bool Contains(Point point)
        {
            return false;
            //throw new NotImplementedException();
        }

        public object Clone()
        {
            PinballElement element = (PinballElement)MemberwiseClone();
            element._boundingContainer = null;

            OnClone(element);
            return element;
        }

        /// <summary>
        /// Does additional stuff when an element is cloned.
        /// </summary>
        /// <param name="element"></param>
        protected virtual void OnClone(PinballElement element)
        {
        }

        /// <summary>
        /// Sets up event listeners when a new machine is entered.
        /// </summary>
        /// <param name="machine"></param>
  		protected virtual void EnterMachine(PinballGameMachine machine)
        {
        }

        /// <summary>
        /// Removes event listeners when a machine is left
        /// </summary>
        /// <param name="machine"></param>
        protected virtual void LeaveMachine(PinballGameMachine machine)
        {
        }

        public BoundingContainer getBoundingContainer()
        {
            return this.boundingContainer;
        }

        public Vector2 getLocation()
        {
            return this.Location;
        }

        public void setLocation(Vector2 newLoc)
        {
            this.Location.X = newLoc.X;
            this.Location.Y = newLoc.Y;
        }

        public virtual void notifyIntersection(Ball b)
        {
            //placeholder
        }

        
    }
}
