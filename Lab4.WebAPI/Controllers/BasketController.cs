using Lab2.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        // Db context
        private BasketDbContext _db;
        public BasketController()
        {
            _db = new BasketDbContext();
        }
        // saraksta iegušana
        [HttpGet]
        public Basket[] GetBaskets()
        {
            var data = _db.Baskets.ToArray();
            return data;
        }
        // Viena ieraksta atgriešanaa
        [HttpGet("{id}")]
        public Basket GetBasket(int id)
        {
            var data = _db.Baskets.FirstOrDefault(s => s.Id == id);
            return data;
        }
        //Ieraksta pievienošana
        [HttpPost]
        public void Post([FromBody] Basket basket)
        {
            _db.Baskets.Add(basket);
            _db.SaveChanges();
        }
        //ieraksta dzēšana
        [HttpDelete("{id}")]
        public void DeleteBasket(int id)
        {
            var data = _db.Baskets.FirstOrDefault(s => s.Id == id);

            if (data != null)
            {
                _db.Baskets.Remove(data);
                _db.SaveChanges();
            }
        }
        // Ieraksta labošana
        [HttpPut("{id}")]
        public void UpdateBasket([FromBody] Basket basket, int id)
        {
            var existingBasket = _db.Baskets.FirstOrDefault(s => s.Id == id);

            if (existingBasket != null)
            {
                existingBasket.Id = basket.Id;
                existingBasket.BasketNumber = basket.BasketNumber;
                existingBasket.BasketType = basket.BasketType;
                existingBasket.BasketSize = basket.BasketSize;
            }
            _db.SaveChanges();
        }
    }
}
