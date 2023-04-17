using HairBook_Server_Side.Models.DAL;

namespace HairBook_Server_Side.Models
{
    public class HairColor
    {
        private int colorNum;
        private string colorName;

        public int ColorNum { get => colorNum; set => colorNum = value; }
        public string ColorName { get => colorName; set => colorName = value; }

        public int Insert()
        {
            DBServices dbs = new DBServices();
            return dbs.InsertHairColor(this);
        }

        public int InsertClientHairColor(string phoneNum, int colorNum)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertClientHairColor(phoneNum,colorNum);
        }
    }
}
