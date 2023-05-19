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

        public DateTime Date { get => date; set => date = value; }
        public string Time { get => time; set => time = value; }
        public string EmpPhone { get => empPhone; set => empPhone = value; }
        public string Clientphone { get => clientphone; set => clientphone = value; }
        public int ServiceNum { get => serviceNum; set => serviceNum = value; }
        public int QueueNum { get => queueNum; set => queueNum = value; }

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

        public List<TimeSpan> ReadAvailableTimes(int serviceNum, string phoneNum, DateTime Date,int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadAvailableTimes(serviceNum, phoneNum, Date, hairSalonId);
        }

        public int OrderQueue(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.OrderQueue(this, hairSalonId);
        }
        public Object MoveQueue(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            int res= dbs.OrderQueue(this, hairSalonId);
            if (res !=0)
                return dbs.DeleteQueue(this.QueueNum, hairSalonId);
            else
                return 0;
        }

        public List<string> ReadPhonesToRemind(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.ReadPhonesToRemind(hairSalonId);
        }

        public List<string> QueueReminder(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.QueueReminder(hairSalonId);
        }


        public Object createTender(int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetTodayWaiting(hairSalonId);
           // return dbs.createTender(waiting);
        }

        public Object DeleteQueue(int queueNum,int hairSalonId)
        {
            DBServices dbs = new DBServices();
            return dbs.DeleteQueue(queueNum, hairSalonId);
        }

        


    }
}
