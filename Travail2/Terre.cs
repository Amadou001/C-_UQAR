using System;

namespace Program
{
    abstract class Terre : Pokemon
    {
        protected int nombrePattes;
        protected float taille;

        public Terre(string nom, float poids, int nombrePattes, float taille):base(nom, poids)
        {
            this.nombrePattes = nombrePattes;
            this.taille = taille;
        }

        public override float Vitesse()
        {
            return this.nombrePattes * this.taille * 3;
        }

    }
}
