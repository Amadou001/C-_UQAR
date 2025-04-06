using System;



namespace GestionMagasin
{
    abstract class Article
    {
        public double Prix { get; set; }

        public Article(double prix)
        {
            this.Prix = prix;
        }
        
        public Article()
        {
        }

        public  abstract void ToJson();
        public virtual void DeleteFleursListByName(List<Fleur> fleurs)
        {
            Console.WriteLine("Deleting flowers from the list...");
        }

        
    
    }
}