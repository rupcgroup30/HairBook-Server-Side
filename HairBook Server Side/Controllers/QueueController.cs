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
        [HttpGet("GetQueuesByClient")]
        public Object GetQueuesByClient(int hairSalonId, string phoneNum, int flag)
        {
            Queue queue = new Queue();
            return queue.GetQueuesByClient(hairSalonId, phoneNum,flag);
        }

        // GET: api/<QueueController>
        [HttpGet("GetAllQueues")]
        public Object GetAllQueues(int hairSalonId, int flag)
        {
            Queue queue = new Queue();
            return queue.GetAllQueues(hairSalonId, flag);
        }


        // GET: api/<QueueController>
        [HttpGet("GetAvailableTimes")]
        public List<string> GetAvailableTimes(int serviceNum,string phoneNum, DateTime Date, int hairSalonId)
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

        [HttpGet("/createTender/{hairSalonId}")]
        public List<Object> createTender(int hairSalonId)
        {
            Queue queue = new Queue();
            return queue.createTender(hairSalonId);
        }


        // GET api/<QueueController>/5
        [HttpGet("/TommorowQueueReminder")]
        public List<Object> QueueReminder(int hairSalonId)
        {
            Queue queue = new Queue();
            return queue.QueueReminder(hairSalonId);
        }


        // POST api/<QueueController>
        [HttpPost("OrderQueue")]
        public int OrderQueue([FromBody] Queue queue,int hairSalonId,int flag)
        {
            return queue.OrderQueue(hairSalonId,flag);
        }

        [HttpPost("/PostIntoWaiting")]
        public int PostIntoWaiting([FromBody] Queue queue,int hairSalonId)
        {
            return queue.InsertToWaitingList(queue, hairSalonId);
        }

        // POST api/<QueueController>
        [HttpPost("MoveQueue")]
        public Object MoveQueue([FromBody] Queue queue, int hairSalonId,int flag)
        {
            return queue.MoveQueue(hairSalonId,flag);
        }

        //// PUT api/<QueueController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<QueueController>/5
        [HttpDelete("DeleteQueue")]
        public Object DeleteQueue(int queueNum,int hairSalonId,int flag)
        {
            Queue queue = new Queue();
            return queue.DeleteQueue(queueNum, hairSalonId,flag);
        }
    }
}
