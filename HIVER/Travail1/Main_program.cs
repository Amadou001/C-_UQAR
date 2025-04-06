using System;
using System.Collections.Generic;

using GestionMagasin;

namespace Program
{
    class Main_program
    {
        public static void Main()
        { 
            Utilisateur utilisateur = new Proprietaire();
            utilisateur.CreerAdministrateur(); // Créer un administrateur
            Fleur fleur = new Fleur();
            fleur.ChargementDuStockFromCsv(); // Charger le stock à partir d'un fichier CSV
            GestionMenus obj = new GestionMenus();
            obj.GestionFlux();
        }
    }
}