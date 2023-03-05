using HairBook_Server_Side.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HairBook_Server_Side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareKindController : ControllerBase
    {
        // GET: api/<Treatment>
        [HttpGet]
        public List<CareKind> Get()
        {
            CareKind careKind=new CareKind();
            return careKind.Read();
        }

        // GET api/<Treatment>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Treatment>
        [HttpPost]
        public int Post([FromBody] CareKind treatment)
        {
            return treatment.Insert();
        }

        // PUT api/<Treatment>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Treatment>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
