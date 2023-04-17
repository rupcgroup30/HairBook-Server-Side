using Microsoft.AspNetCore.Mvc;
using HairBook_Server_Side.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HairBook_Server_Side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/<ProductController>
        [HttpGet]
        public List<Product> Get()
        {
            Product product = new Product();
            return product.Read();
        }

        // GET api/<ProductController>/5
        [HttpGet("{seriesName}")]
        public List<Product> Get(string seriesName)
        {
            Product product = new Product();
            return product.ReadRelatedProducts(seriesName);
        }

        // POST api/<ProductController>
        [HttpPost]
        public int Post([FromBody] Product product)
        {
            return product.Insert();
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        public int Put(int id, string phoneNum, int amount, DateTime date)
        {
            Product product = new Product();
            return product.UpdateNOrder(id, phoneNum, amount, date);
        }

        [HttpPut("UpdateProdAmount")]
        public int Put(int id, int amount)
        {
            Product product = new Product();
            return product.UpdateProductAmount(id, amount);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
