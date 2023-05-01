using HairBook_Server_Side.Models.DAL;

namespace HairBook_Server_Side.Models
{
    public class HairSalon
    {

        private string salonName;
        private string salonPhoneNum;
        private string address;
        private string city;
        private string facebook;
        private string instagram;

        public string SalonName { get => salonName; set => salonName = value; }
        public string SalonPhoneNum { get => salonPhoneNum; set => salonPhoneNum = value; }
        public string Address { get => address; set => address = value; }
        public string City { get => city; set => city = value; }
        public string Facebook { get => facebook; set => facebook = value; }
        public string Instagram { get => instagram; set => instagram = value; }

        public int Insert(int colorNum, string colorName)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertHairColor(colorNum,colorName);
        }

        public int InsertClientHairColor(string phoneNum, int colorNum)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertClientHairColor(phoneNum,colorNum);
        }

        public int InsertHairSalonInfo(string managerPhone, HairSalon hairSalonInfo)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertHairSalonInfo(managerPhone,hairSalonInfo);
        }

        public HairSalon ReadHairSalonInfo()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadHairSalonInfo();
        }

        public int InsertHairSalonWorkTime(string managerPhone, string fromHour, string toHour, string day)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertHairSalonWorkTime(managerPhone, fromHour, toHour, day);
        }

        public Object ReadHairSalonWorkTime()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadHairSalonWorkTime();
        }
    }
}
