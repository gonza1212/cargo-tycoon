using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace cargoTycoon
{
    class Company
    {
        private GameDate date;
        private int money;
        private List<Truck> trucks;
        private List<RoutePlan> routePlans;

        public Company()
        {
            this.date = new GameDate();
            this.money = 20000;
            this.trucks = new List<Truck>();
            this.routePlans = new List<RoutePlan>();
        }

        public int Money()
        {
            return this.money;
        }

        public void SubMoney(int amount)
        {
            this.money -= amount;
        }

        public void addMoney(int amount)
        {
            this.money += amount;
        }

        public void AddTruck(Truck t)
        {
            this.trucks.Add(t);
        }

        public List<Truck> Trucks()
        {
            return this.trucks;
        }

        public GameDate Date()
        {
            return this.date;
        }

        public List<RoutePlan> RoutePlans()
        {
            return this.routePlans;
        }
    }
}
