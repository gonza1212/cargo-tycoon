using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace cargoTycoon
{
    class MapMenu
    {
        private Texture2D bg;
        private Vector2 pos;
        private GuiButton close;
        private GuiButton finishRoutePlan;
        private bool open;
        private int truckSelected; // esto sirve para asignar rapidamente hojas de ruta a un camion si se abrio este menu desde un camion y no desde la interfaz
        private List<Tract> routePlan;
        private bool routePlanFinished;
        private List<Truck> trucksInTravel;

        public MapMenu()
        {
            bg = Globals.content.Load<Texture2D>("bgMapMenu");
            pos = new Vector2(10, 20);
            close = new GuiButton(Globals.content.Load<Texture2D>("buttonCancel"), Globals.content.Load<Texture2D>("buttonCancelHover"), (int)pos.X + bg.Width - 56, (int)pos.Y + 8);
            finishRoutePlan = new GuiButton(Globals.content.Load<Texture2D>("buttonFinishRoutePlan"), Globals.content.Load<Texture2D>("buttonFinishRoutePlanHover"), (int)pos.X + bg.Width - 136, (int)pos.Y + bg.Height - 40);
            open = false;
            routePlan = new List<Tract>();
            routePlanFinished = false;
            trucksInTravel = new List<Truck>();
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(bg, pos, Color.White);
            Globals.spriteBatch.Draw(Map.bg, new Vector2(pos.X + 8, pos.Y + 8), Color.White);
            close.Draw();
            finishRoutePlan.Draw();
            for (int i = 0; i < Map.towns.Count; i++)
            {
                Map.towns[i].Draw();
            }
            string tracts = "";
            for (int i = 0; i < routePlan.Count; i++)
            {
                tracts += routePlan[i].Origin() + "-" + routePlan[i].Destination() + " | ";
            }
            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), tracts, new Vector2(10, 660), Color.Black);
            for (int i = 0; i < trucksInTravel.Count; i++)
            {
                if(trucksInTravel[i].InTravel())
                {
                    Globals.spriteBatch.Draw(trucksInTravel[i].Overworld(), trucksInTravel[i].OverworldPos(), Color.White);
                }
            }
        }

        public void Update(Action<string, int> actionForGarageState)
        {
            close.Update();
            finishRoutePlan.Update();
            if (close.Click())
            {
                SetOpen(false);
                routePlan.Clear();
                actionForGarageState("closeMapForTruck", truckSelected);
                truckSelected = -1;
                routePlanFinished = false;
            }
            for (int i = 0; i < Map.towns.Count; i++)
            {
                if(Map.towns[i].Click())
                {
                    if(routePlan.Count == 0)
                    {
                        routePlan.Add(new Tract(0, i, Map.routes[0].Distance(), 0, 0, Map.routes[0].LastRouteUnitIndex()));
                    } else
                    {
                        routePlan.Add(new Tract(routePlan[routePlan.Count - 1].Destination(), i, Map.routes[0].Distance(), 0, Map.routes[i].LastRouteUnitIndex(), 0));
                    }
                }
            }
            if(finishRoutePlan.Click())
            {
                SetOpen(false);
                routePlanFinished = true;
                actionForGarageState("closeMapForTruck", truckSelected);
                truckSelected = -1;
            }
        }

        public bool IsOpen()
        {
            return this.open;
        }

        public void SetOpen(bool open)
        {
            this.open = open;
        }

        public void SetTruckSelected(int truckSelected)
        {
            this.truckSelected = truckSelected;
        }

        public List<Tract> GetRoutePlan()
        {
            return this.routePlan;
        }

        public void CleanRoutePlan()
        {
            this.routePlan.Clear();
            this.routePlanFinished = true;
        }

        public void TrucksInTravel(List<Truck> trucks)
        {
            this.trucksInTravel = trucks;
        }
    }
}
