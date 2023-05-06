using Microsoft.AspNetCore.Mvc;
using HairBook_Server_Side.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HairBook_Server_Side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HairSalonController : ControllerBase
    {
        // GET: api/<HairColorController>
        [HttpGet("GetHairSalonInfo")]
        public HairSalon Get(int hairSalonId)
        {
            HairSalon hairSalon = new HairSalon();
            return hairSalon.ReadHairSalonInfo(hairSalonId);
        }

        // GET api/<HairColorController>/5
        [HttpGet("GetHairSalonWorkTime")]
        public Object ReadHairSalonWorkTime(int hairSalonId)
        {
            HairSalon hairSalon = new HairSalon();
            return hairSalon.ReadHairSalonWorkTime(hairSalonId);
        }

        // POST api/<HairColorController>
        [HttpPost("PostSalonInfo")]
        public int PostSalonInfo(int hairSalonId, HairSalon hairSalonInfo)
        {
            return hairSalonInfo.InsertHairSalonInfo(hairSalonId, hairSalonInfo);
        }

        // POST api/<HairColorController>
        [HttpPost("PostHairColor")]
        public int PostHairColor( int colorNum, string colorName, int hairSalonId)
        {
            HairSalon hairColor = new HairSalon();
            return hairColor.Insert(hairSalonId, colorNum,colorName);
        }

                // POST api/<HairColorController>
        [HttpPost("{phoneNum}/{colorNum}")]
        public int PostClientHairColor( string phoneNum, int colorNum, int hairSalonId)
        {
            HairSalon hairColor = new HairSalon();
            return hairColor.InsertClientHairColor(phoneNum,colorNum, hairSalonId);
        }

        // POST api/<HairColorController>
        [HttpPost("PostWorkTime")]
        public int InsertHairSalonWorkTime(int hairSalonId, string fromHour, string toHour, string day)
        {
            HairSalon hairSalon = new HairSalon();
            return hairSalon.InsertHairSalonWorkTime(hairSalonId, fromHour, toHour, day);
        }



        //// PUT api/<HairColorController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<HairColorController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
