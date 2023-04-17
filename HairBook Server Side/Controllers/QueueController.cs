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
        [HttpGet]
        public List<string> Get(int serviceNum,string phoneNum, DateTime Date)
        {
            Queue queue= new Queue();
            return queue.ReadAvailableTimes(serviceNum, phoneNum, Date);
        }

        // GET api/<QueueController>/5
        [HttpGet("/GetOrderQueueReminder")]
        public List<string> GetPhonesToRemind()
        {
            Queue queue = new Queue();
            return queue.ReadPhonesToRemind();
        }

        // GET api/<QueueController>/5
        [HttpGet("/TommorowQueueReminder")]
        public List<string> QueueReminder()
        {
            Queue queue = new Queue();
            return queue.QueueReminder();
        }
        

        // POST api/<QueueController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //return queue.InsertToWaitingList(queue);
        }

        [HttpPost("/Queue")]
        public int PostIntoWaiting([FromBody] Queue queue)
        {
            return queue.InsertToWaitingList(queue);
        }

        // PUT api/<QueueController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<QueueController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
