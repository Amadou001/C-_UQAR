using System;

namespace Program
{
    class Casanier: Terre
    {
        private int heuresTel;

        public Casanier(string nom, float poids, int nombrePattes, float taille, int heuresTel):base(nom, poids, nombrePattes, taille)
        {
            this.heuresTel = heuresTel;
        }

          public override void Affiche()
        {
            Console.WriteLine("Je suis le pokemon {0} mon poids est de {1} kg, ma vitesse est de {2}km/h, j'ai {3} pattes, ma taille est de {4} m et je regarde la télé {5}h par jour. ", this.nom, this.poids, this.Vitesse(), this.nombrePattes, this.taille, this.heuresTel);
        }
    }
}
