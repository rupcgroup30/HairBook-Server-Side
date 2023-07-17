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
        private bool isActive;
        public string ProductName { get => productName; set => productName = value; }
        public string Description { get => description; set => description = value; }
        public double Price { get => price; set => price = value; }
        public string SeriesName { get => seriesName; set => seriesName = value; }
        public string CareKind { get => careKind; set => careKind = value; }
        public int Amount { get => amount; set => amount = value; }
        public int ProductNum { get => productNum; set => productNum = value; }
        public string Image { get => image; set => image = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        public int Insert(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertProduct(this, hairSalonId);
        }

        public List<Product> Read(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadProducts( hairSalonId);
        }

        public List<Product> ReadRelatedProducts(string seriesName,int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadRelatedProducts(seriesName, hairSalonId);
        }

        public int UpdateNOrder(int id,string phoneNum, int amount, DateTime date,int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.UpdateNOrdetProduct(id, phoneNum,amount,date, hairSalonId);
        }

        public int UpdateProduct(int id, int amount,float price, bool isActive,int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.UpdateProduct(id, amount,price,isActive, hairSalonId);
        }

        public Object ReadOrderedProdByClient(int hairSalonId, string phoneNum, int flag)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadOrderedProdByClient( hairSalonId,phoneNum,flag);
        }

        public List<Object> GetTommorowProductsOrders(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetTommorowProductsOrders(hairSalonId);
        }
    }
}
