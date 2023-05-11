using Microsoft.AspNetCore.Mvc;
using HairBook_Server_Side.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HairBook_Server_Side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        //// GET: api/<UserController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<UserController>/5
        [HttpGet("{phoneNum}/{hairSalonId}")]
        public Client Get(string phoneNum,int hairSalonId)
        {
            Client client= new Client();
            return client.Read(phoneNum, hairSalonId);
        }

        [HttpGet("ReadClientHairColor/{phoneNum}/{hairSalonId}")]
        public int ReadClientHairColor(string phoneNum, int hairSalonId)
        {
            Client client = new Client();
            return client.ReadClientHairColor(phoneNum, hairSalonId);
        }

        [HttpGet("GetAllClients/{hairSalonId}")]
        public List<Client> GetAllClients(int hairSalonId)
        {
            Client client = new Client();
            return client.ReadAllClients(hairSalonId);
        }

        // GET api/<UserController>/5
        [HttpGet("GetCode")]
        public int GetCode(string phoneNum,int hairSalonId)
        {
            Client client = new Client();
            return client.GetCode(phoneNum, hairSalonId);
        }


        // POST api/<UserController>
        [HttpPost]
        public int Post([FromBody] Client client, int hairSalonId)
        {
            return client.Insert(hairSalonId);
        }

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
