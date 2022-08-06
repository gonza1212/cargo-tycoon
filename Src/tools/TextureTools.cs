using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace cargoTycoon
{
    class TextureTools
    {

        public static Texture2D subTexture(Texture2D original, int subWidth, int subHeight)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, subWidth, subHeight);
            Texture2D subTexture = new Texture2D(Globals.graphics.GraphicsDevice, sourceRectangle.Width, sourceRectangle.Height);
            Color[] data = new Color[sourceRectangle.Width * sourceRectangle.Height];
            original.GetData(0, sourceRectangle, data, 0, data.Length);
            subTexture.SetData(data);
            return subTexture;
        }

        public static Texture2D subTexture(Texture2D original, int x, int y, int subWidth, int subHeight)
        {
            Rectangle sourceRectangle = new Rectangle(x, y, subWidth, subHeight);
            Texture2D subTexture = new Texture2D(Globals.graphics.GraphicsDevice, sourceRectangle.Width, sourceRectangle.Height);
            Color[] data = new Color[sourceRectangle.Width * sourceRectangle.Height];
            original.GetData(0, sourceRectangle, data, 0, data.Length);
            subTexture.SetData(data);
            return subTexture;
        }
    }
}
