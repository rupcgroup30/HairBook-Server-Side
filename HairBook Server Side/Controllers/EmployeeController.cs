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
        [HttpGet("GetAllEmployees")]
        public List<Employee> GetAllEmployees()
        {
            Employee emp = new Employee();
            return emp.ReadAllEmp();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("GetDatesByEmployee")]
        public Object GetDatesByEmployee(string EmpPhone)
        {
            Employee emp = new Employee();
            return emp.ReadDatesByEmployee(EmpPhone);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("GetByService")]
        public Object GetByService(int service)
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

        //// PUT api/<EmployeeController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{phoneNum}")]
        public int Delete(string phoneNum)
        {
            Employee emp = new Employee();
            return emp.DeleteEmployee(phoneNum);
        }
    }
}
