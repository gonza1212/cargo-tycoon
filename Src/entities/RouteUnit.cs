using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace cargoTycoon
{
    class RouteUnit
    {
        private Vector2 pos;
        // clima, tipo de ruta, estado

        public RouteUnit(Vector2 pos)
        {
            this.pos = pos;
        }

        public Vector2 Pos()
        {
            return this.pos;
        }
    }
}
