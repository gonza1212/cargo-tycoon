using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace cargoTycoon
{
    class Map
    {
        public static List<Route> routes = CreateRoutes(); // lista de listas de puntos con datos de tipo y estado de ruta
        public static List<GuiButton> towns = CreateTownsButtons(); // incluye ciudades y capitales
        public static Texture2D bg = Globals.content.Load<Texture2D>("map");

        private static List<Route> CreateRoutes()
        {
            List<Route> list = new List<Route>();
            list.Add(new Route("Ruta 1", 450));
            list[0].AddRouteUnit(new RouteUnit(new Vector2(665, 375)));
            list[0].AddRouteUnit(new RouteUnit(new Vector2(655, 365)));
            list[0].AddRouteUnit(new RouteUnit(new Vector2(628, 375)));
            list[0].AddRouteUnit(new RouteUnit(new Vector2(610, 386)));
            list[0].AddRouteUnit(new RouteUnit(new Vector2(595, 385)));
            list[0].AddRouteUnit(new RouteUnit(new Vector2(595, 365)));
            list[0].AddRouteUnit(new RouteUnit(new Vector2(570, 355)));
            return list;
        }

        private static List<GuiButton> CreateTownsButtons()
        {
            List<GuiButton> list = new List<GuiButton>();
            list.Add(new GuiButton(Globals.content.Load<Texture2D>("buttonCity"), 690, 375));
            list.Add(new GuiButton(Globals.content.Load<Texture2D>("buttonCity"), 535, 345));
            return list;
        }

        // falta un metodo para reubicar los botones cuando se redimensiona el frame
    }
}
