using HairBook_Server_Side.Models.DAL;
using Microsoft.AspNetCore.Identity;

namespace HairBook_Server_Side.Models
{
    public class Product
    {
        private int productNum;
        private string productName;
        private string description;
        private double price;
        private string seriesName;
        private string careKind;
        private int amount;
        private string image;

        public string ProductName { get => productName; set => productName = value; }
        public string Description { get => description; set => description = value; }
        public double Price { get => price; set => price = value; }
        public string SeriesName { get => seriesName; set => seriesName = value; }
        public string CareKind { get => careKind; set => careKind = value; }
        public int Amount { get => amount; set => amount = value; }
        public int ProductNum { get => productNum; set => productNum = value; }
        public string Image { get => image; set => image = value; }

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

        public int UpdateNOrder(int id,string phoneNum, int amount, DateTime date)
        {
            DBServices dbs = new DBServices();
            return dbs.UpdateProduct(id, phoneNum,amount,date);
        }
    }
}
