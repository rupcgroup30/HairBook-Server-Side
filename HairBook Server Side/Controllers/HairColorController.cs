using Microsoft.AspNetCore.Mvc;
using HairBook_Server_Side.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HairBook_Server_Side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HairColorController : ControllerBase
    {
        // GET: api/<HairColorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<HairColorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HairColorController>
        [HttpPost]
        public int Post([FromBody] HairColor hairColor)
        {
            return hairColor.Insert();
        }

                // POST api/<HairColorController>
        [HttpPost("{phoneNum}/{colorNum}")]
        public int Post([FromBody] string phoneNum, int colorNum)
        {
            HairColor hairColor = new HairColor();
            return hairColor.InsertClientHairColor(phoneNum,colorNum);
        }

        // PUT api/<HairColorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HairColorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
