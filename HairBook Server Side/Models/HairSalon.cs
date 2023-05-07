﻿using HairBook_Server_Side.Models.DAL;

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

        public int Insert(int hairSalonId,int colorNum, string colorName)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertHairColor(colorNum,colorName,hairSalonId);
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

        public int InsertHairSalonWorkTime(int hairSalonId, string fromHour, string toHour, string day)
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
