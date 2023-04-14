using HairBook_Server_Side.Models.DAL;

namespace HairBook_Server_Side.Models
{
    public class Client : User
    {

        private DateTime birthDate;
        private string gender;
        private int code;
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Gender { get => gender; set => gender = value; }
        public int Code { get => code; set => code = value; }

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
    }
}
