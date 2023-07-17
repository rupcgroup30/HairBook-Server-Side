using HairBook_Server_Side.Models;
using System.Threading;

namespace HairBook_Server_Side
{
    public class RepeatingService : BackgroundService
    {
        private ILogger<RepeatingService> _logger;
        private readonly CancellationTokenSource _cancellationTokenSource;
        public RepeatingService(ILogger<RepeatingService> logger)
        {
            _logger = logger;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                //await Task.Delay(TimeSpan.Zero);
                RepeatingOrderQueueReminder(cancellationToken);
                RepeatingQueueReminder(cancellationToken);
                RepeatingWaitingSuggestion(cancellationToken);
                RepeatingTommorowProductsOrders(cancellationToken);
            }
            catch (TaskCanceledException)
            {
                // Handle cancellation gracefully if needed
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the RepeatingService.");
            }
        }

        public async Task RepeatingQueueReminder(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Perform your desired function call here
                HairSalon hairSalon = new HairSalon();
                List<Object> hs = (List<Object>)hairSalon.ReadAllHairSalons();
                Queue queue = new Queue();
                foreach (var item in hs)
                {
                    int hairSalonId = Convert.ToInt32(item.GetType().GetProperty("Id").GetValue(item, null));
                    List<Object> phones = queue.QueueReminder(hairSalonId);
                    foreach (var item2 in phones)
                    {
                        string token = (item2.GetType().GetProperty("token").GetValue(item2, null)).ToString();
                        string KindCare = (item2.GetType().GetProperty("KindCare").GetValue(item2, null)).ToString();
                        string time = (item2.GetType().GetProperty("time").GetValue(item2, null)).ToString();
                        PushNotification pn = new PushNotification();
                        pn.QueueReminderPNAsync(token, KindCare, time);
                    }
                }
                Console.WriteLine("Function called at: " + DateTime.Now);

                // Delay for 24 hours until the next function call
                //await Task.Delay(TimeSpan.FromDays(1));
                DateTime now = DateTime.Now;
                DateTime desiredTime = new DateTime(now.Year, now.Month, now.Day, 17, 01, 0);

                TimeSpan difference = desiredTime.Subtract(now);
                double differenceInMilliseconds = difference.TotalMilliseconds;
                await Task.Delay(Convert.ToInt32(differenceInMilliseconds), cancellationToken);
            };
        }


        public async Task RepeatingTommorowProductsOrders(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Perform your desired function call here
                HairSalon hairSalon = new HairSalon();
                List<Object> hs = (List<Object>)hairSalon.ReadAllHairSalons();
                Product product=new Product();
                foreach (var item in hs)
                {
                    int hairSalonId = Convert.ToInt32(item.GetType().GetProperty("Id").GetValue(item, null));
                    List<Object> Orders = product.GetTommorowProductsOrders(hairSalonId);
                    foreach (var item2 in Orders)
                    {
                        string token = (item2.GetType().GetProperty("token").GetValue(item2, null)).ToString();
                        int ConfirmNum = Convert.ToInt32(item2.GetType().GetProperty("ConfirmNum").GetValue(item2, null));
                        string productName = (item2.GetType().GetProperty("productName").GetValue(item2, null)).ToString();
                        PushNotification pn = new PushNotification();
                        pn.TommorowProductsOrdersPNAsync(token, productName, ConfirmNum);
                    }
                }
                Console.WriteLine("Function called at: " + DateTime.Now);

                // Delay for 24 hours until the next function call
                //await Task.Delay(TimeSpan.FromDays(1));
                DateTime now = DateTime.Now;
                DateTime desiredTime = new DateTime(now.Year, now.Month, now.Day, 17, 03, 0);

                TimeSpan difference = desiredTime.Subtract(now);
                double differenceInMilliseconds = difference.TotalMilliseconds;
                await Task.Delay(Convert.ToInt32(differenceInMilliseconds), cancellationToken);
            };
        }


        public async Task RepeatingOrderQueueReminder(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Perform your desired function call here
                HairSalon hairSalon = new HairSalon();
                List<Object> hs = (List<Object>)hairSalon.ReadAllHairSalons();
                Queue queue = new Queue();
                foreach (var item in hs)
                {
                    int hairSalonId = Convert.ToInt32(item.GetType().GetProperty("Id").GetValue(item, null));
                    List<string> phones = queue.ReadPhonesToRemind(hairSalonId);
                    foreach (var item2 in phones)
                    {
                        PushNotification pn = new PushNotification();
                        pn.OrderQueueReminderPNAsync(item2);
                    }
                }
                Console.WriteLine("Function called at: " + DateTime.Now);

                // Delay for 24 hours until the next function call
                DateTime now = DateTime.Now;
                DateTime desiredTime = new DateTime(now.Year, now.Month, now.Day, 17, 00, 0);

                TimeSpan difference = desiredTime.Subtract(now);
                double differenceInMilliseconds = difference.TotalMilliseconds;
                await Task.Delay(Convert.ToInt32(differenceInMilliseconds), cancellationToken);
            };
        }

        public async Task RepeatingWaitingSuggestion(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Perform your desired function call here
                HairSalon hairSalon = new HairSalon();
                List<Object> hs = (List<Object>)hairSalon.ReadAllHairSalons();
                Queue queue = new Queue();
                foreach (var item in hs)
                {
                    int hairSalonId = Convert.ToInt32(item.GetType().GetProperty("Id").GetValue(item, null));
                    List<Object> Waiting = queue.createTender(hairSalonId);
                    foreach (var item2 in Waiting)
                    {
                        string Token = (item2.GetType().GetProperty("Token").GetValue(item2, null)).ToString();
                        string careKind = (item2.GetType().GetProperty("careKind").GetValue(item2, null)).ToString();
                        string Time = (item2.GetType().GetProperty("Time").GetValue(item2, null)).ToString();
                        double Price = Convert.ToDouble(item2.GetType().GetProperty("Price").GetValue(item2, null));
                        double minPrice = Convert.ToDouble(item2.GetType().GetProperty("minPrice").GetValue(item2, null));

                        PushNotification pn = new PushNotification();
                        pn.WaitingSuggestionPNAsync(Token, careKind, Time, Price, minPrice);
                    }
                }
                Console.WriteLine("Function called at: " + DateTime.Now);

                // Delay for 24 hours until the next function call
                //await Task.Delay(TimeSpan.FromDays(1));
                DateTime now = DateTime.Now;
                DateTime desiredTime = new DateTime(now.Year, now.Month, now.Day, 17, 02, 0);

                TimeSpan difference = desiredTime.Subtract(now);
                double differenceInMilliseconds = difference.TotalMilliseconds;
                await Task.Delay(Convert.ToInt32(differenceInMilliseconds), cancellationToken);
            };

        }



    }
}
