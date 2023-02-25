namespace HairBook_Server_Side.Models
{
    public class User
    {
        private string firstName;
        private string lastName;
        private string phoneNum;
        private string image;
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string Image { get => image; set => image = value; }

    }
}
