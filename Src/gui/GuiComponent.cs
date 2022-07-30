using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace cargoTycoon
{
    abstract class GuiComponent
    {
        protected Rectangle clickableArea;
        protected Vector2 quickPos; // sirve para obtener la posicion a la hora de dibujar sin tener que crear un nuevo vector2
        public GuiComponent()
        {
        }

        public abstract void Update();

        public abstract void Draw();

        public abstract void debugDraw();

        public bool hover()
        {
            return this.clickableArea.Contains(Mouse.GetState().Position);
        }

        public int X()
        {
            return this.clickableArea.X;
        }

        public int Y()
        {
            return this.clickableArea.Y;
        }

        public int Width()
        {
            return this.clickableArea.Width;
        }

        public int Height()
        {
            return this.clickableArea.Height;
        }

        public void Teleport(int x, int y)
        {
            this.clickableArea.X = x;
            this.clickableArea.Y = y;
            this.quickPos = new Vector2(x, y);
        }
    }
}
