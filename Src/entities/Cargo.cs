using System;
using System.Collections.Generic;
using System.Text;

namespace cargoTycoon
{
    class Cargo
    {
        private string description;
        private int weight;
        private int payment;

        public Cargo(string description, int weight, int payment)
        {
            this.description = description;
            this.weight = weight;
            this.payment = payment;
        }

        public string Description()
        {
            return this.description;
        }

        public int Weight()
        {
            return this.weight;
        }

        public int Payment()
        {
            return this.payment;
        }
    }
}
