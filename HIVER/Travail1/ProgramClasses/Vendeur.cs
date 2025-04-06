using System;
using System.Collections.Generic;


namespace GestionMagasin
{
    class Vendeur : Utilisateur
    {
        public Vendeur(string nom, string prenom, string email) : base(nom, prenom, email)
        {
        }
        public Vendeur()
        {
        }
        public override void Seconnecter(string name, string user_type, int id)
        {
            Console.Clear();
            Console.WriteLine($"\n🧑‍💼 Bonjour {name} {user_type}!\nVous êtes connecté avec l'ID {id}. Préparez-vous à gérer vos commandes.");
        }


        public override void ValiderCommande(string status, Commande commande)
        {
            if (commande != null)
            {
                commande.ValiderCommande(this, status);   
            }
        }

        public override void EnregistrerModelBouquet(List<Fleur> fleurs)
        {
            Console.Clear();
            Bouquet bouquet = new Bouquet(fleurs);
            bouquet.AjouterBouquet(bouquet);
            bouquet.AfficherUnBouquet();
            Console.WriteLine("Vous avez enregistré un modèle de bouquet.");

        }
}
}