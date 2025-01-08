using System;
using System.Collections.Generic;
using System.Linq;


namespace Structure
{
    public class Banque
{
    private List<Carte> cartes;

    public Banque()
    {
        cartes = new List<Carte>();
        InitialiseBanque();
        Melanger();
    }
    public Banque(List<Carte> pile)
    {
        cartes = new List<Carte>(pile);
        InitialiseBanque();
        Melanger();
    }

    private void InitialiseBanque()
    {
        string[] couleurs = { "Rouge", "Jaune", "Vert", "Bleu" };
        string[] valeurs = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "Passer", "Renverser", "RamasseDeux"};

        foreach (string coleur in couleurs)
        {
            foreach (string valeur in valeurs)
            {
                cartes.Add(new Carte(coleur, valeur));
                if (valeur != "0") cartes.Add(new Carte(coleur, valeur)); 
            }
        }

        cartes.AddRange(new[] {
            new Carte("joker", "Joker")});

    }

    private void Melanger()
    {
        Random rand = new Random();
        cartes = cartes.OrderBy(_ => rand.Next()).ToList();
    }

    public Carte CarteRamasse()
    {
        if (cartes.Count == 0) throw new InvalidOperationException("La banque est vide.");
        Carte carte = cartes[0];
        cartes.RemoveAt(0);
        return carte;
    }

    public int BanqueTaille()
    {
        return cartes.Count;
    }
}

}
