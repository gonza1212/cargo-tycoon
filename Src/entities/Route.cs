using System;
using System.Collections.Generic;
using System.Text;

namespace cargoTycoon
{
    class Route
    {
        private string name;
        private int distance;
        private List<RouteUnit> units;

        public Route(string name, int distance)
        {
            this.name = name;
            this.distance = distance;
            this.units = new List<RouteUnit>();
        }

        public string Name()
        {
            return this.name;
        }

        public int Distance()
        {
            return this.distance;
        }

        public void AddRouteUnit(RouteUnit unit)
        {
            this.units.Add(unit);
        }

        public int LastRouteUnitIndex()
        {
            return units.Count - 1;
        }

        public List<RouteUnit> Units()
        {
            return this.units;
        }
    }
}
