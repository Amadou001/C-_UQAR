using System;
using System.Collections.Generic;
using Newtonsoft.Json;



namespace GestionMagasin
{
    class Bouquet: Article
    {
        public List<Fleur> listFleurs = new List<Fleur>();
        public static List<Bouquet> list_bouquets = new List<Bouquet>();

        public static int count = 0;
        public int Model { get; set; } // pour identifier chaque bouquet

        public Bouquet(List<Fleur> listFleurs)
        {
            Random random = new Random();
            count = random.Next(100000, 999999);
            this.Model = count;
            this.listFleurs = listFleurs;
            base.Prix = CalculerPrix();
            list_bouquets.Add(this);
            this.ToJson();
        }

        public Bouquet()
        {}


        public override void ToJson()
        {
            try
            {
                // Set up serializer settings to ignore reference loops
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                // Serialize the list of Fleur objects
                string json = JsonConvert.SerializeObject(list_bouquets, Formatting.Indented, settings);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "JsonFiles", "Bouquets.json");
                File.WriteAllText(filePath, json);
                //Console.WriteLine($"JSON file written to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static void FromJson()
        {
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "JsonFiles", "Bouquets.json");
                string json = File.ReadAllText(filePath);
                list_bouquets = JsonConvert.DeserializeObject<List<Bouquet>>(json);
                //Console.WriteLine($"JSON file read from {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public double CalculerPrix()
        {
            double prix = 0;
            foreach (Fleur fleur in listFleurs)
            {
                prix += fleur.Prix;
            }
            return prix + 3;
        }
        

        public void AjouterFleur(Fleur fleur)
        {
            listFleurs.Add(fleur);
            this.Prix = CalculerPrix();
        }

        public static void AfficherBouquets()
        {
            Bouquet.FromJson();

            // Table header
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Model",-10} {"Fleur(s)",-30} {"Prix",-10}");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            // Table rows
            foreach (Bouquet bouquet in list_bouquets)
            {
                string fleurs = string.Join(", ", bouquet.listFleurs.ConvertAll(f => f.Nom));
                Console.WriteLine($"{bouquet.Model,-10} {fleurs,-30} {bouquet.Prix, -10:C2}");
            }

            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }

        public static Bouquet GetBouquetByModel(int model)
        {
            //Bouquet.FromJson();
            foreach (Bouquet bouquet in list_bouquets)
            {
                if (bouquet.Model == model)
                {
                    return bouquet;
                }
            }
            return null;
        }

        public void AjouterBouquet(Bouquet bouquet)
        {
            FromJson();
            list_bouquets.Add(bouquet);
            this.ToJson();
        }
        public void AfficherUnBouquet()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Model",-10} {"Fleur(s)",-30} {"Prix",-10} $");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            // Table rows
            string fleurs = string.Join(", ", this.listFleurs.ConvertAll(f => f.Nom));
            Console.WriteLine($"{this.Model,-10} {fleurs,-30} {this.Prix, -10:C2}");

            Console.WriteLine("---------------------------------------------------------------------------------------------");



    }
}
}