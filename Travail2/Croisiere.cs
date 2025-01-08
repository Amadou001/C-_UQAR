using System;

namespace Program
{
    class Croisiere: Mer
    {
        public Croisiere(string nom, float poids, int nombreNageoires): base(nom, poids, nombreNageoires)
        {

        }

        public override float Vitesse()
        {
            return ((this.poids/25) * this.nombreNageoires) / 2;
        }
    }
}