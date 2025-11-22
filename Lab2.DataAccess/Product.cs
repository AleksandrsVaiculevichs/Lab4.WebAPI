using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DataAccess
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        public int ProductPrice { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductCategory { get; set; }

        public List<Basket> Baskets { get; } = new();
        public List<Pircejs> Pircejs { get; } = new();

    }
}
