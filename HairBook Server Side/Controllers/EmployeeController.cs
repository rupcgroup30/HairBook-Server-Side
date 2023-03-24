using HairBook_Server_Side.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HairBook_Server_Side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // GET: api/<EmployeeController>
        [HttpGet]
        public Employee Get(string phoneNum, string password)
        {
            Employee emp = new Employee();
            return emp.Read(phoneNum, password);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{service}")]
        public List<object> GetByService(int service)
        {
            Employee emp = new Employee();
            return emp.ReadByService(service);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public int Post([FromBody] Employee employee)
        {
            return employee.Insert();
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
