using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace cargoTycoon
{
    // PARECE QUE VOY A TENER QUE HACER EL STACK DE MENUS ABIERTOS Y QUE, ADEMAS DE SERVIR PARA CERRAR EL MENU CORRECTO, SIRVE TAMBIEN PARA SABER CUANDO DIBUJARLOS
    class GarageState : GameState
    {
        private Company company;
        private Texture2D bg;
        private Vector2 pos;
        // no se si esto va aca
        private TruckSeller seller; // esto despues se reemplaza por una lista de concesionarios, aunque tal vez en otros states
        private GuiButton buttonMap;
        private GuiButton buttonTruckSeller;
        // cargas
        private List<Cargo> cargos;
        // menus
        private MapMenu mapMenu;
        // test
        private int clicks;


        public GarageState(StatesManager sm, Company company)
        {
            this.sm = sm;
            this.company = company;
            this.bg = Globals.content.Load<Texture2D>("bg0");
            this.pos = new Vector2(0, 0);
            this.buttonTruckSeller = new GuiButton(Globals.content.Load<Texture2D>("buttonTruck"), Globals.content.Load<Texture2D>("buttonTruckHover"), 10, 10);
            this.buttonMap = new GuiButton(Globals.content.Load<Texture2D>("buttonMap"), Globals.content.Load<Texture2D>("buttonMapHover"), 84, 10);
            this.seller = new TruckSeller("Basic - Venta de Camiones", Globals.content.Load<Texture2D>("bgTruckSeller"));
            this.mapMenu = new MapMenu();
            this.cargos = new List<Cargo>();
            seller.AddTruck(new Truck(0, "Foord", "Basic", 3000, 12500));
            seller.AddTruck(new Truck(1, "Foord", "Middle", 3500, 15000));
            seller.AddTruck(new Truck(2, "Foord", "Better", 4000, 17500));
            cargos.Add(new Cargo("mudanza familiar", 650, 800));
            cargos.Add(new Cargo("cargamento de colchones para tienda", 1500, 1000));
            cargos.Add(new Cargo("repuestos de auto", 2570, 1250));
            this.clicks = 0;
        }

        public override void DebugDraw()
        {
            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "Camiones: " + company.Trucks().Count + " | Planes de Ruta: " + company.RoutePlans().Count + " | Click Counter: " + clicks + " | Cargas: " + cargos.Count, new Vector2(150, 10), Color.Black);
        }
        
        public override void Draw()
        {
            // basic
            Globals.spriteBatch.Draw(bg, pos, Color.White);
            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "$" + company.Money(), new Vector2(1040, 18), Color.Black);
            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), company.Date().DateToString(), new Vector2(1182, 55), Color.Black);
            // gui
            buttonTruckSeller.Draw();
            buttonMap.Draw();
            // trucks
            for (int i = 0; i < company.Trucks().Count; i++)
            {
                company.Trucks()[i].Draw();                    
            }
            // menus
            if (seller.IsShowingStock())
                seller.Draw();
            if(mapMenu.IsOpen())
                mapMenu.Draw();
        }

        public override void LocateGui()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            // basic
            company.Date().Update();
            // gui
            buttonTruckSeller.Update();
            buttonMap.Update();
            // logic
            for (int i = 0; i < company.Trucks().Count; i++)
            {
                company.Trucks()[i].Update(RunAction);
                if (!company.Trucks()[i].HasIssue())
                    company.Trucks()[i].AvalaibleCargo(cargos);
                else
                    company.Trucks()[i].AvalaibleCargo().Clear();
            }
            // menus
            if (seller.IsShowingStock())
            {
                seller.Update();
                if (seller.IsBought())
                {
                    Truck t = seller.Trucks()[seller.TruckSelected()];
                    seller.Trucks().RemoveAt(seller.TruckSelected());
                    t.Teleport(288 + company.Trucks().Count * 240, 350);
                    company.SubMoney(t.Price());
                    company.AddTruck(t);
                    seller.Close();
                }
            }
            if (buttonTruckSeller.Click())
                seller.SetShowingStock(true);
            if (buttonMap.Click()) {
                mapMenu.TrucksInTravel(company.Trucks());
                mapMenu.SetOpen(true);
            }
            if (mapMenu.IsOpen())
            {
                mapMenu.TrucksInTravel(company.Trucks());
                mapMenu.Update(RunAction);
            }
        }

        /**
         * Metodo que ejecuta las acciones de menus que no tienen comunicacion entre si
         * TEST
         */
        public void RunAction(string action, int value)
        {
            switch(action)
            {
                case "closeMapForTruck":
                    for (int i = 0; i < company.Trucks().Count; i++)
                    {
                        if (company.Trucks()[i].Id() == value)
                        {
                            company.Trucks()[i].SetCreatingRoutePlan(false);
                            if(mapMenu.GetRoutePlan().Count > 0)
                            {
                                int rpid = 0;
                                RoutePlan rp = new RoutePlan(rpid, mapMenu.GetRoutePlan());
                                company.RoutePlans().Add(rp);
                                company.Trucks()[i].RoutePlanId(rpid);
                                company.Trucks()[i].RoutePlan(rp);
                                mapMenu.CleanRoutePlan();
                            }
                            clicks++;
                            break;
                        }
                    }
                    break;
                case "openMapForTruck":
                    for (int i = 0; i < company.Trucks().Count; i++)
                    {
                        if (company.Trucks()[i].Id() == value)
                        {
                            company.Trucks()[i].SetCreatingRoutePlan(true);
                            mapMenu.SetTruckSelected(value);
                            mapMenu.SetOpen(true);
                            clicks++;
                            break;
                        }
                    }
                    break;
                case "getCargoPayment":
                    company.addMoney(value);
                    break;
            }
        }
    }
}
