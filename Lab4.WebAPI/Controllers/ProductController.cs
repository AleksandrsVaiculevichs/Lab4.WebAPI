using Lab2.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Db context
        private BasketDbContext _db;
        public ProductController()
        {
            _db = new BasketDbContext();
        }




        // saraksta iegušana
        [HttpGet]
        public Product[] GetProducts()
        {
            var data = _db.Products.ToArray();
            return data;
        }
        // Viena ieraksta atgriešanaa
        [HttpGet("{id}")]
        public Product GetProducts(int id)
        {
            var data = _db.Products.FirstOrDefault(s => s.Id == id);
            return data;
        }
        //Ieraksta pievienošana
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }
        //ieraksta dzēšana
        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
            var data = _db.Products.FirstOrDefault(s => s.Id == id);

            if (data != null)
            {
                _db.Products.Remove(data);
                _db.SaveChanges();
            }
        }
        // Ieraksta labošana
        [HttpPut("{id}")]
        public void UpdateProduct([FromBody] Product product, int id)
        {
            var existingProduct = _db.Products.FirstOrDefault(s => s.Id == id);

            if (existingProduct != null)
            {
                existingProduct.Id = id;
                existingProduct.ProductPrice = product.ProductPrice;
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductCategory = product.ProductCategory;
            }
            _db.SaveChanges();
        }
    }
}
