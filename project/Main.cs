using System;
using System.Collections.Generic;
using Structure;

namespace MainProgram
{
    class MainClass
    {
        public static void Main()
        {
            Jeu jeu = new Jeu(new List<string> { "Marie", "David", "Jeremie" });
            jeu.Jouer();

        }
    }
}
