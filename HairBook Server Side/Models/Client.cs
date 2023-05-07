using HairBook_Server_Side.Models.DAL;

namespace HairBook_Server_Side.Models
{
    public class Client : User
    {

        private DateTime birthDate;
        private string gender;
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Gender { get => gender; set => gender = value; }

        public int Insert(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertClient(this, hairSalonId);
        }

        public Client Read(string phoneNum,int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadClient(phoneNum, hairSalonId);
        }

        public List<Client> ReadAllClients(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadAllClients(hairSalonId);
        }

        public int GetCode(string phoneNum, int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetCode(phoneNum, hairSalonId);
        }
    }
}
