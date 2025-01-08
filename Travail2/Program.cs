using System;

using Program;

namespace Program1
{
    class Prog
    {
        static void Main(string[] args)
    {

        Pokemons obj = new Pokemons();;

        obj.AjoutePokemon("Sportif", "Pikachu", 18, 2, 0.85f, 120);
        obj.AjoutePokemon("Sportif", "Hippodocus", 20, 2, 0.9f, 130);
        obj.AjoutePokemon("Casanier", "Salameche", 12, 2, 3.9f, 8);
        obj.AjoutePokemon("Casanier", "Elekable", 14, 2, 5.9f, 10);
        obj.AjoutePokemon("Mer", "Rondoudou", 45, 2);
        obj.AjoutePokemon("Mer", "Carapuce", 55, 2);
        obj.AjoutePokemon("Croisiere", "Bulbizarre", 15, 3);
        obj.AjoutePokemon("Croisiere", "Leviator", 17, 3);
        obj.Afficher_pokemons();
        obj.Count_pokemons();
        obj.Vitesse_moyenne();
    }
    }
}
