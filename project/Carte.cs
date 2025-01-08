using System;

namespace Structure
{
    public class Carte
{
    public string Couleur { get; set; } 
    public string Valeur { get; set; } 

    public Carte(string couleur, string valeur)
    {
        Couleur = couleur;
        Valeur = valeur;
    }

    public string AfficherCarte()
    {
        return $"{Couleur} {Valeur}";
    }


}
}
