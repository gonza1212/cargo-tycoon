using System;
using System.Collections.Generic;
using System.Text;

namespace cargoTycoon
{
    class Tract
    {
        private string origin;
        private string destination;
        private int distance;

        public Tract(string origin, string destination, int distance)
        {
            this.origin = origin;
            this.destination = destination;
            this.distance = distance;
        }

        public string Origin()
        {
            return this.origin;
        }

        public string Destination()
        {
            return this.destination;
        }

        public int Distance()
        {
            return this.distance;
        }
    }
}
