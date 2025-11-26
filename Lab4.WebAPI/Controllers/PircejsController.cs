using Lab2.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PircejsController : ControllerBase
    {
        // Db context
        private BasketDbContext _db;
        public PircejsController()
        {
            _db = new BasketDbContext();
        }

        //meklēšanas funkcija 3 uzdevums!!!!!!!!!!!!!!!!!!!
        [HttpGet("search")]
        public Product[] SearchProducts(string? name, string? category, int? price)
        {
            var query = _db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.ProductName.Contains(name));

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.ProductCategory.Contains(category));

            if (price.HasValue)
                query = query.Where(p => p.ProductPrice == price);

            return query.ToArray();
        }

        // saraksta iegušana
        [HttpGet]
        public Pircejs[] GetPircejs()
        {
            var data = _db.Pircejs.ToArray();
            return data;
        }
        // Viena ieraksta atgriešanaa
        [HttpGet("{id}")]
        public Pircejs GetPircejs(int id)
        {
            var data = _db.Pircejs.FirstOrDefault(s => s.Id == id);
            return data;
        }
        //Ieraksta pievienošana
        [HttpPost]
        public void Post([FromBody] Pircejs pircejs)
        {
            _db.Pircejs.Add(pircejs);
            _db.SaveChanges();
        }
        //ieraksta dzēšana
        [HttpDelete("{id}")]
        public void DeletePircejs(int id)
        {
            var data = _db.Pircejs.FirstOrDefault(s => s.Id == id);

            if (data != null)
            {
                _db.Pircejs.Remove(data);
                _db.SaveChanges();
            }
        }
        // Ieraksta labošana
        [HttpPut("{id}")]
        public void UpdatePircejs([FromBody] Pircejs pircejs, int id)
        {
            var existingPircejs = _db.Pircejs.FirstOrDefault(s => s.Id == id);

            if (existingPircejs != null)
            {
                existingPircejs.Id = pircejs.Id;
                existingPircejs.Name = pircejs.Name;
                existingPircejs.Surname = pircejs.Name;
            }
            _db.SaveChanges();
        }
    }
}
