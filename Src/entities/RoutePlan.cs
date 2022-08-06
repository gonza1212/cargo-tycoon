using System;
using System.Collections.Generic;
using System.Text;

namespace cargoTycoon
{
    class RoutePlan
    {
        private int id;
        private List<Tract> tracts;

        public RoutePlan(int id, List<Tract> tracts)
        {
            this.id = id;
            this.tracts = new List<Tract>();
            for (int i = 0; i < tracts.Count; i++)
            {
                this.tracts.Add(tracts[i]);
            }
        }

        public void AddTract(Tract t)
        {
            tracts.Add(t);
        }

        public List<Tract> Tracts()
        {
            return tracts;
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
