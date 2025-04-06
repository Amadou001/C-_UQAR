using System;
using System.Collections.Generic;

namespace GestionMagasin
{
    class GestionMenus
    {
        public void MenuPrincipal()
        {
            Console.WriteLine("\n1. Se connecter");
            Console.WriteLine("2. S'inscrire");
            Console.WriteLine("3. Quitter\n");
            Console.Write("Veuillez choisir une option:  ");
            int choix = Convert.ToInt32(Console.ReadLine());
            switch (choix)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("\n1. Client");
                    Console.WriteLine("2. Proprietaire (ou Administrateur)");
                    Console.WriteLine("3. Vendeur");
                    Console.WriteLine("4. Fournisseur\n");
                    Console.Write("Veuillez choisir une option:  ");
                    int choix2 = Convert.ToInt32(Console.ReadLine());
                    switch (choix2)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("\nEntrez votre id: ");
                            int idClient = Convert.ToInt32(Console.ReadLine());
                            Utilisateur client = Utilisateur.CheckConnection("Client", idClient);
                            if (client != null)
                            {
                                MenuClient(client);
                            }
                            else
                            {

                                MenuPrincipal();
                            }
                
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("\nEntrez votre id: ");
                            int idProprietaire = Convert.ToInt32(Console.ReadLine());
                            Utilisateur proprietaire = Utilisateur.CheckConnection("Proprietaire", idProprietaire);
                            if (proprietaire != null)
                            {
                                MenuProprietaire(proprietaire);
                            }
                            else
                            {
                                MenuPrincipal();
                            }

                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("\nEntrez votre id: ");
                            int idVendeur = Convert.ToInt32(Console.ReadLine());
                            Utilisateur vendeur = Utilisateur.CheckConnection("Vendeur", idVendeur);
                            if (vendeur != null)
                            {
                                MenuVendeur(vendeur);
                            }
                            else
                            {  
                                MenuPrincipal();
                            }
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("\nEntrez votre id: ");
                            int idFournisseur = Convert.ToInt32(Console.ReadLine());
                            Utilisateur fournisseur = Utilisateur.CheckConnection("Fournisseur", idFournisseur);
                            if (fournisseur != null)
                            {
                                MenuFournisseur(fournisseur);
                            }
                            else
                            {
                                MenuPrincipal();
                            }
                            break;
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("\nL'inscription par soit même n'est disponible que pour les clients");
                    Console.WriteLine("Entrez votre nom: ");
                    string nom = Console.ReadLine();
                    Console.WriteLine("Entrez votre prénom: ");
                    string prenom = Console.ReadLine();
                    Console.WriteLine("Entrez votre email: ");
                    string email = Console.ReadLine();
                    Client client5 = new Client(nom, prenom, email);
                    client5.AjouterUtilisateur(client5);
                    Console.WriteLine("\nVous êtes inscrit en tant que client avec l'id " + client5.Id);

                    MenuPrincipal();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }

        public  void MenuClient(Utilisateur client)
        {
            Commande commande = Commande.FromJson();
            Console.WriteLine("\n1. Passer une commande");
            Console.WriteLine("2. Se déconnecter");
            if (commande != null && commande.Client == client)
            {
                Console.WriteLine("3. Suivre une commande\n");
            }
            Console.Write("Veuillez choisir une option:  ");
            int choix = Convert.ToInt32(Console.ReadLine());
            switch (choix)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("\nVoulez-vous une fleur ou un bouquet?");
                    Console.WriteLine("1. Fleur");
                    Console.WriteLine("2. Bouquet\n");
                    Console.Write("Veuillez choisir une option:  ");
                    int choix2 = Convert.ToInt32(Console.ReadLine());
                    if (choix2 == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("\nFleurs disponibles: \n");
                        Fleur.AfficherFleurs();
                        Fleur fleur1 = new Fleur();
                        Console.WriteLine("\nEntrez le nom de la fleur: ");
                        string nomFleur = Console.ReadLine();
                        Fleur fleur = Fleur.GetFleurByName(nomFleur);
                        if (fleur != null)
                        { 
                            client.PasserCommande(fleur);
                            MenuClient(client);
                        }
                        else
                        {
                            Console.WriteLine("\nFleur inexistante");
                            MenuClient(client);
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n1- Customiser un bouquet");
                        Console.WriteLine("2- Choisir un modèle de bouquet");
                        Console.Write("\nVeuillez choisir une option:  ");
                        int choix3 = Convert.ToInt32(Console.ReadLine());
                        if (choix3 == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("\nFleurs disponibles: \n");
                            Fleur.AfficherFleurs();
                            List<Fleur> listFleurs = new List<Fleur>();
                            while (true)
                            {
                                Console.Write("\nEntrez le nom de la fleur:  ");
                                string nomFleur = Console.ReadLine();
                                Fleur fleur = Fleur.GetFleurByName(nomFleur);
                                if (fleur != null)
                                {
                                    listFleurs.Add(fleur);
                                }
                                else
                                {
                                    Console.WriteLine("\nFleur inexistante\n");
                                }
                                Console.WriteLine("Voulez-vous ajouter une autre fleur? (O/N)");
                                string reponse = Console.ReadLine();
                                if (reponse == "N")
                                {
                                    break;
                                }
                            }
                            Bouquet bouquet = new Bouquet(listFleurs);
                            client.PasserCommande(bouquet); // Passer la commande pour le bouquet
                            MenuClient(client);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Modèles de bouquets disponibles: \n");
                            Bouquet.AfficherBouquets();
                            Console.WriteLine("Entrez l'id du bouquet: ");
                            int idBouquet = Convert.ToInt32(Console.ReadLine());
                            Bouquet bouquet = Bouquet.GetBouquetByModel(idBouquet);
                            if (bouquet != null)
                            {
                                client.PasserCommande(bouquet);
                                MenuClient(client);
                            }
                            else
                            {
                                Console.WriteLine("Article inexistant");
                                MenuClient(client);
                            }
                        }
                    }
                    break;
                case 2:
                    MenuPrincipal();
                    break;

                case 3:
                    commande = Commande.FromJson();
                    commande.AfficherCommande();
                    MenuClient(client);
                    break;
            }
        }

        public  void MenuProprietaire(Utilisateur proprietaire)
        {
            Console.WriteLine("\n1. Ajouter un vendeur");
            Console.WriteLine("2. Supprimer un vendeur");
            Console.WriteLine("3. Ajouter un fournisseur");
            Console.WriteLine("4. Supprimer un fournisseur");
            Console.WriteLine("5. Se déconnecter");
            Console.Write("\nVeuillez choisir une option:  ");
            int choix = Convert.ToInt32(Console.ReadLine());
            switch (choix)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Entrez le nom du vendeur: ");
                    string nom = Console.ReadLine();
                    Console.WriteLine("Entrez le prénom du vendeur: ");
                    string prenom = Console.ReadLine();
                    Console.WriteLine("Entrez l'email du vendeur: ");
                    string email = Console.ReadLine();
                    Vendeur vendeur = new Vendeur(nom, prenom, email);
                    vendeur.AjouterUtilisateur(vendeur);
                    Console.WriteLine($"Vous avez ajouté le vendeur {vendeur.Prenom} avec l'id " + vendeur.Id);
                    MenuProprietaire(proprietaire);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Entrez l'id du vendeur à supprimer: ");
                    int idVendeur = Convert.ToInt32(Console.ReadLine());
                    Utilisateur vendeurToDelete = Vendeur.GetUtilisateurByID(idVendeur);
                    if (vendeurToDelete != null)
                    {
                        proprietaire.SupprimerUtilisateur(vendeurToDelete);

                        Console.WriteLine($"Vous avez supprimé le vendeur {vendeurToDelete.Nom} {vendeurToDelete.Prenom} avec l'id " + vendeurToDelete.Id);
                    }
                    else
                    {
                        Console.WriteLine("Vendeur inexistant\n");
                    }
                    MenuProprietaire(proprietaire);
                    break; 
                case 3:
                    Console.Clear();
                    Console.WriteLine("Entrez le nom du fournisseur: ");
                    string nomFournisseur = Console.ReadLine();
                    Console.WriteLine("Entrez le prénom du fournisseur: ");
                    string prenomFournisseur = Console.ReadLine();
                    Console.WriteLine("Entrez l'email du fournisseur: ");
                    string emailFournisseur = Console.ReadLine();
                    Fournisseur fournisseur = new Fournisseur(nomFournisseur, prenomFournisseur, emailFournisseur);
                    fournisseur.AjouterUtilisateur(fournisseur);
                    Console.WriteLine($"Vous avez ajouté le fournisseur {fournisseur.Prenom} avec l'id " + fournisseur.Id);
                    MenuProprietaire(proprietaire);
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Entrez l'id du fournisseur à supprimer: ");
                    int idFournisseur = Convert.ToInt32(Console.ReadLine());
                    Utilisateur fournisseurToDelete = Fournisseur.GetUtilisateurByID(idFournisseur);
                    if (fournisseurToDelete != null)
                    {
                        proprietaire.SupprimerUtilisateur(fournisseurToDelete);
                        Console.WriteLine($"Vous avez supprimé le fournisseur {fournisseurToDelete.Nom} {fournisseurToDelete.Prenom} avec l'id " + fournisseurToDelete.Id);
                        MenuProprietaire(proprietaire);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Fournisseur inexistant");
                        MenuProprietaire(proprietaire);
                        break;
                    }
                case 5:
                   MenuPrincipal();
                   break;
            }
        }

        public void MenuVendeur(Utilisateur vendeur)
        {
            Console.WriteLine("\n1. Inscrire un client");
            Console.WriteLine("2. Supprimer un client");
            Console.WriteLine("3. Valider une commande");
            Console.WriteLine("4. Enregistrer un modèle de bouquet");
            Console.WriteLine("5. Se déconnecter\n");
            Console.Write("\nVeuillez choisir une option:  ");
            int choix = Convert.ToInt32(Console.ReadLine());
            switch (choix)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Entrez votre nom: ");
                    string nom = Console.ReadLine();
                    Console.WriteLine("Entrez votre prénom: ");
                    string prenom = Console.ReadLine();
                    Console.WriteLine("Entrez votre email: ");
                    string email = Console.ReadLine();
                    Client client5 = new Client(nom, prenom, email);
                    client5.AjouterUtilisateur(client5);
                    Console.WriteLine($"Vous avez inscrit le client {client5.Nom} avec l'id: " + client5.Id);
                    MenuVendeur(vendeur);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Entrez l'id du client à supprimer: ");
                    int idClient = Convert.ToInt32(Console.ReadLine());
                    Utilisateur client = Client.GetUtilisateurByID(idClient);
                    if (client != null)
                    {
                        vendeur.SupprimerUtilisateur(client);
                        Console.WriteLine("Vous avez supprimé le client " + client.Nom + " " + client.Prenom + " avec l'id: " + client.Id);
                    }
                    else
                    {
                        Console.WriteLine("Client inexistant");
                    }
                    MenuVendeur(vendeur);
                    break;
                case 3:
                    Commande commande = Commande.FromJson();
                    if (commande == null)
                    {
                        Console.WriteLine("Aucune commande trouvée.");
                        MenuVendeur(vendeur);
                        break;
                    }
                    commande.AfficherCommandePourVendeur();
                    Console.WriteLine("\n1- Valider la commande");
                    Console.WriteLine("2- Annuler la commande");
                    Console.Write("\nVeuillez choisir une option:  ");
                    int choix2 = Convert.ToInt32(Console.ReadLine());
                    if (choix2 == 1)
                    {
                        vendeur.ValiderCommande("Validée", commande);
                        //Console.WriteLine("Commande validée");
                    }
                    else
                    {
                        vendeur.ValiderCommande("Annulée", commande);
                        //Console.WriteLine("Commande annulée");
                    }
                    MenuVendeur(vendeur);
                    break;
                case 4:
                    Console.Clear();
                    Fleur.AfficherFleurs();
                    List<Fleur> listFleurs = new List<Fleur>();
                    while (true)
                    {
                        Console.Write("\nEntrez le nom de la fleur:  ");
                        string nomFleur = Console.ReadLine();
                        Fleur fleur = Fleur.GetFleurByName(nomFleur);
                        if (fleur != null)
                        {
                            listFleurs.Add(fleur);
                        }
                        else
                        {
                            Console.WriteLine("\nFleur inexistante\n");
                        }
                        Console.WriteLine("Voulez-vous ajouter une autre fleur? (O/N)");
                        string reponse = Console.ReadLine();
                        if (reponse == "N")
                        {
                            break;
                        }
                    }
                    vendeur.EnregistrerModelBouquet(listFleurs);
                    MenuVendeur(vendeur);
                    break;
                case 5:
                    MenuPrincipal();
                    break;
            }
        }

        public  void MenuFournisseur(Utilisateur fournisseur)
        {
            Console.WriteLine("\n1. Ajouter un produit");
            Console.WriteLine("2. Se déconnecter");
            Console.Write("\nVeuillez choisir une option:  ");
            int choix = Convert.ToInt32(Console.ReadLine());
            switch (choix)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Entrez le nom de la fleur: ");
                    string nom = Console.ReadLine();
                    Console.WriteLine("Entrez le prix de la fleur: ");
                    double prix = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Entrez la quantité de la fleur: ");
                    int quantite = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Entrez la couleur de la fleur: ");
                    string couleur = Console.ReadLine();
                    Console.WriteLine("Entrez la description de la fleur: ");
                    string description = Console.ReadLine();
                    Fleur fleur = new Fleur(nom, couleur, description, prix, quantite);
                    fournisseur.AjouterProduit(fleur); // Ajouter la fleur
                    MenuFournisseur(fournisseur);
                    break;
                case 2:
                    Console.Clear();
                    MenuPrincipal();
                    break;
            }
        }

        public  void GestionFlux()
        {
            GestionMenus gestionMenus = new GestionMenus();
            Console.WriteLine("\n___________________Bienvenue dans notre magasin___________________\n");
            gestionMenus.MenuPrincipal();
        }
    }
}
