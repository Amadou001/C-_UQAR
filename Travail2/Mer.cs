using System;

namespace Program
{
    class Mer : Pokemon
    {
        protected int nombreNageoires;

        public Mer(string nom, float poids, int nombreNageoires):base(nom, poids)
        {
            this.nombreNageoires = nombreNageoires;
        }

        public override float Vitesse()
        {
            return (this.poids / 25) * this.nombreNageoires;
        }

        public override void Affiche()
        {
             Console.WriteLine("Je suis le pokemon {0} mon poids est de {1} kg, ma vitesse est de {2}km/h, j'ai {3} nageoires .", this.nom, this.poids, this.Vitesse(), this.nombreNageoires);
        }
    }
}
