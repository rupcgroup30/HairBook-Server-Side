using HairBook_Server_Side.Models.DAL;
using System.Data.SqlClient;
using Twilio.Http;
using Twilio.TwiML.Messaging;
using static System.Net.Mime.MediaTypeNames;

namespace HairBook_Server_Side.Models
{
    public class HairSalon
    {
        private int _id;
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
        public int Id { get => _id; set => _id = value; }

        public int Insert(int hairSalonId,int colorNum, string colorName)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertHairColor(colorNum,colorName,hairSalonId);
        }

        public int UpdateHairColor(int colorNum, bool isActive, int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.UpdateHairColor(colorNum, isActive, hairSalonId);
        }

        public int InsertHairSalonImages(string image, int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertHairSalonImages(image,hairSalonId);
        }

        public List<string> ReadHairSalonImages(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadHairSalonImages(hairSalonId);
        }

        public Object ReadAllHairSalons()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadAllHairSalon();
        }

        public int InsertClientHairColor(string phoneNum, int colorNum, int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertClientHairColor(phoneNum, colorNum, hairSalonId);
        }

        public int InsertHairSalonInfo(int hairSalonId, HairSalon hairSalonInfo)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertHairSalonInfo(hairSalonId,hairSalonInfo);
        }

        public HairSalon ReadHairSalonInfo(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadHairSalonInfo(hairSalonId);
        }

        public int InsertHairSalonWorkTime(int hairSalonId, string fromHour, string toHour, int day)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertHairSalonWorkTime(hairSalonId, fromHour, toHour, day);
        }

        public Object ReadHairSalonWorkTime(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadHairSalonWorkTime(hairSalonId);
        }

        public Object ReadHairColors(bool flag, int HairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadHairColors(flag, HairSalonId);
        }
    }
}
