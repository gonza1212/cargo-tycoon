using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace cargoTycoon
{
    // PARA MOSTRAR EL MAPA DE LA REGION AL HACER CLICK EN CREAR HOJA DE RUTA, PRIMERO TENGO QUE HACER LA PILA O STACK DE MENUS ABIERTOS PARA CERRARLOS Y ENTOCES MOSTRAR EL MAPA
    // UNA SEGUNDA OPCION ES QUE SIMPLEMENTE LO MUESTRE POR ENCIMA PARA QUE CUANDO CIERRE EL MAPA SE VEA EL MENU DEL CAMION CON LA HOJA DE RUTA ASIGNADA Y LAS CARGAS DISPONIBLES
    class GarageState : GameState
    {
        private Company company;
        private GuiButton button00;
        private Texture2D bg;
        private Vector2 pos;
        // no se si esto va aca
        private TruckSeller seller; // esto despues se reemplaza por una lista de concesionarios, aunque tal vez en otros states


        public GarageState(StatesManager sm, Company company)
        {
            this.sm = sm;
            this.company = company;
            this.bg = Globals.content.Load<Texture2D>("bg0");
            this.pos = new Vector2(0, 0);
            this.button00 = new GuiButton(Globals.content.Load<Texture2D>("buttonTruck"), Globals.content.Load<Texture2D>("buttonTruckHover"), 10, 10);
            this.seller = new TruckSeller("Basic - Venta de Camiones", Globals.content.Load<Texture2D>("bgTruckSeller"));
            seller.AddTruck(new Truck("Foord", "0", 12500));
            seller.AddTruck(new Truck("Foord", "1", 15000));
            seller.AddTruck(new Truck("Foord", "2", 17500));
        }

        public override void DebugDraw()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            // basic
            Globals.spriteBatch.Draw(bg, pos, Color.White);
            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "$" + company.Money(), new Vector2(1040, 18), Color.Black);
            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), company.Date().DateToString(), new Vector2(1182, 55), Color.Black);
            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "Camiones: " + company.Trucks().Count, new Vector2(100, 10), Color.Black);
            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "Planes de Ruta: " + company.RoutePlans().Count, new Vector2(200, 10), Color.Black);
            // trucks
            for (int i = 0; i < company.Trucks().Count; i++)
            {
                company.Trucks()[i].Draw();    
            }
            // gui
            button00.Draw();
            // menus
            if (seller.IsShowingStock())
                seller.Draw();
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
            button00.Update();
            // logic
            for (int i = 0; i < company.Trucks().Count; i++)
            {
                company.Trucks()[i].Update();
            }
            // menus
            if (seller.IsShowingStock())
            {
                seller.Update();
                if(seller.IsBought())
                {
                    Truck t = seller.Trucks()[seller.TruckSelected()];
                    seller.Trucks().RemoveAt(seller.TruckSelected());
                    t.Teleport(288, 350);
                    company.SubMoney(t.Price());
                    company.AddTruck(t);
                    seller.Close();
                }
            }
            if (button00.Click())
                seller.SetShowingStock(true);
        }
    }
}
