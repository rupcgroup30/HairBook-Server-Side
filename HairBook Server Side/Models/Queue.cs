using HairBook_Server_Side.Models.DAL;

namespace HairBook_Server_Side.Models
{
    public class Queue
    {
        private int queueNum;
        private DateTime date;
        private string time;
        private string empPhone;
        private string clientphone;
        private int serviceNum;


        private static List<Queue> QueuesList = new List<Queue>();

        public DateTime Date { get => date; set => date = value; }
        public string Time { get => time; set => time = value; }
        public string EmpPhone { get => empPhone; set => empPhone = value; }
        public string Clientphone { get => clientphone; set => clientphone = value; }
        public int ServiceNum { get => serviceNum; set => serviceNum = value; }

        public int InsertToWaitingList(Queue queue)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertToWaitingList(this);
        }

        public List<TimeSpan> ReadAvailableTimes(int serviceNum, string phoneNum, DateTime Date)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadAvailableTimes(serviceNum, phoneNum, Date);
        }

        public int OrderQueue()
        {
            DBServices dbs = new DBServices();
            return dbs.OrderQueue(this);
        }

        public List<string> ReadPhonesToRemind()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadPhonesToRemind();
        }

        public List<string> QueueReminder()
        {
            DBServices dbs = new DBServices();
            return dbs.QueueReminder();
        }

        public int DeleteQueue(int queueNum)
        {
            DBServices dbs = new DBServices();
            return dbs.DeleteQueue(queueNum);
        }

        


    }
}
