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
        private double rank;
        private bool isActive;
        public int EmployeeNum { get => employeeNum; set => employeeNum = value; }
        public string Password { get => password; set => password = value; }
        public string EmpolyeeType { get => empolyeeType; set => empolyeeType = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public double Rank { get => rank; set => rank = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public int Insert(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertEmployee(this, hairSalonId);
        }

        public int InsertEmployeeVacation(int hairSalonId, string phoneNum, DateTime startDate, DateTime endDate, string fromHour, string toHour)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertEmployeeVacation(hairSalonId, phoneNum, startDate, endDate, fromHour, toHour);
        }

        public Object ReadEmployeesVacations(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadEmployeesVacations(hairSalonId);
        }

        public Employee Read(string phoneNum, string password, int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadEmployee(phoneNum, password, hairSalonId);
        }

        public int UpdateEmployee(string phoneNum, string password, string type, string isActive, int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.UpdateEmployee(phoneNum, password, type, isActive, hairSalonId);
        }

        public int SetEmpSpecialize(int hairSalonId, string phoneNum, List<string> numOfCare)
        {
            int sum=0;
            DBServices dbs = new DBServices();
            foreach (string i in numOfCare)
            {
                sum+= dbs.SetEmpSpecialize(hairSalonId, phoneNum, Convert.ToInt32(i));
            }
            return sum;
        }

        public int UpdateEmployeeRank(double rank, int employeeNum)
        {
            DBServices dbs = new DBServices();
            return dbs.UpdateEmployeeRank(rank, employeeNum);
        }

        public List<Employee> ReadAllEmp(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadAllEmployees(hairSalonId);
        }

        public Object ReadByService(int service, int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadByService(service, hairSalonId);

        }

        public Object ReadDatesByEmployee(string EmpPhone, int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadDatesByEmployee(EmpPhone, hairSalonId);
        }
    }

}
