using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace GestionMagasin
{
    abstract class Utilisateur
    {
        public static List<Utilisateur> list ;
        public string Nom{get; set;}
        public string Prenom{get; set;}
        public string Email{get; set;}
        public int Id {get; set;}

        public Utilisateur(string nom, string prenom, string email)
        {
            list = new List<Utilisateur>();
            FromJson();
            this.Nom = nom;
            this.Prenom = prenom;
            this.Email = email;
            Random random = new Random();
            this.Id = random.Next(100000, 999999); // Générer un id aléatoire
        }
        public Utilisateur()
        {
        }

        public abstract void Seconnecter(string name, string user_type, int id);

        public void ToJson()
        {
            try
            {
                string json = JsonConvert.SerializeObject(
                    list, 
                    Formatting.Indented, 
                    new JsonSerializerSettings 
                    { 
                        TypeNameHandling = TypeNameHandling.All,  // Stores type info for proper deserialization
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore 
                    }
                );

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "JsonFiles", "Utilisateur.json");
                File.WriteAllText(filePath, json);
                //Console.WriteLine($"JSON file written to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        public static void FromJson()
        {
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "JsonFiles", "Utilisateur.json");

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("No JSON file found.");
                    list = new List<Utilisateur>(); // Assure une liste vide si le fichier n'existe pas
                    return;
                }

                string json = File.ReadAllText(filePath);
                var tempList = JsonConvert.DeserializeObject<List<Utilisateur>>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                list = tempList ?? new List<Utilisateur>(); // Si désérialisation retourne null

                //Console.WriteLine($"JSON file read from {filePath}");
            }
            catch (Exception ex)
            {
                list = new List<Utilisateur>(); // fallback en cas d'erreur
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        /*
            * Méthodes virtuelles pour les actions des utilisateurs
            * Ces méthodes peuvent être redéfinies dans les classes dérivées
            * pour fournir des implémentations spécifiques.
            * Ces méthodes ont été définies ici comme virtuelle pour éviter des problèmes de compilation
            * Exemple : 
            * - PasserCommande peut être redéfini dans la classe Client
            * - ValiderCommande peut être redéfini dans la classe Vendeur
        */
        public virtual void PasserCommande(Article article) // Client
        {
            Console.WriteLine("Commande passée");
        }

        public virtual void ValiderCommande(string status, Commande commande) // Vendeur
        {
            Console.WriteLine("Commande validée");
        }

        public virtual void EnregistrerModelBouquet(List<Fleur> fleurs)
        {
            Console.WriteLine("Modèle de bouquet enregistré");
        }

        public virtual void AjouterProduit(Fleur fleur)
        {
            Console.WriteLine("Produit ajouté");
        }

        public static Utilisateur CheckConnection(string user_type, int id)
        {
            Utilisateur utilisateur = GetUtilisateurByID(id);
            if (utilisateur != null)
            {
                if (utilisateur.GetType().Name == user_type)
                {
                    utilisateur.Seconnecter(utilisateur.Prenom, user_type, id);
                    return utilisateur;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Type d'utilisateur incorrect");
                    return null;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Utilisateur non trouvé");
                return null;
            }
        }

        public static Utilisateur GetUtilisateurByID(int id)
        {
            FromJson();
            foreach (Utilisateur utilisateur in list)
            {
                if (utilisateur.Id == id)
                {
                    return utilisateur;
                }
            }
            return null;
        }

       public static bool operator ==(Utilisateur utilisateur1, Utilisateur utilisateur2)
        {
            if (ReferenceEquals(utilisateur1, utilisateur2)) return true;  // Same reference, return true
            if (utilisateur1 is null || utilisateur2 is null) return false; // One is null, return false

            return utilisateur1.Id == utilisateur2.Id;  // Compare IDs
        }

        public static bool operator !=(Utilisateur utilisateur1, Utilisateur utilisateur2)
        {
            return !(utilisateur1 == utilisateur2);  
        }

        public void AjouterUtilisateur(Utilisateur utilisateur)
        {
            Console.Clear();
            if (list == null) 
            {
                list = new List<Utilisateur>(); // Ensure list is initialized
            }

            list.Add(utilisateur);
            ToJson();  // Save the updated list to the JSON file after adding the utilisateur
        }

        public void SupprimerUtilisateur(Utilisateur utilisateur)
        {
            Console.Clear();
            if (list != null && list.Contains(utilisateur))
            {
                list.Remove(utilisateur);
                ToJson();  // Save the updated list to the JSON file after removing the utilisateur
            }
            else
            {
                Console.WriteLine("Utilisateur non trouvé dans la liste.");
            }

        }


        /*
            * Méthodes pour créer L'administrateur ou le propriétaire
            * cette méthode ne sera utilisée qu'une seule fois selon certaines conditions
        */
        public void CreerAdministrateur()
        {
            Console.Clear();
            FromJson();
            if (list.Count > 0) // L'administrateur doit être le premier utilisateur
            {
                Console.WriteLine("\nUn administrateur existe déjà");
                return;
            }
            Console.WriteLine("Premier utilisateur\n");
            Console.WriteLine("Création d'un administrateur ou propriétaire");
            Console.Write("Nom:  ");
            string nom = Console.ReadLine();
            Console.Write("Prénom:  ");
            string prenom = Console.ReadLine();
            Console.Write("Email:  ");
            string email = Console.ReadLine();
            Utilisateur administrateur = new Proprietaire(nom, prenom, email);
            administrateur.AjouterUtilisateur(administrateur);

            Console.WriteLine("\nVous avez créé un administrateur");
            Console.WriteLine("Nom:  " + administrateur.Nom + "\nPrénom:  " + administrateur.Prenom + "\nEmail:  " + administrateur.Email + "\nID:  " + administrateur.Id);
        }
}
}
