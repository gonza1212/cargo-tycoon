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
        private string brand; // marca
        private string model;
        private int price;
        // datos necesarios cuando pertenece al jugador
        private string issue;
        private int routePlan;
        // graphics
        private Vector2 pos;
        private Texture2D bgForSeller;
        private Texture2D sprite; // sprite para garage
        private GuiButton alert;
        private bool menuOpen;
        // menu
        private GuiButton createRoutePlan;

        public Truck(string brand, string model, int price)
        {
            this.brand = brand;
            this.model = model;
            this.price = price;
            this.routePlan = -1;
            this.issue = "sin problemas";
            this.alert = new GuiButton(Globals.content.Load<Texture2D>("buttonExclamation"), Globals.content.Load<Texture2D>("buttonExclamationHover"), 10, 10);
            this.menuOpen = false;
            this.pos = new Vector2(0, 0);
            this.createRoutePlan = new GuiButton(Globals.content.Load<Texture2D>("buttonCreateRoutePlan"), Globals.content.Load<Texture2D>("buttonCreateRoutePlanHover"), 10, 10);
            switch (model)
            {
                case "0":
                    bgForSeller = Globals.content.Load<Texture2D>("truck00");
                    sprite = Globals.content.Load<Texture2D>("spriteTruck00");
                    break;
                case "1":
                    bgForSeller = Globals.content.Load<Texture2D>("truck01");
                    sprite = Globals.content.Load<Texture2D>("spriteTruck01");
                    break;
                case "2":
                    bgForSeller = Globals.content.Load<Texture2D>("truck02");
                    sprite = Globals.content.Load<Texture2D>("spriteTruck02");
                    break;
            }
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(sprite, pos, Color.White);
            if (HasIssue() && !IsMenuOpen())
            {
                alert.Teleport((int)pos.X + 55, (int)pos.Y);
                alert.Draw();
            }
            if (IsMenuOpen())
            {
                Globals.spriteBatch.Draw(Globals.content.Load<Texture2D>("bgTruckMenu"), pos, Color.White);
                Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "Marca: " + brand, new Vector2(pos.X + 15, pos.Y + 20), Color.Black);
                Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "Modelo: " + model, new Vector2(pos.X + 15, pos.Y + 40), Color.Black);
                Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), issue, new Vector2(pos.X + 15, pos.Y + 60), Color.Red);
                createRoutePlan.Draw();
            }
        }

        public void Update()
        {
            if (routePlan < 0)
                issue = "* No hay Plan de Ruta asignado *";
            else
                issue = "sin problemas";
            if (Alert().Click())
                MenuOpen(true);
            if (IsMenuOpen())
            {
                createRoutePlan.Update();
            }
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
            createRoutePlan.Teleport(x + 100, y + 150);
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
            return !this.issue.Contains("sin problemas");
        }

        public int RoutePlan()
        {
            return this.routePlan;
        }

        public void RoutePlan(int rp)
        {
            this.routePlan = rp;
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
    }
}
