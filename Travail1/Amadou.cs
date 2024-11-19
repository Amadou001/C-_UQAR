using System;
using System.Collections.Generic;
using System.Linq;



class Exo1
{
    public void fonction_ex()
    {
        Console.WriteLine("\n    SOLUTIONNER  UNE EQUATION DU SECOND DÉGRÉS DANS R\n");
        Console.WriteLine(" ax² + bx + c = 0");

        Console.WriteLine(" choissez une valeurs pour a ");
        float a = float.Parse(Console.ReadLine());

        Console.WriteLine(" choissez une valeurs pour b ");
        float b = float.Parse(Console.ReadLine());

        Console.WriteLine(" choissez une valeurs pour c ");
        float c = float.Parse(Console.ReadLine());
        Console.WriteLine();

        Console.WriteLine(" Votre équation est donc: " + a + "x² + " + b + "x + " + c + "= 0");

        double delta = b*b - (4 * a * c);

        if (delta < 0)
            Console.WriteLine(" cette équation n'a pas de solution dans R");

        if ( delta>0)
        {
            double x1 = (-b - Math.Sqrt(delta)) / (2 * a);
            double x2 = (-b + Math.Sqrt(delta)) / (2 * a);
            string format1 = x1.ToString("F2");
            string format2 = x2.ToString("F2");


            Console.WriteLine(" cette equation admet deux solutions distinctes x₁ =" + format1 + " et x₂=" + format2);

        }

        if ( delta == 0)

        {
            double x = -b / (2 * a);
            string format = x.ToString("F2");
            Console.WriteLine(" Cette équation admet une solution double x₀=" + format);

        }
        }
}




class Exo2
{
    public void fonction_ex()
    {
        int dividende, diviseur;
        int quotient = 0;
        int reste = 0;
        int calcul = 0;
        bool valid = true;

        Console.WriteLine("\n      DIVISION\n");
        Console.WriteLine("Entrez votre dividende: ");
        dividende = Int32.Parse(Console.ReadLine());

        Console.WriteLine("Entrez votre diviseur: ");
        diviseur = Int32.Parse(Console.ReadLine());
        Console.WriteLine();



        if (diviseur == 0 )  
            valid = false;


        if ( dividende>0 && diviseur>0)

        {
            for (int i = 0; i <= dividende; i++)
            {

                for (int j = 0; j < diviseur; j++)
                {

                    calcul = (diviseur * i) + j;
                    
                    if (dividende == calcul)

                    {
                        quotient = i;
                        reste = j;
                        
                        break;

                    }


                }


            }
        }


        if (dividende<0 && diviseur<0)

        {
            for (int i = 0; i >= dividende; i--)
            {

                for (int j = 0; j > diviseur; j--)
                {

                    calcul = (diviseur * (-i)) +j;
            
                        if (dividende == calcul)

                        {
                            quotient = -i;
                            reste = j;
                            
                            break;

                        }
                }


            }
        }

        if (dividende > 0 && diviseur < 0)

        {
            for (int i = 0; i <= dividende; i++)
            {
                for (int j = 0; j > diviseur; j--)
                {
                    calcul = (diviseur * (-i)) + (-j);
                    
                    if (dividende == calcul)

                    {
                        quotient = -i;
                        reste = -j;
                        
                        break;

                    }
                }
            }
        }


        if (dividende < 0 && diviseur > 0)


        {
            for (int i = 0; i >= dividende; i--)
            {

                for (int j = 0; j < diviseur; j++)
                {

                    calcul = (diviseur * i) + (-j);
                    
                    if (dividende == calcul )

                    {
                        
                        quotient = i;
                        reste = -j;
                        
                        break;

                    }
                    
                }

            }
        }


        if (valid)
            Console.Write($"Quotient: {quotient} \nReste: {reste}\n");
        else
            Console.WriteLine("Division impossible");
        }
}




class Exo3
{
    public void fonction_ex()
    {
        int nbrDecimale;
        string hexa;
        string binaire;

        Console.WriteLine("\n       HEXA_TO_BIN\n");
        Console.WriteLine("Entrez un nombre hexadécimal");
        hexa = Console.ReadLine();
        Console.WriteLine();

        try
        {
            nbrDecimale = Convert.ToInt32(hexa, 16);

            binaire = Convert.ToString(nbrDecimale, 2);

            Console.WriteLine($"{hexa}₁₆ = {binaire}₂");
        }
        catch
        {
            Console.WriteLine("Nombre hexadécimal non valide");
        }
    }
}




class Exo4
{
    public void fonction_ex()
    {
        List<double> tableau = new List<double>();

        Console.WriteLine("\n           ECART TYPE\n");
        Console.WriteLine("Entrez les éléments du tableau (entrez 'stop' pour terminer) :");

        while (true)
        {
            string entree = Console.ReadLine();
            if (entree.ToLower() == "stop")
                break;

            if (double.TryParse(entree, out double nombre))
            {
                tableau.Add(nombre);
            }
            else
            {
                Console.WriteLine("Veuillez entrer un nombre valide.");
            }
        }

        if (tableau.Count > 0)
        {
            double ecartType = CalculerEcartType(tableau);
            Console.WriteLine($"\nL'écart-type du tableau est : {ecartType:F4}");
        }
        else
        {
            Console.WriteLine("Le tableau est vide.");
        }
    }
    public double CalculerEcartType(List<double> tableau)
    {
        double moyenne = tableau.Average();
        double variance = tableau.Sum(x => Math.Pow(x - moyenne, 2)) / tableau.Count;
        return Math.Sqrt(variance);
    }
}



class Exo5
{
    public int CalculerPoids(string phrase)
    {
        int poids = 0;
        foreach (char c in phrase.ToUpper())
        {
            if (c >= 'A' && c <= 'Z')
            {
                poids += c - 'A' + 1;
            }
        }
        return poids;
    }

    public void fonction_ex()
    {
        List<string> phrases = new List<string>();
        Console.WriteLine("\n           CLASSER PHRASE\n");
        Console.WriteLine("Entrez les phrases (entrez 'stop' pour terminer) :");

        while (true)
        {
            string phrase = Console.ReadLine();
            if (phrase.ToLower() == "stop")
                break;
            phrases.Add(phrase);
        }

        var phrasesTriees = phrases.OrderBy(CalculerPoids).ToList();

        Console.WriteLine("\nPhrases classées par poids :");
        foreach (var phrase in phrasesTriees)
        {
            Console.WriteLine($"{phrase} (Poids: {CalculerPoids(phrase)})");
        }
    }
}




class Exo6
{
    public string FusionnerMots(string mot1, string mot2)
    {
        int longueurMax = Math.Max(mot1.Length, mot2.Length);
        char[] motFusionne = new char[longueurMax * 2];
        int index = 0;

        for (int i = 0; i < longueurMax; i++)
        {
            if (i < mot1.Length)
            {
                motFusionne[index++] = mot1[i];
            }
            if (i < mot2.Length)
            {
                motFusionne[index++] = mot2[i];
            }
        }

        return new string(motFusionne, 0, index);
    }


    public void fonction_ex()
    {
        Console.WriteLine("\n        FUSION MOTS\n");
        Console.WriteLine("Entrez le premier mot :");
        string mot1 = Console.ReadLine();

        Console.WriteLine("Entrez le deuxième mot :");
        string mot2 = Console.ReadLine();

        string mot3 = FusionnerMots(mot1, mot2);
        Console.WriteLine($"\nLe mot fusionné est : {mot3}");
    }
}


class Exo7
{
    public void fonction_ex()
    {

        Console.WriteLine("\n           AFFICHE CHARACTÈRES\n");
        Console.Write("Entrer le nombre de lignes : ");
        int lignes = int.Parse(Console.ReadLine());

        Console.Write("Entrer le nombre de lettres à utiliser : ");
        Console.WriteLine();
        int nbLettres = int.Parse(Console.ReadLine());

        char lettre = 'A'; 
        int compteurLettres = 0; 

        for (int i = 0; i < lignes; i++)
        {
           
            Console.Write(new string(' ', i));

            
            for (int j = 0; j < lignes - i; j++)
            {

                Console.Write(lettre + " ");
                
                lettre++;
                compteurLettres++;


                if (compteurLettres >= nbLettres)
                {
                    lettre = 'A';
                    compteurLettres = 0; 
                }
            }
            Console.WriteLine();
        }
    }
}




class Exo8
{
    List<int> numbers = new List<int>();
    public void Jackpot()
    {
        Random random = new Random();
        int temp = 0;
        int i = 0;

        while(i < 5)
        {
            temp = random.Next(10);
           // if (!numbers.Contains(temp))
           // {
                numbers.Add(temp);
                i++;
            //}
        }
        
    }

    public int Win_check()
    {
        int temp, rep = 0;
        for (int i = 0; i < numbers.Count; i++)
        {
            temp = numbers[i];
            int rep_temp = 0;
            for (int j = i + 1; j < numbers.Count; j++)
            {
                if (temp == numbers[j])
                {
                    rep_temp++;
                }
            }

            if (rep_temp > rep)
                rep = rep_temp;
        }

        return rep;
    }

    public void Prize(double montant, int rep)
    {
        int score = rep + 1;
        switch(score)
        {
            case 3:
            case 4:
                Console.WriteLine($"Remboursement: {montant}");
                break;
            case 5:
                Console.WriteLine($"Félicitations !!!! \n Montant gagné : {Math.Pow(montant, 5)}");
                break;
            default:
                Console.WriteLine("Vous avez perdu !!!");
                break;
        }

    }

    public void Value_gen()
    {
        Console.Write("Valeur générée: ");
       for (int i = 0; i < numbers.Count; i++)
        {
            Console.Write(numbers[i] + " ");
        }
        Console.WriteLine();
    }

    public void fonction_ex()
    {
        double montant = 0;
        Console.WriteLine("\n        MACHINE A SOUS\n");
        Console.Write("Faites entrer votre argent :");
        montant = double.Parse(Console.ReadLine());
        Console.WriteLine();
        Jackpot();
        Value_gen();
        Prize(montant, Win_check());
    }
}



class Exo9
{
    Dictionary <int, string> questions = new Dictionary<int, string>();
    List <int> ques_demandees = new List<int>();
    List <int> fausse_rep = new List<int>();
    string[] bon_reponses = new string[10];
    string[] mauv_reponses = new string[10];
    public void Initializer()
    {
        questions[0] = "Quelle est la capitale de la France ?";
        questions[1] = "Quel est le plus grand océan du monde ?";
        questions[2] = "Quel est l'élément chimique avec le symbole \"O\" ?";
        questions[3] = "Qui a écrit Les Misérables ?";
        questions[4] = "Quel est le nombre de continents sur Terre ?";
        questions[5] = "Quelle planète est la plus proche du Soleil ?";
        questions[6] = "Quel est le plus long fleuve du monde ?";
        questions[7] = "Qui a peint la Joconde ?";
        questions[8] = "Dans quel pays se trouve le Colisée ?";
        questions[9] = "Quel est le langage utilisé pour le développement d'applications Android ?";

        
        bon_reponses[0] = "Paris";
        bon_reponses[1] = "Océan Pacifique";
        bon_reponses[2] = "Oxygène";
        bon_reponses[3] = "Victor Hugo";
        bon_reponses[4] = "5";
        bon_reponses[5] = "Mercure";
        bon_reponses[6] = "Le Nil";
        bon_reponses[7] = "Léonard de Vinci";
        bon_reponses[8] = "Italie";
        bon_reponses[9] = "Java";
    
        mauv_reponses[0] = "Marseille, Lyon";
        mauv_reponses[1] = "Océan Atlantique, Océan Indien";
        mauv_reponses[2] = "Or, Osmium";
        mauv_reponses[3] = "Émile Zola, Gustave Flaubert";
        mauv_reponses[4] = "7, 6";
        mauv_reponses[5] = "Mars, Vénus";
        mauv_reponses[6] = "L'Amazone, Le Mississippi";
        mauv_reponses[7] = "Vincent Van Gogh, Michel-Ange";
        mauv_reponses[8] = "Espagne, Grèce";
        mauv_reponses[9] = "C++, Python";


    }
    
    

    public int Question_selection()
    {
        Random random = new Random();

        int questin_num = random.Next(10);

        return questin_num;
    }

    public void reponses_possibles(int question_num)
    {
        string[] false_ans = mauv_reponses[question_num].Split(", ");
        Console.WriteLine(questions[question_num]+ "\n");
        Console.WriteLine($"1) {bon_reponses[question_num]}");
        Console.WriteLine($"2) {false_ans[0]}");
        Console.WriteLine($"3) {false_ans[1]}\n");

    }

    public void correct_reponses()
    {
        if (fausse_rep.Count > 0)
        {
            Console.WriteLine("Correction:");
            foreach (int item in fausse_rep)
            {
                Console.WriteLine($"{questions[item]} reponse: {bon_reponses[item]}");
            }
        }

    }

    public void results(int score)
    {
        switch (score)
        {
            case 0:
            case 1:
                Console.WriteLine($"Resultat: E \n");
                correct_reponses();
                break;
            case 2:
                Console.WriteLine($"Resultat: D \n ");
                correct_reponses();
                break;
            case 3:
                Console.WriteLine($"Resultat: B \n ");
                correct_reponses();
                break;
            default:
                Console.WriteLine($"Resultat: A \n ");
                correct_reponses();
                break;
        }
    }

    public void fonction_ex()
    {
        int question_num;
        Initializer();
        int i = 0;
        int score = 0;
        string rep;
        Console.WriteLine("\n              QCM\n");
        Console.WriteLine("Pour repondre aux questions suivantes, veillez saisir 1, 2 ou 3 \n");
        do
        {
            question_num = Question_selection();
            if (!ques_demandees.Contains(question_num))
            {
                ques_demandees.Add(question_num);
                reponses_possibles(question_num);
                i++;
                Console.Write("reponse: ");
                rep = Console.ReadLine();
                Console.WriteLine();
                if (rep == "1")
                {
                    score++;
                }
                else
                    fausse_rep.Add(question_num);
            }

        }while(i < 4);
        
        Console.Clear();
        results(score);
    }
}










class Multi_fonction
{
    static void Main()
    {   string continuerMenu = "";
        bool continuer = true;
        while (continuer)
        {
            string choix = "";
            Console.WriteLine("\n_____________________MENU_________________________");
            Console.WriteLine("|                                                 |");
            Console.Write("| 1- Machine à sous (jackpot)                     |\n");
            Console.Write("| 2- QCM (Question à choix multiple)              |\n");
            Console.Write("| 3- Delta (equation 2nd degrés dans R)           |\n");
            Console.Write("| 4- Division (quotient & reste sans utiliser \"/\")|\n");
            Console.Write("| 5- Conversion from Hexa to Bin                  |\n");
            Console.Write("| 6- Ecart Type                                   |\n");
            Console.Write("| 7- Classer Phrase                               |\n");
            Console.Write("| 8- Fusion Mots                                  |\n");
            Console.Write("| 9- Affiche Charactères                          |\n");
            Console.WriteLine("|_________________________________________________|");

            Console.Write("Entrer votre choix: ");
            choix = Console.ReadLine();

            switch(choix)
            {
                case "1":
                    Exo8 obj1 = new Exo8();
                    obj1.fonction_ex();
                    break;
                case "2":
                    Exo9 obj2 = new Exo9();
                    obj2.fonction_ex();
                    break;
                case "3":
                    Exo1 obj3 = new Exo1();
                    obj3.fonction_ex();
                    break;
                case "4":
                    Exo2 obj4 = new Exo2();
                    obj4.fonction_ex();
                    break;
                case "5":
                    Exo3 obj5 = new Exo3();
                    obj5.fonction_ex();
                    break;
                case "6":
                    Exo4 obj6 = new Exo4();
                    obj6.fonction_ex();
                    break;
                case "7":
                    Exo5 obj7 = new Exo5();
                    obj7.fonction_ex();
                    break;
                case "8":
                    Exo6 obj8 = new Exo6();
                    obj8.fonction_ex();
                    break;
                case "9":
                    Exo7 obj9 = new Exo7();
                    obj9.fonction_ex();
                    break;

                default:
                    Console.WriteLine("\n     Choix inexistant!! \n");
                    break;
            }


            Console.WriteLine("\nPour continuer rentrer 1 ou n'importe quel autre bouton pour quitter\n");
            continuerMenu = Console.ReadLine();
            if (continuerMenu != "1")
                continuer = false;
        }
        
    }
}
