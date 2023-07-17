using HairBook_Server_Side.Models.DAL;

namespace HairBook_Server_Side.Models
{
    public class Client : User
    {
        private int hairSalonId;
        private DateTime birthDate;
        private string gender;
        private string token;
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Token { get => token; set => token = value; }
        public int HairSalonId { get => hairSalonId; set => hairSalonId = value; }

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

        public int ReadClientHairColor(string phoneNum, int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadClientHairColor(phoneNum, hairSalonId);
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
