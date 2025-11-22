using Microsoft.EntityFrameworkCore;
using Lab2.DataAccess;

namespace Lab2.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new BasketDbContext();
            db.Database.EnsureCreated();

            //pievienojam ierakstus 3.1
            var newProduct1 = new Product
            {
                ProductName = "Piens Alma",
                ProductPrice = 1,
                ProductCategory = "Milk"
            };
            var newProduct2 = new Product
            {
                ProductName = "Melna Maize",
                ProductPrice = 2,
                ProductCategory = "Bread"
            };
            var newProduct3 = new Product
            {
                ProductName = "Konfekte Migla",
                ProductPrice = 3,
                ProductCategory = "Candy"
            };

            db.Products.AddRange(newProduct1, newProduct2, newProduct3);
            db.SaveChanges();

            var newPircejs1 = new Pircejs
            {
                Name = "Aleksandrs",
                Surname = "Vaiculevičs"
            };

            db.Pircejs.Add(newPircejs1);
            db.SaveChanges();

            // izdzest ierakstus 3.2
            db.Remove(newProduct3);
            db.SaveChanges();

            // Modififcet ierakstus 3.3
            newProduct2.ProductPrice = 1;
            db.SaveChanges();

            // Atlasit ierakstus 2.1
            var results = db.Products
                .Include(s => s.Baskets)
                .Where(s => s.ProductName == "Melna Maize");

            foreach (var product in results)
            {
                System.Console.WriteLine($" 2.1. Uzdevuma Atbilde: {product.ProductName} {product.ProductPrice} {product.ProductCategory}");
            }

            // Atlasit ierakstu firstordefault 2.2
            var pirmais_produkts = db.Products.FirstOrDefault();
            System.Console.WriteLine($" 2.2. Uzdevuma Atbilde: {pirmais_produkts.ProductName} {pirmais_produkts.ProductPrice} {pirmais_produkts.ProductCategory}");
            

            //// mes katru reizi pievienojam jaunus ierakstus, tāpēc ir nepieciešams to attīrīt
            //var allproducts = db.Products.ToList();
            //db.RemoveRange(allproducts);
            //db.SaveChanges();
        }
    }
}

