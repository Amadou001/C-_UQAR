using System;
using System.Collections.Generic;


namespace GestionMagasin
{
    class Proprietaire : Utilisateur
    {
        public Proprietaire(string nom, string prenom, string email) : base(nom, prenom, email)
        {
        }
        public Proprietaire()
        {
        }
        public override void Seconnecter(string name, string user_type, int id)
        {
            Console.Clear();
            Console.WriteLine($"\n🔐 Accès administrateur : {name}\nRôle : {user_type} | ID : {id}\nBienvenue dans le panneau de gestion.");
        }


        public void AjouterVendeur()
        {
            Console.WriteLine("Vous avez ajouté un vendeur");
        }

        public void SupprimerVendeur()
        {
            Console.WriteLine("Vous avez supprimé un vendeur");
        }

        public void AjouterFournisseur()
        {
            Console.WriteLine("Vous avez ajouté un fournisseur");
        }

        public void SupprimerFournisseur()
        {
            Console.WriteLine("Vous avez supprimé un fournisseur");
        }
    }
}