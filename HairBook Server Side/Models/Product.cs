using HairBook_Server_Side.Models.DAL;

namespace HairBook_Server_Side.Models
{
    public class Product
    {
        private string productName;
        private string description;
        private double price;
        private string seriesName;

        public string ProductName { get => productName; set => productName = value; }
        public string Description { get => description; set => description = value; }
        public double Price { get => price; set => price = value; }
        public string SeriesName { get => seriesName; set => seriesName = value; }

        public int Insert()
        {
            DBServices dbs = new DBServices();
            return dbs.InsertProduct(this);
        }

        public List<Product> Read()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadProducts();
        }
    }
}
