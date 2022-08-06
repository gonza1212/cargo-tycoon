using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace cargoTycoon
{
    class Truck
    {
        // datos basicos del camion
        private int id; // identifica al camion de otras instancias del mismo modelo y marca
        private string brand; // marca
        private string model;
        private int dragCapacity;
        private int price;
        // datos necesarios cuando pertenece al jugador
        private string issue;
        private int routePlanId;
        private RoutePlan routePlan;
        // graphics
        private Vector2 pos;
        private Vector2 overworldPos;
        private Texture2D bgForSeller;
        private Texture2D sprite; // sprite para garage
        private Texture2D overworld; // sprite para overworld
        private GuiButton alert;
        private GuiButton cargo;
        private bool menuOpen;
        // menu
        private GuiButton createRoutePlan;
        private GuiButton close;
        private GuiButton shipment;
        private bool creatingRoutePlan;
        // cargas disponibles
        private List<Cargo> avalaibleCargo;
        private List<Cargo> selectedCargo;
        private List<GuiButton> selectableCargo;
        private int totalWeight;
        // shipment
        private bool inTravel;
        private int currentTract;
        private int kmTraveled;
        private float deltaKmTraveled;
        private float speed;
        private int dayForRest;
        private float kmPerUnit;
        private float deltaUnitTraveled;
        private int currentRouteUnit;

        public Truck(int id, string brand, string model, int dragCapacity, int price)
        {
            this.id = id;
            this.brand = brand;
            this.model = model;
            this.dragCapacity = dragCapacity;
            this.price = price;
            this.routePlanId = -1;
            this.issue = "ok";
            this.alert = new GuiButton(Globals.content.Load<Texture2D>("buttonExclamation"), Globals.content.Load<Texture2D>("buttonExclamationHover"), 10, 10);
            this.cargo = new GuiButton(Globals.content.Load<Texture2D>("buttonCargo"), Globals.content.Load<Texture2D>("buttonCargoHover"), 10, 10);
            this.shipment = new GuiButton(Globals.content.Load<Texture2D>("buttonShipment"), Globals.content.Load<Texture2D>("buttonShipmentHover"), 10, 10);
            this.menuOpen = false;
            this.pos = new Vector2(0, 0);
            this.overworldPos = new Vector2(0, 0);
            this.createRoutePlan = new GuiButton(Globals.content.Load<Texture2D>("buttonCreateRoutePlan"), Globals.content.Load<Texture2D>("buttonCreateRoutePlanHover"), 10, 10);
            this.close = new GuiButton(Globals.content.Load<Texture2D>("buttonCancel"), Globals.content.Load<Texture2D>("buttonCancelHover"), (int)pos.X + Globals.content.Load<Texture2D>("bgTruckMenu").Width - 56, (int)pos.Y + 8);
            this.creatingRoutePlan = false;
            this.inTravel = false;
            this.avalaibleCargo = new List<Cargo>();
            this.selectedCargo = new List<Cargo>();
            this.selectableCargo = new List<GuiButton>();
            this.totalWeight = 0;
            this.kmTraveled = 0;
            this.deltaKmTraveled = 0;
            this.dayForRest = 210; // ups de un dia completo
            this.kmPerUnit = 0;
            this.deltaUnitTraveled = 0;
            this.currentRouteUnit = 0;
            overworld = Globals.content.Load<Texture2D>("truckMiniOverworld");
            switch (model)
            {
                case "Basic":
                    bgForSeller = Globals.content.Load<Texture2D>("truck00");
                    sprite = Globals.content.Load<Texture2D>("spriteTruck00");
                    break;
                case "Middle":
                    bgForSeller = Globals.content.Load<Texture2D>("truck01");
                    sprite = Globals.content.Load<Texture2D>("spriteTruck01");
                    break;
                case "Better":
                    bgForSeller = Globals.content.Load<Texture2D>("truck02");
                    sprite = Globals.content.Load<Texture2D>("spriteTruck02");
                    break;
            }
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(sprite, pos, Color.White);
            if (!IsMenuOpen())
            {
                cargo.Draw();
                if(HasIssue())
                    alert.Draw();
            }
            if (IsMenuOpen())
            {
                Globals.spriteBatch.Draw(Globals.content.Load<Texture2D>("bgTruckMenu"), pos, Color.White);
                Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), brand + " " + model, new Vector2(pos.X + 15, pos.Y + 15), Color.Black);
                Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), issue + "(km: " + kmTraveled + ")|kmPerUnit:" + kmPerUnit + "|speedPerUps:" + speed, new Vector2(pos.X + 15, pos.Y + 35), Color.Red);
                createRoutePlan.Draw();
                close.Draw();
                shipment.Draw();
                if (totalWeight > 0) {
                    int weightWidth = (totalWeight * 100 / dragCapacity) * Globals.content.Load<Texture2D>("totalWeight").Width / 100;
                    if(weightWidth > Globals.content.Load<Texture2D>("totalWeight").Width)
                        Globals.spriteBatch.Draw(Globals.content.Load<Texture2D>("totalWeightExceded"), new Vector2(pos.X + 9, pos.Y + 281), Color.White);
                    else
                        Globals.spriteBatch.Draw(TextureTools.subTexture(Globals.content.Load<Texture2D>("totalWeight"), weightWidth, Globals.content.Load<Texture2D>("totalWeight").Height), new Vector2(pos.X + 9, pos.Y + 281), Color.White);
                    Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "0Kg", new Vector2(pos.X + 8, pos.Y + 274), Color.Black);
                    Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), dragCapacity + "Kg", new Vector2(pos.X + Globals.content.Load<Texture2D>("bgTruckMenu").Width - 70, pos.Y + 274), Color.Black);
                }
                if (!HasIssue())
                {
                    for (int i = 0; i < avalaibleCargo.Count; i++)
                    {
                        selectableCargo[i].Draw();
                        Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), avalaibleCargo[i].Description() + " (" + avalaibleCargo[i].Weight() + "kg)", new Vector2(selectableCargo[i].X() + 4, selectableCargo[i].Y() + 8), Color.Black);
                    }
                }
            }
        }

        public void Update(Action<string, int> methodForGarageState)
        {
            if (inTravel)
            {
                if (kmTraveled >= routePlan.Tracts()[currentTract].Distance())
                {
                    dayForRest--;
                    if (dayForRest == 105)
                    {
                        int payment = 0;
                        for (int i = 0; i < selectedCargo.Count; i++)
                        {
                            payment += selectedCargo[i].Payment();
                        }
                        selectedCargo.Clear();
                        methodForGarageState("getCargoPayment", payment);
                    }
                    if (dayForRest == 0)
                    {
                        dayForRest = 210;
                        kmTraveled = 0;
                        currentTract++;
                        if (currentTract >= routePlan.Tracts().Count)
                        {
                            inTravel = false;
                            switch (model)
                            {
                                case "Basic":
                                    sprite = Globals.content.Load<Texture2D>("spriteTruck00");
                                    break;
                                case "Middle":
                                    sprite = Globals.content.Load<Texture2D>("spriteTruck01");
                                    break;
                                case "Better":
                                    sprite = Globals.content.Load<Texture2D>("spriteTruck02");
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    deltaKmTraveled += speed;
                    deltaUnitTraveled += speed;
                    while (deltaKmTraveled >= 1)
                    {
                        deltaKmTraveled--;
                        kmTraveled++;
                    }
                    if(deltaUnitTraveled >= kmPerUnit)
                    {
                        deltaUnitTraveled -= kmPerUnit;
                        Route r = Map.routes[routePlan.Tracts()[currentTract].RouteId()];
                        if(++currentRouteUnit < r.Units().Count)
                            overworldPos = r.Units()[currentRouteUnit].Pos();
                    }
                }
            }
            if (routePlanId < 0)
                issue = "* No hay Plan de Ruta asignado *";
            else
                issue = "ok";
            if (Alert().Click())
                MenuOpen(true);
            if (IsMenuOpen())
            {
                createRoutePlan.Update();
                close.Update();
                shipment.Update();
                if (!HasIssue())
                {
                    totalWeight = 0;
                    for (int i = 0; i < avalaibleCargo.Count; i++)
                    {
                        selectableCargo[i].Update();
                        if (selectableCargo[i].Click())
                        {
                            selectableCargo[i].Toggle();
                        }
                        if (selectableCargo[i].IsToggle())
                            totalWeight += avalaibleCargo[i].Weight();
                    }
                }
                if (createRoutePlan.Click())
                {
                    creatingRoutePlan = true;
                    methodForGarageState("openMapForTruck", id);
                }
                if(shipment.Click())
                {
                    for (int i = 0; i < avalaibleCargo.Count; i++)
                    {
                        if(selectableCargo[i].IsToggle())
                        {
                            selectedCargo.Add(avalaibleCargo[i]);
                        }
                    }
                    currentTract = 0;
                    Route r = Map.routes[routePlan.Tracts()[currentTract].RouteId()];
                    speed = 18 * 9; // 15km/h * 9hs de conduccion por dia (15 representa 75km/h)
                    speed = speed / 210; // cuantos km avanza por update
                    kmPerUnit = r.Distance() / r.Units().Count;
                    inTravel = true;
                    currentRouteUnit = routePlan.Tracts()[currentTract].FromRouteUnit();
                    overworldPos = r.Units()[currentRouteUnit].Pos();
                    switch (model)
                    {
                        case "Basic":
                            sprite = Globals.content.Load<Texture2D>("spriteTruck00InTravel");
                            break;
                        case "Middle":
                            sprite = Globals.content.Load<Texture2D>("spriteTruck01InTravel");
                            break;
                        case "Better":
                            sprite = Globals.content.Load<Texture2D>("spriteTruck02InTravel");
                            break;
                    }
                    menuOpen = false;
                    methodForGarageState("closeMapForTruck", id);
                }
                if(close.Click())
                {
                    menuOpen = false;
                    for (int i = 0; i < avalaibleCargo.Count; i++)
                    {
                        selectableCargo[i].Toggle(false);
                    }
                }
            }
            cargo.Update();
        }

        public string Brand()
        {
            return this.brand;
        }

        public string Model()
        {
            return this.model;
        }

        public int Price()
        {
            return this.price;
        }

        public Texture2D BgForSeller()
        {
            return this.bgForSeller;
        }

        public void Teleport(int x, int y)
        {
            this.pos.X = x;
            this.pos.Y = y;
            alert.Teleport(x + 55, y);
            cargo.Teleport(x + 55, y);
            createRoutePlan.Teleport(x + Globals.content.Load<Texture2D>("bgTruckMenu").Width - 134, y + Globals.content.Load<Texture2D>("bgTruckMenu").Height - 38);
            close.Teleport(x + Globals.content.Load<Texture2D>("bgTruckMenu").Width - 56, y + 8);
            shipment.Teleport(x + 6, y + Globals.content.Load<Texture2D>("bgTruckMenu").Height - 38);
        }

        public Texture2D Sprite()
        {
            return this.sprite;
        }

        public string Issue()
        {
            return this.issue;
        }

        public void Issue(string issue)
        {
            this.issue = issue;
        }

        public bool HasIssue()
        {
            return !this.issue.Contains("ok");
        }

        public int RoutePlanId()
        {
            return this.routePlanId;
        }

        public void RoutePlanId(int rp)
        {
            this.routePlanId = rp;
        }

        public GuiButton Alert()
        {
            return this.alert;
        }

        public bool IsMenuOpen()
        {
            return this.menuOpen;
        }

        public void MenuOpen(bool menuOpen)
        {
            this.menuOpen = menuOpen;
        }

        public bool IsCreatingRoutePlan()
        {
            return this.creatingRoutePlan;
        }

        public void SetCreatingRoutePlan(bool crp)
        {
            this.creatingRoutePlan = crp;
        }

        public int Id()
        {
            return this.id;
        }

        public void AvalaibleCargo(List<Cargo> ac)
        {
            this.avalaibleCargo = ac;
            for (int i = 0; i < ac.Count; i++)
            {
                selectableCargo.Add(new GuiButton(Globals.content.Load<Texture2D>("bgAvalaibleCargo"), Globals.content.Load<Texture2D>("bgAvalaibleCargoHover"), (int)pos.X + 10, (int)pos.Y + 84 + i * 50, true));
            }
        }

        public List<Cargo> AvalaibleCargo()
        {
            return avalaibleCargo;
        }

        public int DragCapacity()
        {
            return this.dragCapacity;
        }

        public void RoutePlan(RoutePlan routePlan)
        {
            this.routePlan = routePlan;
        }

        public bool InTravel()
        {
            return this.inTravel;
        }

        public Texture2D Overworld()
        {
            return this.overworld;
        }

        public Vector2 OverworldPos()
        {
            return this.overworldPos;
        }
    }
}
