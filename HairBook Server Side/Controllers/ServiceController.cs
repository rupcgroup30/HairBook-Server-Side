using HairBook_Server_Side.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HairBook_Server_Side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        // GET: api/<Service>
        [HttpGet]
        public List<Service> Get()
        {
            Service service=new Service();
            return service.Read();
        }

        // GET api/<Service>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Service>
        [HttpPost]
        public int Post([FromBody] Service service)
        {
            return service.Insert();
        }

        // PUT api/<Service>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Service>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
