using System;
using System.Collections.Generic;
using System.Text;

namespace cargoTycoon
{
    class RoutePlan
    {
        private List<Tract> tracts;

        public RoutePlan()
        {
            this.tracts = new List<Tract>();
        }

        public void AddTract(Tract t)
        {
            tracts.Add(t);
        }

        public List<Tract> Tracts()
        {
            return tracts;
        }

        public string Origin()
        {
            if (tracts.Count > 0)
                return tracts[0].Origin();
            else
                return "Plan de ruta vacío";
        }

        public string Destination()
        {
            if (tracts.Count > 0)
                return tracts[tracts.Count - 1].Destination();
            else
                return "Plan de ruta vacío";
        }

        public int TotalDistance()
        {
            int total = 0;
            for (int i = 0; i < tracts.Count; i++)
            {
                total += tracts[i].Distance();
            }
            return total;
        }
    }
}
