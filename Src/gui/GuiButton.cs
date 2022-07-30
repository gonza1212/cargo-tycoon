using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace cargoTycoon
{
    class GuiButton : GuiComponent
    {
        private Texture2D bg, bgNormal, bgHover;
        private bool mReleased;

        public GuiButton(Texture2D bgNormal, int x, int y)
        {
            this.bgNormal = bgNormal;
            this.bgHover = bgNormal;
            this.bg = bgNormal;
            this.clickableArea = new Rectangle(x, y, bg.Width, bg.Height);
            this.quickPos = new Vector2(this.X(), this.Y());
            this.mReleased = false;
        }

        public GuiButton(Texture2D bgNormal, Texture2D bgHover, int x, int y)
        {
            this.bgNormal = bgNormal;
            this.bgHover = bgHover;
            this.bg = bgNormal;
            this.clickableArea = new Rectangle(x, y, bg.Width, bg.Height);
            this.quickPos = new Vector2(this.X(), this.Y());
            this.mReleased = false;
        }

        public override void debugDraw()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            Globals.spriteBatch.Draw(bg, quickPos, Color.White);
        }

        public override void Update()
        {
            if (hover())
                bg = bgHover;
            else
                bg = bgNormal;
        }

        public bool Click()
        {
            return Mouse.GetState().LeftButton == ButtonState.Pressed && clickableArea.Contains(Mouse.GetState().Position);
        }
    }
}
