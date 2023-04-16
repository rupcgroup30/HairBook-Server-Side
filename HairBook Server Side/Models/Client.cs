using HairBook_Server_Side.Models.DAL;

namespace HairBook_Server_Side.Models
{
    public class Client : User
    {

        private DateTime birthDate;
        private string gender;
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Gender { get => gender; set => gender = value; }

        public int Insert()
        {
            DBServices dbs = new DBServices();
            return dbs.InsertClient(this);
        }

        public Client Read(string phoneNum)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadClient(phoneNum);
        }

        public int GetCode(string phoneNum)
        {
            DBServices dbs = new DBServices();
            return dbs.GetCode(phoneNum);
        }
    }
}
