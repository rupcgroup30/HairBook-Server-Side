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
        public List<Service> Get(int hairSalonId)
        {
            Service service=new Service();
            return service.Read(hairSalonId);
        }

        //// GET api/<Service>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<Service>
        [HttpPost]
        public int Post([FromBody] Service service,int hairSalonId)
        {
            return service.Insert(hairSalonId);
        }

        // PUT api/<Service>/5
        [HttpPut("UpdateService")]
        public int UpdateService([FromBody] Service service,int hairSalonId)
        {
            return service.UpdateService(hairSalonId);
        }

        //// DELETE api/<Service>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
