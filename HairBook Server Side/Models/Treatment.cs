using HairBook_Server_Side.Models.DAL;

namespace HairBook_Server_Side.Models
{
    public class Treatment
    {
        private string treatmentType;
        private double price;
        private string treatmentDuration1;
        private string treatmentDuration2;
        private string treatmentDuration3;
        private string break1;
        private string break2;
        private string break3;


        private double minPrice;
        public string TreatmentType { get => treatmentType; set => treatmentType = value; }
        public double Price { get => price; set => price = value; }
        public string TreatmentDuration1 { get => treatmentDuration1; set => treatmentDuration1 = value; }
        public double MinPrice { get => minPrice; set => minPrice = value; }
        public string TreatmentDuration2 { get => treatmentDuration2; set => treatmentDuration2 = value; }
        public string TreatmentDuration3 { get => treatmentDuration3; set => treatmentDuration3 = value; }
        public string Break1 { get => break1; set => break1 = value; }
        public string Break2 { get => break2; set => break2 = value; }
        public string Break3 { get => break3; set => break3 = value; }

        public int Insert()
        {
            DBServices dbs = new DBServices();
            return dbs.InsertTreatment(this);
        }
    }
}
