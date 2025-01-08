using System;
using System.Collections.Generic;
using System.Linq;

namespace Structure
{
    public class Jeu
{
    private Banque banque; 
    private List<Joueur> joueurs; 
    private List<Carte> pile;
    private int joueur_index;
    private bool est_renverse; 
    public Jeu(List<string> nom_joueurs)
    {
        banque = new Banque();
        joueurs = new List<Joueur>();
        pile = new List<Carte>();
        joueur_index = 0;
        est_renverse = false;

        foreach (string nom in nom_joueurs)
        {
            joueurs.Add(new Joueur(nom));
        }

        Commencer();
    }

    private void Commencer()
    {
        // partage 7 cartes a chaque joueur
        for (int i = 0; i < 7; i++)
        {
            foreach (Joueur joueur in joueurs)
            {
                joueur.Ramasser(banque);
            }
        }

        // placer la premiere carte sur la pile
        Carte prem_carte;
        do
        {
            prem_carte = banque.CarteRamasse();
        } while (prem_carte.Couleur == "Joker"); // S'assurer que la premiere cate n'est pas un joker
        pile.Add(prem_carte);

        Console.WriteLine($"Le jeu a commence! premiere carte: {prem_carte.AfficherCarte()}");
        if (prem_carte.Valeur == "Passer")
        {
            Joueur_suivant(); 
        }
        else if (prem_carte.Valeur == "Renverser")
        {
            est_renverse = !est_renverse;
            joueur_index = joueurs.Count - 1;

        }
    }

    public void Jouer()
    {
        while (!JeuTermine())
        {
            Joueur joueur_actuel = joueurs[joueur_index];
            Console.WriteLine($"\n Tour de {joueur_actuel.Nom}. Carte au-dessus: {CarteDessus().AfficherCarte()}");

            
            AfficherCarteJoueur(joueur_actuel);

           
            Console.WriteLine("choisissez une carte pour jouer ou taper  'r' pour ramasser:");
            var input = Console.ReadLine();

            if (input.ToLower() == "r")
            {   
                if (banque.BanqueTaille() == 0)
                {
                    banque = new Banque(pile);
                }
                joueur_actuel.Ramasser(banque);
                Console.WriteLine($"{joueur_actuel.Nom} a ramassé une carte");
            }
            else if (int.TryParse(input, out int index_carte) && index_carte > 0 && index_carte <= joueur_actuel.Main.Count)
            {
                JouerCarte(joueur_actuel, index_carte - 1);
            }
            else
            {
                Console.WriteLine("Veillez entrer une bonne valeur");
                continue;
            }

            // prochain joueur
            Joueur_suivant();
        }

        Console.WriteLine($"Jeu terminé! Gagnant: {Gagnant().Nom}");
    }

    private void AfficherCarteJoueur(Joueur joueur)
    {
        for (int i = 0; i < joueur.Main.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {joueur.Main[i].AfficherCarte()}");
        }
    }

    private Carte CarteDessus()
    {
        return pile[pile.Count - 1];
    }

    private void JouerCarte(Joueur joueur, int index_carte)
    {
        Carte carte = joueur.Main[index_carte];
        Carte carteDessus = CarteDessus();

        if (ValideJeu(carte, carteDessus))
        {
            joueur.Main.RemoveAt(index_carte);
            pile.Add(carte);
            Console.WriteLine($"{joueur.Nom} a joué {carte.AfficherCarte()}");

            GererCarteSpeciale(carte);
        }
        else
        {
            Console.WriteLine("\nMauvais jeu, Tu dois jouer la bonne carte selon la couleur ou le numéro de la carte du dessus");
            if (banque.BanqueTaille() == 0)
                {
                    banque = new Banque(pile);
                }
                joueur.Ramasser(banque);
                Console.WriteLine($"{joueur.Nom} a ramassé une carte");

        }
    }

    private bool ValideJeu(Carte carte, Carte carteDessus)
    {
        return carte.Couleur == carteDessus.Couleur || carte.Valeur == carteDessus.Valeur || carte.Valeur == "Joker";
    }

    private void GererCarteSpeciale(Carte carte)
    {
        switch (carte.Valeur)
        {
            case "Renverser":
                est_renverse = !est_renverse;
                Console.WriteLine("Direction du jeu a changé");
                break;
            case "Passer":
                Joueur_suivant(); 
                Console.WriteLine("Joueur suivant sauté!");
                break;
            case "RamasseDeux":
                Joueur_suivant();
                joueurs[joueur_index].Ramasser(banque);
                joueurs[joueur_index].Ramasser(banque);
                Console.WriteLine($"{joueurs[joueur_index].Nom} a ramassé deux cartes!");
                break;
            case "Joker":
                Console.WriteLine("Choisi une couleur: Rouge, Jaune, Vert, ou Bleu: (attention écrire la couleur telle indiqué)");
                carte.Couleur = Console.ReadLine();
                break;
        }
    }

    private void Joueur_suivant()
    {
        if (est_renverse)
        {
            joueur_index = (joueur_index - 1 + joueurs.Count) % joueurs.Count;
        }
        else
        {
            joueur_index = (joueur_index + 1) % joueurs.Count;
        }
    }

    private bool JeuTermine()
    {
        return joueurs.Any(joueur => joueur.Main.Count == 0);
    }

    private Joueur Gagnant()
    {
        return joueurs.FirstOrDefault(joueur => joueur.Main.Count == 0);
    }
}

}