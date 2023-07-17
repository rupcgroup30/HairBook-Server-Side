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

        [HttpGet("GetAllHairSalons")]
        public Object GetAllHairSalon()
        {
            HairSalon hairSalon = new HairSalon();
            return hairSalon.ReadAllHairSalons();
        }

        // GET api/<HairColorController>/5
        [HttpGet("GetHairSalonWorkTime")]
        public Object ReadHairSalonWorkTime(int hairSalonId)
        {
            HairSalon hairSalon = new HairSalon();
            return hairSalon.ReadHairSalonWorkTime(hairSalonId);
        }


        [HttpGet("GetHairColors/{flag}/{HairSalonId}")]
        public Object ReadHairColors(bool flag, int HairSalonId)
        {
            {
                HairSalon hairSalon = new HairSalon();
                return hairSalon.ReadHairColors(flag,HairSalonId);
            }
        }

        [HttpGet("GetHairSalonImages")]
        public List<string> GetHairSalonImages(int hairSalonId)
        {
            HairSalon hairSalon = new HairSalon();
            return hairSalon.ReadHairSalonImages(hairSalonId);
        }


        // POST api/<HairColorController>
        [HttpPost("PostSalonInfo")]
        public int PostSalonInfo(int hairSalonId, HairSalon hairSalonInfo)
        {
            return hairSalonInfo.InsertHairSalonInfo(hairSalonId, hairSalonInfo);
        }


        // POST api/<HairColorController>
        [HttpPost("PostSalonMessage")]
        public void PostSalonMessage(int hairSalonId, string subject,string message)
        {
            PushNotification ps = new PushNotification();
            ps.SendMessagePNAsync(hairSalonId, subject, message);
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
        public int InsertHairSalonWorkTime(int hairSalonId, string fromHour, string toHour, int day)
        {
            HairSalon hairSalon = new HairSalon();
            return hairSalon.InsertHairSalonWorkTime(hairSalonId, fromHour, toHour, day);
        }

        // POST api/<HairColorController>
        [HttpPost("PostHairSalonImages")]
        public int PostHairSalonImages(string image, int hairSalonId)
        {
            HairSalon hairSalon = new HairSalon();
            return hairSalon.InsertHairSalonImages(image,hairSalonId);
        }



        // PUT api/<HairColorController>/5
        [HttpPut("UpdateHairColor")]
        public int UpdateHairColor(int colorNum, bool isActive, int hairSalonId)
        {
            HairSalon hairColor = new HairSalon();
            return hairColor.UpdateHairColor(colorNum, isActive, hairSalonId);
        }

        //// DELETE api/<HairColorController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
