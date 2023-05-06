using HairBook_Server_Side.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HairBook_Server_Side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        // GET: api/<QueueController>
        [HttpGet("GetAvailableTimes")]
        public List<TimeSpan> GetAvailableTimes(int serviceNum,string phoneNum, DateTime Date, int hairSalonId)
        {
            Queue queue= new Queue();
            return queue.ReadAvailableTimes(serviceNum, phoneNum, Date, hairSalonId);
        }

        // GET api/<QueueController>/5
        [HttpGet("/GetOrderQueueReminder")]
        public List<string> GetPhonesToRemind(int hairSalonId)
        {
            Queue queue = new Queue();
            return queue.ReadPhonesToRemind(hairSalonId);
        }

        // GET api/<QueueController>/5
        [HttpGet("/TommorowQueueReminder")]
        public List<string> QueueReminder(int hairSalonId)
        {
            Queue queue = new Queue();
            return queue.QueueReminder(hairSalonId);
        }


        // POST api/<QueueController>
        [HttpPost("OrderQueue")]
        public int OrderQueue([FromBody] Queue queue,int hairSalonId)
        {
            return queue.OrderQueue(hairSalonId);
        }

        [HttpPost("/PostIntoWaiting")]
        public int PostIntoWaiting([FromBody] Queue queue,int hairSalonId)
        {
            return queue.InsertToWaitingList(queue, hairSalonId);
        }

        //// PUT api/<QueueController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<QueueController>/5
        [HttpDelete("DeleteQueue")]
        public Object DeleteQueue(int queueNum,int hairSalonId)
        {
            Queue queue = new Queue();
            return queue.DeleteQueue(queueNum, hairSalonId);
        }
    }
}
