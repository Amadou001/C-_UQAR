using System;


namespace Program
{
     abstract class Pokemon
    {
        protected string nom;
        protected float poids;

        public Pokemon(string nom, float poids)
        {
            this.nom = nom;
            this.poids = poids;

        }

        public abstract float Vitesse();
        public abstract void  Affiche();
    }
}
