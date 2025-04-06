using System;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace GestionMagasin
{
    class Fleur : Article
    {
        // Static list to hold flower objects
        public static List<Fleur> list_fleurs = new List<Fleur>();

        public string? Nom { get; set; }
        public string? Couleur { get; set; }
        public string? Description { get; set; }

        public int Quantite { get; set; }

        // Constructor with parameters for Fleur
        public Fleur(string nom, string couleur, string description, double prix, int quantite) : base(prix)
        {
            this.Nom = nom;
            this.Couleur = couleur;
            this.Description = description;
            this.Quantite = quantite;
            
        }

        // Default constructor for Fleur
        public Fleur()
        {
        }

        // Method to load Fleur data from a CSV file into the static list
        public void LoadFleursFromCsv()
        {
            list_fleurs.Clear();  // Clear existing flowers to prevent duplication
            using (var reader = new StreamReader("fleurs_db.csv"))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null, // Skip header validation
                MissingFieldFound = null // Skip missing field validation
            }))
            {
                // Read records from CSV
                var records = csv.GetRecords<dynamic>(); // Use dynamic for direct header access

                // Add records to the list
                foreach (var record in records)
                {
                    // Access each field by header name (as defined in the CSV)
                    string nom = record.Nom ?? "Unknown";
                    string couleur = record.Couleur ?? "Unknown";
                    string description = record.Caractéristiques ?? "Unknown";
                    double prix = record.PrixUnitaireCAD != null ? Convert.ToDouble(record.PrixUnitaireCAD ) : 0.0;
                    int quantite = 10; // Default quantity for each flower

                    // Create a new Fleur object and add it to the list
                    Fleur fleur = new Fleur(nom, couleur, description, prix, quantite);
                    list_fleurs.Add(fleur);
                }
                ToJson(); // Write to JSON file
            }

        }

        public override void ToJson()
        {
            try
            {
                string json = JsonConvert.SerializeObject(
                    list_fleurs,
                    Formatting.Indented,
                    new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
                );

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "JsonFiles", "Fleurs.json");
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
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "JsonFiles", "Fleurs.json");

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("No JSON file found.");
                    list_fleurs = new List<Fleur>(); // Assure une liste vide si le fichier n'existe pas
                    return;
                }

                string json = File.ReadAllText(filePath);
                var tempList = JsonConvert.DeserializeObject<List<Fleur>>(json);

                list_fleurs = tempList ?? new List<Fleur>(); // Si désérialisation retourne null

                // Console.WriteLine($"JSON file read from {filePath}");
            }
            catch (Exception ex)
            {
                list_fleurs = new List<Fleur>(); // fallback en cas d'erreur
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

       public static void AfficherFleurs()
        {
            Fleur.FromJson();

            // Table header
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Nom",-20} {"Couleur",-15} {"Description",-30} {"Prix",-10} $ {"Quantité en stock",-10}");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            // Table rows
            foreach (Fleur fleur in list_fleurs)
            {
                Console.WriteLine($"{fleur.Nom,-20} {fleur.Couleur,-15} {fleur.Description,-30} {fleur.Prix, -10:C2}  {fleur.Quantite,-10}");
            }

            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }


        public static Fleur GetFleurByName(string nom)
        {
            //Fleur.FromJson();
            foreach (Fleur fleur in list_fleurs)
            {
                if (fleur.Nom == nom)
                {
                    return fleur;
                }
            }
            return null;
        }

        public Fleur DeleteFleurByName(string nom)
        {
            FromJson();
            Fleur fleur = GetFleurByName(nom);
            if (fleur.Quantite > 0)
            {
                fleur.Quantite--;
                ToJson();
            }
            else
            {
                list_fleurs.Remove(fleur);
                ToJson();
            }
            return fleur;
        }

          public override void DeleteFleursListByName(List<Fleur> fleurs)
        {
            FromJson();
            foreach (Fleur fleur in fleurs)
            {
                Fleur existingFleur = GetFleurByName(fleur.Nom);
                if (existingFleur == null)
                {
                    Console.WriteLine($"Fleur {fleur.Nom} not found.");
                    continue;
                }
                else
                {
                    if (existingFleur.Quantite > 0)
                    {
                        existingFleur.Quantite--;
                    }
                    else
                    {
                        list_fleurs.Remove(fleur);
                    }
                }

                
            }
            Enregistrer();
        }


        public static void Enregistrer()
        {
            Fleur fleur = new Fleur();
            fleur.ToJson();
        }


        public void AfficherUneFleur()
        {
            Console.WriteLine($"Nom: {this.Nom}, Couleur: {this.Couleur}, \nDescription: {this.Description}, \nPrix: {this.Prix:C2} $, \nQuantité restante: {this.Quantite}");
        }

        public void AjouterFleur(Fleur fleur)
        {
            FromJson();
            list_fleurs.Add(fleur);
            this.ToJson();
        }

        public void ChargementDuStockFromCsv() // elle ne sera appelée que si le json est vide
        {
            list_fleurs = new List<Fleur>();
            FromJson();
            if (list_fleurs.Count == 0)
            {
                LoadFleursFromCsv();
                Console.WriteLine("Le stock a été chargé avec succès à partir du fichier csv.");

            }
            else
            {
                Console.WriteLine("Le stock est déjà chargé.");
            }

        }
    }
}
