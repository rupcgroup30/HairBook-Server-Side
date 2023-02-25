using HairBook_Server_Side.Models.DAL;

namespace HairBook_Server_Side.Models
{
    public class Client : User
    {

        private string birthDate;
        private bool gender;
        public string BirthDate { get => birthDate; set => birthDate = value; }
        public bool Gender { get => gender; set => gender = value; }

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
