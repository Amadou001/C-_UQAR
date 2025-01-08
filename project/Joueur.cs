using System;
using System.Collections.Generic;
using System.Linq;

namespace Structure
{
    public class Joueur
{
    public string Nom { get; set; }
    public List<Carte> Main { get; set; }

    public Joueur(string nom)
    {
        Nom = nom;
        Main = new List<Carte>();
    }

    public void Ramasser(Banque banque)
    {
        Main.Add(banque.CarteRamasse());
    }

    public void AfficherJoueur()
    {
        Console.WriteLine($"{Nom} a {Main.Count} carte.");
    }
}

}