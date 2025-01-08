using System;
using System.Collections.Generic;

namespace Program
{
    class Pokemons
    {
        private List<Pokemon> pokemons;


        public Pokemons()
        {
            this.pokemons = new List<Pokemon>();
        }

        public void AjoutePokemon(string nomClass, params object[] args)
        {
            Pokemon pokemon = null;

            switch (nomClass)
            {
                case "Sportif":
                    pokemon = new Sportif(
                         args[0].ToString(),
                    Convert.ToSingle(args[1]),
                    Convert.ToInt32(args[2]),
                    Convert.ToSingle(args[3]),
                    Convert.ToSingle(args[4]) 
                    );
                    break;

                case "Casanier":
                    pokemon = new Casanier(
                         args[0].ToString(),
                    Convert.ToSingle(args[1]),
                    Convert.ToInt32(args[2]),
                    Convert.ToSingle(args[3]),
                    Convert.ToInt32(args[4]) 
                    );
                    break;

                case "Mer":
                    pokemon = new Mer(
                        Convert.ToString(args[0]),
                        Convert.ToSingle(args[1]),
                        Convert.ToInt32(args[2])
                    );
                    break;

                case "Croisiere":
                    pokemon = new Croisiere(
                        Convert.ToString(args[0]),
                        Convert.ToSingle(args[1]),
                        Convert.ToInt32(args[2])
                    );
                    break;

                default:
                    Console.WriteLine("Invalid Pokemon");
                    break;
            }

            this.pokemons.Add(pokemon);
        }


        public void Afficher_pokemons()
        {
            foreach (Pokemon pokemon in this.pokemons)
            {
                pokemon.Affiche();
            }
        }


        public void Count_pokemons()
        {
            int num_sportif = 0, num_casanier = 0, num_mer = 0, num_croisiere = 0, total = 0;

            foreach (Pokemon pokemon in this.pokemons)
            {   
                Type objType = pokemon.GetType();

                if (objType.Equals(typeof(Sportif)))
                {
                    num_sportif++;
                }
                else if (objType.Equals(typeof(Casanier)))
                {
                    num_casanier++;
                }
                else if(objType.Equals(typeof(Mer)))
                {
                    num_mer++;
                }
                else
                {
                    num_croisiere++;
                }
            }
            
            total = num_sportif + num_casanier + num_mer + num_croisiere;
            Console.Write("\n Nombre de Pokemons: \n -Sportif: {0}\n -Casanier: {1}\n -Mer: {2}\n -Croisiere: {3}\n\t Total: {4}", num_sportif, num_casanier, num_mer, num_croisiere, total);
        }

        public void Vitesse_moyenne()
        {   
            float sum_vitesse = 0;
            float Vitesse_moy = 0;
             foreach (Pokemon pokemon in this.pokemons)
            {
                sum_vitesse += pokemon.Vitesse();   
            }

            Vitesse_moy = sum_vitesse / this.pokemons.Count;

            Console.WriteLine($"\nLa vitesse moyenne des pokemons crées est de {Vitesse_moy:F2} km/h");
        }

    }

}
