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
        private string token;

        public DateTime Date { get => date; set => date = value; }
        public string Time { get => time; set => time = value; }
        public string EmpPhone { get => empPhone; set => empPhone = value; }
        public string Clientphone { get => clientphone; set => clientphone = value; }
        public int ServiceNum { get => serviceNum; set => serviceNum = value; }
        public int QueueNum { get => queueNum; set => queueNum = value; }
        public string Token { get => token; set => token = value; }

        public Object GetQueuesByClient(int hairSalonId, string phoneNum,int flag)
        {
            DBServices dbs = new DBServices();
            return dbs.GetQueuesByClient(hairSalonId, phoneNum,flag);
        }

        public Object GetAllQueues(int hairSalonId, int flag)
        {
            DBServices dbs = new DBServices();
            return dbs.GetAllQueues(hairSalonId, flag);
        }

        public int InsertToWaitingList(Queue queue,int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.InsertToWaitingList(this, hairSalonId);
        }

        public List<string> ReadAvailableTimes(int serviceNum, string phoneNum, DateTime Date,int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadAvailableTimes(serviceNum, phoneNum, Date, hairSalonId);
        }

        public int OrderQueue(int hairSalonId,int flag)
        {
            DBServices dbs = new DBServices();
            return dbs.OrderQueue(this, hairSalonId,flag);
        }
        public Object MoveQueue(int hairSalonId,int flag)
        {
            DBServices dbs = new DBServices();
            int res= dbs.OrderQueue(this, hairSalonId,1);
            if (res !=0)
                return dbs.DeleteQueue(this.QueueNum, hairSalonId,flag);
            else
                return 0;
        }

        public List<string> ReadPhonesToRemind(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadPhonesToRemind(hairSalonId);
        }

        public List<Object> QueueReminder(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.QueueReminder(hairSalonId);
        }


        public List<Object> createTender(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetTodayWaiting(hairSalonId);
           // return dbs.createTender(waiting);
        }

        public Object DeleteQueue(int queueNum,int hairSalonId,int flag)
        {
            DBServices dbs = new DBServices();
            return dbs.DeleteQueue(queueNum, hairSalonId,flag);
        }

        


    }
}
