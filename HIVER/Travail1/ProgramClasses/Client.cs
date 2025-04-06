using System;
using System.Collections.Generic;


namespace GestionMagasin
{
    class Client : Utilisateur
    {
       
        public Client(string nom, string prenom, string email) : base(nom, prenom, email)
        {
        }
        public Client()
        {
        }
        public override void Seconnecter(string name, string user_type, int id)
        {
            Console.Clear();
            Console.WriteLine($"\n🌸 Bienvenue cher(e) client {name}!\nVous êtes connecté en tant que {user_type} avec l'ID {id}.\nProfitez de notre sélection de fleurs !");
        }


        public override void PasserCommande(Article article)
        {
            Console.Clear();
            string typePaiement = "";
            while(true)
            {
                Console.WriteLine("\nSélectionnez le mode de paiement :");
                Console.WriteLine("\n1. Payer par carte débit");
                Console.WriteLine("2. Payer par carte crédit");
                Console.WriteLine("3. Payer par cash");
                Console.Write("\nVotre choix : ");
                int choix = int.Parse(Console.ReadLine());
                if (choix == 1)
                {
                    typePaiement = "carte débit";
                    break;
                }
                else if (choix == 2)
                {
                    typePaiement = "carte crédit";
                    break;

                }
                else if (choix == 3)
                {
                    typePaiement = "cash";
                    break;
                }
                else
                {
                    Console.WriteLine("Choix invalide");
                    return;
                }
            }
            Console.WriteLine("Vous avez passé une commande! Veillez attendre qu'elle soit validée par un vendeur. :)");
            Commande commande = new Commande(this, article, typePaiement);

        }

    }
}