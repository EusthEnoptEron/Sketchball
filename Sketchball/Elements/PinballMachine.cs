﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    [Serializable]
    public class PinballMachine : ICloneable
    {
        public delegate void CollisionEventHandler(PinballElement sender);
        public delegate void GameOverEventHandler();

        public event CollisionEventHandler Collision;
        public event GameOverEventHandler GameOver;

        // 500px = 1m
        public const float PIXELS_TO_METERS_RATIO = 500f / 1;

        public ElementCollection Elements {get; private set;}
        public Size Bounds {get; private set;}

        public int Width { get { return Bounds.Width; } }
        public int Height { get { return Bounds.Height; } }

        public float Gravity = 9.81f;

        /// <summary>
        /// Tilt of the pinball machine in radians.
        /// </summary>
        public float Angle = (float)(Math.PI / 180 * 30);

        internal InputManager Input { get; private set; }

        public PinballMachine(Size bounds)
        {
            Elements = new ElementCollection(this);
            Bounds = bounds;

            Input = new InputManager();
        }


        public Vector2 Acceleration
        {
            get
            {
                return new Vector2(0, (float)Math.Sin(Angle) * Gravity * PIXELS_TO_METERS_RATIO);
            }
        }

        public void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.Black, 0, 0, Width, Height);
            foreach (PinballElement element in Elements)
            {
                GraphicsState gstate = g.Save();

                g.TranslateTransform(element.X, element.Y);
                element.Draw(g);

                g.Restore(gstate);
            }
        }
        public virtual void Update(long elapsed)
        {
        }


        public void Add(PinballElement element)
        {
            Elements.Add(element);
        }

        public bool Remove(PinballElement element)
        {
            return Elements.Remove(element);
        }


        public object Clone()
        {
            PinballMachine machine = (PinballMachine)this.MemberwiseClone();

            machine.Bounds = new Size(Width, Height);

            // Clone elements
            machine.Elements = new ElementCollection(machine);
            foreach (PinballElement element in Elements)
            {
                machine.Elements.Add((PinballElement)element.Clone());
            }

            return machine;
        }
    }
}
