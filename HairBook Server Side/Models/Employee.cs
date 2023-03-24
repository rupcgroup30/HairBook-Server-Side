using HairBook_Server_Side.Models.DAL;
using Microsoft.VisualBasic;

namespace HairBook_Server_Side.Models
{
    public class Employee : User
    {
        private int employeeNum;
        private String password;
        private String empolyeeType;
        private DateTime startDate;
        public int EmployeeNum { get => employeeNum; set => employeeNum = value; }
        public string Password { get => password; set => password = value; }
        public string EmpolyeeType { get => empolyeeType; set => empolyeeType = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }

        public int Insert()
        {
            DBServices dbs = new DBServices();
            return dbs.InsertEmployee(this);
        }

        public Employee Read(string phoneNum, string password)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadEmployee(phoneNum,password);
        }

        public List<object> ReadByService(int service)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadByService(service);
        }
    }
}
