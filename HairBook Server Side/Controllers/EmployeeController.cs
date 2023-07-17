using HairBook_Server_Side.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HairBook_Server_Side.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // GET: api/<EmployeeController>
        [HttpGet]
        public Employee Get(string phoneNum, string password,int hairSalonId)
        {
            Employee emp = new Employee();
            return emp.Read(phoneNum, password, hairSalonId);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("GetAllEmployees")]
        public List<Employee> GetAllEmployees(int hairSalonId)
        {
            Employee emp = new Employee();
            return emp.ReadAllEmp(hairSalonId);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("GetDatesByEmployee")]
        public Object GetDatesByEmployee(string EmpPhone,int hairSalonId)
        {
            Employee emp = new Employee();
            return emp.ReadDatesByEmployee(EmpPhone, hairSalonId);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("GetByService")]
        public Object GetByService(int service,int hairSalonId)
        {
            Employee emp = new Employee();
            return emp.ReadByService(service, hairSalonId);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public int Post([FromBody] Employee employee,int hairSalonId)
        {
            return employee.Insert(hairSalonId);
        }

        [HttpGet("GetEmployeesVacations/{hairSalonId}")]
        public Object ReadEmployeesVacations(int hairSalonId)
        {
            Employee emp = new Employee();
            return emp.ReadEmployeesVacations( hairSalonId);
        }

        // POST api/<EmployeeController>
        [HttpPost("SetEmpSpecialize")]
        public int SetEmpSpecialize(int hairSalonId, string phoneNum, string numOfCare)
        {
            Employee emp = new Employee();
            numOfCare = numOfCare.Trim('\"');
            return emp.SetEmpSpecialize(hairSalonId, phoneNum, JsonConvert.DeserializeObject<List<string>>(numOfCare));
        }

        [HttpPost("InsertEmployeeVacation")]
        public int InsertEmployeeVacation(int hairSalonId, string phoneNum, DateTime startDate, DateTime endDate, string fromHour, string toHour)
        {
            Employee emp = new Employee();
            return emp.InsertEmployeeVacation(hairSalonId, phoneNum, startDate, endDate, fromHour, toHour);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("UpdateEmployee")]
        public int UpdateEmployee( string phoneNum,string password, string type,string isActive,int hairSalonId)
        {
            Employee emp = new Employee();
            return emp.UpdateEmployee(phoneNum,password,type,isActive, hairSalonId);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("UpdateEmployeeRank")]
        public int UpdateEmployeeRank(double rank, int employeeNum)
        {
            Employee emp = new Employee();
            return emp.UpdateEmployeeRank(rank,employeeNum);
        }

        //// DELETE api/<EmployeeController>/5
        //[HttpDelete("{phoneNum}")]
        //public int Delete(string phoneNum)
        //{
        //}
    }
}
