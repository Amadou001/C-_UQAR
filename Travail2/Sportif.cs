using System;


namespace Program
{
    class Sportif: Terre
    {
        private float frequenceCardiaque;

        public Sportif(string nom, float poids, int nombrePattes, float taille, float frequence):base(nom, poids, nombrePattes, taille)
        {
            this.frequenceCardiaque = frequence;
        }

        public override void Affiche()
        {
            Console.WriteLine("Je suis le pokemon {0} mon poids est de {1} kg, ma vitesse est de {2}km/h, j'ai {3} pattes, ma taille est de {4} m, ma fréquence cardiaque est de {5} pulsations à la minutes .", this.nom, this.poids, this.Vitesse(), this.nombrePattes, this.taille, this.frequenceCardiaque);
        }
    }
}
