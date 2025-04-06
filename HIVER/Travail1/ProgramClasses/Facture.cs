using System;
using System.Collections.Generic;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace GestionMagasin
{
    class Facture
    {
        private Commande commande;
        public static List<Facture> list_facture = new List<Facture>();
        private DateTime DateFacture { get; set; }
        private double PrixTotal { get; set; }

        public Facture(Commande commande)
        {
            this.commande = commande;
            this.DateFacture = DateTime.Now;
            this.PrixTotal = commande.CalculerPrixTotal();
            list_facture.Add(this);
            this.ToJson();
        }



        // méthode pour générer la facture en format PDF
        public void GenererFacture()
        {
            if (commande == null || commande.Article == null || commande.Client == null || commande.Vendeur == null)
            {
                throw new InvalidOperationException("Les informations de la commande sont incomplètes.");
            }
            
            string directory = "Factures";
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), directory, $"Facture_{commande.Client.Nom}_{commande.Client.Prenom}_{DateFacture:yyyyMMddHHmmss}.pdf");
            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Draw title
                XFont font = new XFont("Arial", 20);
                gfx.DrawString("FACTURE", font, XBrushes.Black, new XPoint(200, 40));

                // Draw date
                font = new XFont("Arial", 12);
                gfx.DrawString($"Date: {DateFacture:yyyy-MM-dd HH:mm}", font, XBrushes.Black, new XPoint(20, 80));

                // Draw table-like structure for the invoice
                string article_info = "";
                if (commande.Article is Fleur fleur)
                {
                    article_info = $"Fleur: {fleur.Nom} - {fleur.Couleur} - {fleur.Description}";
                }
                else if (commande.Article is Bouquet bouquet)
                {
                    article_info = "Bouquet: model " + bouquet.Model + " - ";
                }

                gfx.DrawString($"Client: {commande.Client.Nom} {commande.Client.Prenom}", font, XBrushes.Black, new XPoint(20, 120));
                gfx.DrawString($"Vendeur: {commande.Vendeur.Nom} {commande.Vendeur.Prenom}", font, XBrushes.Black, new XPoint(20, 140));
                gfx.DrawString($"Article: {article_info}", font, XBrushes.Black, new XPoint(20, 160));
                gfx.DrawString($"Méthode de paiement: {commande.TypePaiement}", font, XBrushes.Black, new XPoint(20, 180));
                gfx.DrawString($"Total: {commande.Article.Prix:C2}", font, XBrushes.Black, new XPoint(20, 200));

                // Total Price
                gfx.DrawString($"Total Price: {PrixTotal:C2}", font, XBrushes.Black, new XPoint(20, 240));

                // Save the document
                document.Save(filePath);
            }

            Console.WriteLine($"Facture générée : {filePath}");
        }


        public void ToJson()
        {
            try
            {
                string json = JsonConvert.SerializeObject(
                    list_facture,
                    Formatting.Indented,
                    new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
                );

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "JsonFiles", "Facture.json");
                File.WriteAllText(filePath, json);
                //Console.WriteLine($"JSON file written to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void FromJson()
        {
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "JsonFiles", "Facture.json");
                string json = File.ReadAllText(filePath);
                list_facture = JsonConvert.DeserializeObject<List<Facture>>(json);
                //Console.WriteLine($"JSON file read from {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }

    }
    
}
