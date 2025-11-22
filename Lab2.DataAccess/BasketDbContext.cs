using Microsoft.EntityFrameworkCore;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab2.DataAccess
{
    public class BasketDbContext : DbContext
    {
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Pircejs> Pircejs { get; set; }
        public BasketDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\coding and apps\\code\\Lab2.DataAccess\\Lab2.DataAccess\\BasketDb.mdf ;Integrated Security=True");



    }
}