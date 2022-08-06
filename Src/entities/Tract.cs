using System;
using System.Collections.Generic;
using System.Text;

namespace cargoTycoon
{
    /**
     * Clase que representa un tramo de un plan de ruta
     * Si origen o destino no es el nombre de un pueblo/ciudad, el camion continua con el tramo siguiente sin parar
     */
    class Tract
    {
        private int origin;
        private int destination;
        private int distance;
        private int routeId;
        private int fromRouteUnit;
        private int toRouteUnit;

        public Tract(int origin, int destination, int distance, int routeId, int fromRouteUnit, int toRouteUnit)
        {
            this.origin = origin;
            this.destination = destination;
            this.distance = distance;
            this.routeId = routeId;
            this.fromRouteUnit = fromRouteUnit;
            this.toRouteUnit = toRouteUnit;
        }

        public int Origin()
        {
            return this.origin;
        }

        public int Destination()
        {
            return this.destination;
        }

        public int Distance()
        {
            return this.distance;
        }

        public int RouteId()
        {
            return this.routeId;
        }

        public int FromRouteUnit()
        {
            return this.fromRouteUnit;
        }

         public int ToRouteUnit()
        {
            return this.toRouteUnit;
        }
    }
}
