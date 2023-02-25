namespace HairBook_Server_Side.Models
{
    public class Queue
    {
        private int queueNum;
        private string date;
        private string time;

        private static List<Queue> QueuesList = new List<Queue>();

        public string Date { get => date; set => date = value; }
        public string Time { get => time; set => time = value; }


    }
}
