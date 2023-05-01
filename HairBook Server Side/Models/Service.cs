using HairBook_Server_Side.Models.DAL;

namespace HairBook_Server_Side.Models
{
    public class Service
    {
        private int treatmentNum;
        private string treatmentType;
        private double price;
        private double minPrice;
        private int treatmentDuration1;
        private int treatmentDuration2;
        private int treatmentDuration3;
        private int break1;
        private int break2;
        private int break3;


        public string TreatmentType { get => treatmentType; set => treatmentType = value; }
        public double Price { get => price; set => price = value; }
        public int TreatmentDuration1 { get => treatmentDuration1; set => treatmentDuration1 = value; }
        public double MinPrice { get => minPrice; set => minPrice = value; }
        public int TreatmentDuration2 { get => treatmentDuration2; set => treatmentDuration2 = value; }
        public int TreatmentDuration3 { get => treatmentDuration3; set => treatmentDuration3 = value; }
        public int Break1 { get => break1; set => break1 = value; }
        public int Break2 { get => break2; set => break2 = value; }
        public int Break3 { get => break3; set => break3 = value; }
        public int TreatmentNum { get => treatmentNum; set => treatmentNum = value; }

        public int Insert()
        {
            DBServices dbs = new DBServices();
            return dbs.InsertService(this);
        }

        public List<Service> Read()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadService();
        }

        public int UpdateService()
        {
            DBServices dbs = new DBServices();
            return dbs.UpdateService(this);
        }
    }
}
