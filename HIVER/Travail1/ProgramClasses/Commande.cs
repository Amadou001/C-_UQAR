using System;
using System.IO;
using Newtonsoft.Json;


namespace GestionMagasin
{
    class Commande
    {
        public Client Client { get; set; }
        public Utilisateur Vendeur { get; set; }
        public Article Article { get; set; }

        public string TypePaiement { get; set; }
        public string Status { get; set; }

        public Commande(Client client, Article article, string typePaiement)
        {
            this.TypePaiement = typePaiement;
            this.Client = client;
            this.Article = article;
            this.Status = "en attente";
            this.Tojson();
        }

        public  void ValiderCommande(Utilisateur vendeur, string status)
        {
            Console.Clear();
            this.Vendeur = vendeur;
            if (status == "Validée")
            {
                this.Status = status;
                Console.WriteLine("La commande a été validée par le vendeur " + Vendeur.Nom + " " + Vendeur.Prenom);
                if (this.Article is Fleur fleur)
                {
                    Fleur fleurAchete = fleur.DeleteFleurByName(((Fleur)this.Article).Nom); // Supprime la fleur du stock
                    Console.WriteLine("Fleur retirée du stock");
                    Facture facture = new Facture(this);
                    facture.GenererFacture();
                    Console.WriteLine("\nLa Facture a été générée avec succès dans le dossier Factures\n");
                    fleurAchete.AfficherUneFleur();
                    
                }
                else
                {
                    Fleur.FromJson();
                    Fleur fleurInBouquet = new Fleur();
                    fleurInBouquet.DeleteFleursListByName(((Bouquet)this.Article).listFleurs); // Supprime les fleurs du stock
                    Facture facture = new Facture(this);
                    facture.GenererFacture();
                    Console.WriteLine("\nLa Facture a été générée avec succès dans le dossier Factures\n");

                }
                this.Tojson();
            }
            else
            {
                this.Status = status;
                Console.WriteLine("La commande a été refusée par le vendeur " + Vendeur.Nom + " " + Vendeur.Prenom);
                this.Tojson();
            }
            
        }

        public double CalculerPrixTotal()
        {
            return Article.Prix;
        }

        public void Tojson()
        {

            string file_name = Path.Combine(Directory.GetCurrentDirectory(), "JsonFiles", "Commande.json");
            string json = JsonConvert.SerializeObject(this, 
                Formatting.Indented,
                new JsonSerializerSettings 
                { 
                    TypeNameHandling = TypeNameHandling.All,  // Stores type info for proper deserialization
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore 
                }
                
                );
            File.WriteAllText(file_name, json);
            //Console.WriteLine($"JSON file written to {file_name}");

        }

        public static Commande FromJson()
        {
            string file_name = Path.Combine(Directory.GetCurrentDirectory(), "JsonFiles", "Commande.json");
            string json = File.ReadAllText(file_name);
            Commande commande = JsonConvert.DeserializeObject<Commande>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All  // Ensures derived types are correctly instantiated
                });
            //Console.WriteLine($"JSON file read from {file_name}");
            return commande;
        }

        public void AfficherCommande() // Pour le client
        {
            Console.Clear();
            Console.WriteLine($"Client: {this.Client.Nom} {this.Client.Prenom}");
            if (this.Article is Fleur)
            {
                Fleur fleur = (Fleur)this.Article;
                Console.WriteLine($"Article: {fleur.Nom}");
            }
            Console.WriteLine($"Méthode de paiement: {this.TypePaiement}");
            Console.WriteLine($"Prix: {this.Article.Prix:C2}");
            Console.WriteLine($"Status: {this.Status}");
        }

        public void AfficherCommandePourVendeur() // Pour le vendeur
        {
            Console.Clear();
            Console.WriteLine($"Commande effectuée par: {this.Client.Nom} {this.Client.Prenom}\n");
            if (this.Article is Fleur)
            {
                Fleur fleur = (Fleur)this.Article;
                Console.WriteLine($"Article: {fleur.Nom}");
            }
            else
            {
                Console.WriteLine($"Article: bouquet modèle {((Bouquet)this.Article).Model}");
                Console.WriteLine($"Fleurs: ");
                foreach (Fleur fleur in ((Bouquet)this.Article).listFleurs)
                {
                    Console.WriteLine($"- {fleur.Nom}");
                }
            }
            Console.WriteLine($"Prix: {this.Article.Prix:C2}");
            Console.WriteLine($"Status: {this.Status}");
        }
    }
}