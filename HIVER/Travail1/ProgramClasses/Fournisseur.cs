using System;
using System.Collections.Generic;


namespace GestionMagasin
{
    class Fournisseur : Utilisateur
    {
        public Fournisseur(string nom, string prenom, string email) : base(nom, prenom, email)
        {
        }
        public Fournisseur()
        {
        }
        public override void Seconnecter(string name, string user_type, int id)
        {
            Console.Clear();
            Console.WriteLine($"\n🚚 Bonjour {name} {user_type}!\nVous êtes connecté avec l'ID {id}.\nMerci d'assurer l’approvisionnement du magasin.");
        }


        public override void AjouterProduit(Fleur fleur)
        {
            Console.Clear();
            fleur.AjouterFleur(fleur);
            Console.WriteLine("Vous avez ajouté une, fleur avec le nom:  " + fleur.Nom + " et le prix: " + fleur.Prix, " et la quantité: " + fleur.Quantite);
        }
    }
}
