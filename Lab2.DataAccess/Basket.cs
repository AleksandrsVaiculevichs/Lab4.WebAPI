using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DataAccess
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }
        public int BasketNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string BasketType { get; set; }

        [Required]
        public int BasketSize { get; set; }    
        
        public Product Products { get; set; }
        public Pircejs Pircejs { get; set; }
    }
}
