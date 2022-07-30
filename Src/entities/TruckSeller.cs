using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace cargoTycoon
{
    class TruckSeller
    {
        private string name;
        private List<Truck> trucks;
        // graphics
        private List<GuiButton> truckButtons;
        private GuiButton cancel;
        private GuiButton close;
        private GuiButton buy;
        private Vector2 pos;
        private Texture2D bg;
        private bool showStock;
        private int truckSelected;
        private bool bought;

        public TruckSeller(string name, Texture2D bg)
        {
            this.name = name;
            this.trucks = new List<Truck>();
            this.truckButtons = new List<GuiButton>();
            // graphics
            this.bg = bg;
            this.showStock = false;
            this.bought = false;
            this.pos = new Vector2(160, 90);
            this.truckSelected = -1;
            this.close = new GuiButton(Globals.content.Load<Texture2D>("buttonCancel"), Globals.content.Load<Texture2D>("buttonCancelHover"), (int) pos.X + bg.Width - 58, (int) pos.Y + 10);
            this.cancel = new GuiButton(Globals.content.Load<Texture2D>("buttonCancel"), Globals.content.Load<Texture2D>("buttonCancelHover"), (int)pos.X + 452, (int)pos.Y + 10);
            this.buy = new GuiButton(Globals.content.Load<Texture2D>("buttonBuy"), Globals.content.Load<Texture2D>("buttonBuyHover"), (int)pos.X + 200, (int)pos.Y + 380);
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(bg, pos, Color.White);
            Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), name, new Vector2(pos.X + 25, pos.Y + 50), Color.Orange);
            close.Draw();
            int truckPos = 25;
            for(int i = 0; i < trucks.Count; i++)
            {
                truckButtons[i].Draw();
                Globals.spriteBatch.Draw(trucks[i].BgForSeller(), new Vector2(pos.X + truckPos, pos.Y + 65), Color.White);
                Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "Marca: " + trucks[i].Brand(), new Vector2(pos.X + truckPos + 25, pos.Y + 420), Color.Black);
                Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "Modelo: " + trucks[i].Model(), new Vector2(pos.X + truckPos + 25, pos.Y + 440), Color.Black);
                Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "Precio: $" + trucks[i].Price(), new Vector2(pos.X + truckPos + 25, pos.Y + 460), Color.Black);
                truckPos += 315;
            }
            if(truckSelected >= 0)
            {
                Globals.spriteBatch.Draw(Globals.content.Load<Texture2D>("bgTruckSelected"), pos, Color.White);
                cancel.Draw();
                buy.Draw();
                Globals.spriteBatch.Draw(trucks[truckSelected].BgForSeller(), new Vector2(pos.X + 10, pos.Y + 10), Color.White);
                Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "Camion seleccionado: " + truckSelected, new Vector2(pos.X + 320, pos.Y + 80), Color.Black);
                Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "Marca: " + trucks[truckSelected].Brand(), new Vector2(pos.X + 320, pos.Y + 100), Color.Black);
                Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "Modelo: " + trucks[truckSelected].Model(), new Vector2(pos.X + 320, pos.Y + 120), Color.Black);
                Globals.spriteBatch.DrawString(Globals.content.Load<SpriteFont>("File"), "Precio: $ " + trucks[truckSelected].Price(), new Vector2(pos.X + 320, pos.Y + 140), Color.Black);
            }
        }

        public void Update()
        {
            for (int i = 0; i < truckButtons.Count; i++)
            {
                if(truckButtons[i].Click() && truckSelected < 0)
                {
                    truckSelected = i;
                    return;
                }
            }
            if(truckSelected < 0)
            {
                close.Update();
                if (close.Click())
                    Close();
            }
            if (truckSelected >= 0)
            {
                cancel.Update();
                buy.Update();
                if(cancel.Click())
                    this.truckSelected = -1;
                if (buy.Click())
                    this.bought = true;
            }
        }

        public void Close()
        {
            this.truckSelected = -1;
            this.showStock = false;
            this.bought = false;
        }

        public string Name()
        {
            return this.name;
        }

        public List<Truck> Trucks()
        {
            return this.trucks;
        }

        public void AddTruck(Truck t)
        {
            this.trucks.Add(t);
            this.truckButtons.Add(new GuiButton(Globals.content.Load<Texture2D>("bgTruckSelling"), (int)pos.X + 20 + truckButtons.Count * 315, (int)pos.Y + 60));
        }

        public void SetShowingStock(bool draw)
        {
            this.showStock = draw;
        }

        public bool IsShowingStock()
        {
            return this.showStock;
        }

        public Texture2D Bg()
        {
            return this.bg;
        }

        public Vector2 Pos()
        {
            return this.pos;
        }

        public bool IsBought()
        {
            return this.bought;
        }

        public int TruckSelected()
        {
            return this.truckSelected;
        }
    }
}
