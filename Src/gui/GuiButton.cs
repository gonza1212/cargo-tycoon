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
        private bool toggler;
        private bool toggle;

        public GuiButton(Texture2D bgNormal, int x, int y)
        {
            this.bgNormal = bgNormal;
            this.bgHover = bgNormal;
            this.bg = bgNormal;
            this.clickableArea = new Rectangle(x, y, bg.Width, bg.Height);
            this.quickPos = new Vector2(this.X(), this.Y());
            this.mReleased = false;
            this.toggler = false;
            this.toggle = false;
        }

        public GuiButton(Texture2D bgNormal, Texture2D bgHover, int x, int y)
        {
            this.bgNormal = bgNormal;
            this.bgHover = bgHover;
            this.bg = bgNormal;
            this.clickableArea = new Rectangle(x, y, bg.Width, bg.Height);
            this.quickPos = new Vector2(this.X(), this.Y());
            this.mReleased = false;
            this.toggler = false;
            this.toggle = false;
        }

        public GuiButton(Texture2D bgNormal, Texture2D bgHover, int x, int y, bool toggler)
        {
            this.bgNormal = bgNormal;
            this.bgHover = bgHover;
            this.bg = bgNormal;
            this.clickableArea = new Rectangle(x, y, bg.Width, bg.Height);
            this.quickPos = new Vector2(this.X(), this.Y());
            this.mReleased = false;
            this.toggler = toggler;
            this.toggle = false;
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
            if(IsToggle())
            {
                if(toggle)
                    bg = bgHover;
                else
                    bg = bgNormal;
            } else
            {
                if (hover())
                    bg = bgHover;
                else
                    bg = bgNormal;
            }
        }

        public bool Click()
        {
            if (clickableArea.Contains(Mouse.GetState().Position)) {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && mReleased)
                {
                    mReleased = false;
                    return true;
                }
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    mReleased = true;
                }
            }
            return false;
        }

        public void SetToggler(bool toggler)
        {
            this.toggler = toggler;
        }

        public bool IsToggler()
        {
            return this.toggler;
        }

        public void Toggle()
        {
            this.toggle = !toggle;
        }

        public void Toggle(bool toggle)
        {
            this.toggle = toggle;
        }

        public bool IsToggle()
        {
            return this.toggle;
        }
    }
}
