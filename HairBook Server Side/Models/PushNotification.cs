using HairBook_Server_Side.Models.DAL;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace HairBook_Server_Side.Models
{
    public class PushNotification
    {

        private Timer timer;
        private CancellationTokenSource cancellationTokenSource;

        //public void Start()
        //{
        //    DateTime now = DateTime.Now;
        //    DateTime desiredTime = new DateTime(now.Year, now.Month, now.Day, 17, 39, 0); // Change to your desired time

        //    TimeSpan timeUntilFirstExecution = desiredTime - now;
        //    if (timeUntilFirstExecution < TimeSpan.Zero)
        //    {
        //        // If the desired time has already passed today, schedule it for the next day
        //        timeUntilFirstExecution = timeUntilFirstExecution.Add(TimeSpan.FromDays(1));
        //    }

        //    // Create the timer with the desired time interval
        //    Timer timer = new Timer(YourMethod, null, timeUntilFirstExecution, TimeSpan.FromDays(1));

        //    // Wait for the timer to continue running
        //    Console.WriteLine("Press Enter to stop the program.");
        //    Console.ReadLine();

        //    // Dispose the timer to stop it
        //    timer.Dispose();
        //}

        //static void YourMethod(object state)
        //{
        //    // Your method logic goes here
        //    Console.WriteLine("Your method is executed daily.");
        //}


        public async Task ChangeWorkTimePNAsync(string token)
        {

            string expoPushEndpoint = "https://exp.host/--/api/v2/push/send";
            string expoPushToken = token;

            string payload = @"
            {
                ""to"": ""<expo_push_token>"",
                ""title"": ""שעות המספרה עודכנו"",
                ""body"": ""שים לב ששעות המספרה עודכנו, התעדכן באפליקציה בשעות"",
                ""sound"": ""default"",
                ""data"": {
                    ""customData"": ""Additional data for the notification""
                }
            }";
            payload = payload.Replace("<expo_push_token>", expoPushToken);

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(expoPushEndpoint, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Push notification response: " + responseContent);
            }

        }

        public async Task GetCodePNAsync(string token,int code)
        {

            string expoPushEndpoint = "https://exp.host/--/api/v2/push/send";
            string expoPushToken = token;
            string expoCode = code.ToString();

            string payload = @"
            {
                ""to"": ""<expo_push_token>"",
                ""title"": ""HairBook Code"",
                ""body"": ""Login code: <code>"",
                ""sound"": ""default"",
                ""data"": {
                    ""customData"": ""Additional data for the notification""
                }
            }";
            payload = payload.Replace("<expo_push_token>", expoPushToken);
            payload = payload.Replace("<code>", expoCode);

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(expoPushEndpoint, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Push notification response: " + responseContent);
            }

        }

        public async Task DeleteQueuePNAsync(string token,DateTime date, string treatment, string time)
        {

            string expoPushEndpoint = "https://exp.host/--/api/v2/push/send";
            string expoPushToken = token;

            string payload = @"
            {
                ""to"": ""<expo_push_token>"",
                ""title"": ""תורך התבטל"",
                ""body"": ""התור שלך בתאריך <date> בשעה <time> ל<treatment> התבטל"",
                ""sound"": ""default"",
                ""data"": {
                    ""customData"": ""Additional data for the notification""
                }
            }";
            payload = payload.Replace("<expo_push_token>", expoPushToken);
            payload = payload.Replace("<date>", date.ToString("dd/MM/yyyy"));
            payload = payload.Replace("<time>", time.Substring(0, time.Length - 3));
            payload = payload.Replace("<treatment>", treatment);

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(expoPushEndpoint, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Push notification response: " + responseContent);
            }

        }


        public async Task EnteredFromWaitingPNAsync(string token, DateTime date, string treatment, string time)
        {

            string expoPushEndpoint = "https://exp.host/--/api/v2/push/send";
            string expoPushToken = token;

            string payload = @"
            {
                ""to"": ""<expo_push_token>"",
                ""title"": ""נכנסת מרשימת המתנה"",
                ""body"": ""נקבע לך תור בתאריך <date>, בשעה <time> ל<treatment>"",
                ""sound"": ""default"",
                ""data"": {
                    ""customData"": ""Additional data for the notification""
                }
            }";
            payload = payload.Replace("<expo_push_token>", expoPushToken);
            payload = payload.Replace("<date>", date.ToString("dd/MM/yyyy"));
            payload = payload.Replace("<time>", time.Substring(0, time.Length - 3));
            payload = payload.Replace("<treatment>", treatment);

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(expoPushEndpoint, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Push notification response: " + responseContent);
            }

        }


        public async Task OrderQueuePNAsync(string token, DateTime date, string treatment, string time)
        {

            string expoPushEndpoint = "https://exp.host/--/api/v2/push/send";
            string expoPushToken = token;

            string payload = @"
            {
                ""to"": ""<expo_push_token>"",
                ""title"": ""נקבע לך תור"",
                ""body"": ""נקבע לך תור בתאריך <date>, בשעה <time> ל<treatment>"",
                ""sound"": ""default"",
                ""data"": {
                    ""customData"": ""Additional data for the notification""
                }
            }";
            payload = payload.Replace("<expo_push_token>", expoPushToken);
            payload = payload.Replace("<date>", date.ToString("dd/MM/yyyy"));
            payload = payload.Replace("<time>", time);
            payload = payload.Replace("<treatment>", treatment);

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(expoPushEndpoint, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Push notification response: " + responseContent);
            }

        }


        public async Task SendMessagePNAsync(int hairSalonId, string Subject, string Message)
        {
            List<Client> clientsTokens = new List<Client>();
            DBServices dbs = new DBServices();
            clientsTokens = dbs.ReadAllClients(hairSalonId);
            foreach (Client clientToken in clientsTokens)
            {
                string expoPushEndpoint = "https://exp.host/--/api/v2/push/send";
                string expoPushToken = clientToken.Token;

                string payload = @"
            {
                ""to"": ""<expo_push_token>"",
                ""title"": ""<Subject>"",
                ""body"": ""<Message>"",
                ""sound"": ""default"",
                ""data"": {
                    ""customData"": ""Additional data for the notification""
                }
            }";
                payload = payload.Replace("<expo_push_token>", expoPushToken);
                payload = payload.Replace("<Subject>",Subject);
                payload = payload.Replace("<Message>", Message);

                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(expoPushEndpoint, content);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Push notification response: " + responseContent);
                }
            }

        }



        public async Task QueueReminderPNAsync(string token, string treatment, string time)
        {

            string expoPushEndpoint = "https://exp.host/--/api/v2/push/send";
            string expoPushToken = token;

            string payload = @"
            {
                ""to"": ""<expo_push_token>"",
                ""title"": ""*תזכורת*"",
                ""body"": ""נקבע לך תור למחר, בשעה <time> ל<treatment>"",
                ""sound"": ""default"",
                ""data"": {
                    ""customData"": ""Additional data for the notification""
                }
            }";
            payload = payload.Replace("<expo_push_token>", expoPushToken);
            payload = payload.Replace("<time>", time.Substring(0, time.Length - 3));
            payload = payload.Replace("<treatment>", treatment);

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(expoPushEndpoint, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Push notification response: " + responseContent);
            }

        }


        public async Task TommorowProductsOrdersPNAsync(string token, string productName, int ConfirmNum)
        {

            string expoPushEndpoint = "https://exp.host/--/api/v2/push/send";
            string expoPushToken = token;

            string payload = @"
            {
                ""to"": ""<expo_push_token>"",
                ""title"": ""תזכורת איסוף מוצר"",
                ""body"": ""המוצר <productName> מחכה לך מחר במספרה, מספר האישור הוא: <ConfirmNum>"",
                ""sound"": ""default"",
                ""data"": {
                    ""customData"": ""Additional data for the notification""
                }
            }";
            payload = payload.Replace("<expo_push_token>", expoPushToken);
            payload = payload.Replace("<productName>", productName);
            payload = payload.Replace("<ConfirmNum>", ConfirmNum.ToString());

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(expoPushEndpoint, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Push notification response: " + responseContent);
            }

        }



        public async Task OrderQueueReminderPNAsync(string token)
        {

            string expoPushEndpoint = "https://exp.host/--/api/v2/push/send";
            string expoPushToken = token;

            string payload = @"
            {
                ""to"": ""<expo_push_token>"",
                ""title"": ""*תזכורת*"",
                ""body"": ""הרבה זמן לא קבעת תור למספרה! היכנס לאפליקציה וקבע לך תור עוד היום"",
                ""sound"": ""default"",
                ""data"": {
                    ""customData"": ""Additional data for the notification""
                }
            }";
            payload = payload.Replace("<expo_push_token>", expoPushToken);

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(expoPushEndpoint, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Push notification response: " + responseContent);
            }

        }


        public async Task WaitingSuggestionPNAsync(string token, string treatment, string time,double price,double minPrice)
        {

            string expoPushEndpoint = "https://exp.host/--/api/v2/push/send";
            string expoPushToken = token;

            string payload = @"
            {
                ""to"": ""<expo_push_token>"",
                ""title"": ""יש לנו הצעה בשביך!"",
                ""body"": ""ראינו שנרשמת לרשימת המתנה מחר ל<treatment>, מצאנו לך תור ל<treatment> בשעה <time> במחיר מוזל של <minPrice> במקום <price>! מהר להזמין לפני שיתפס!"",
                ""sound"": ""default"",
                ""data"": {
                    ""customData"": ""Additional data for the notification""
                }
            }";
            payload = payload.Replace("<expo_push_token>", expoPushToken);
            payload = payload.Replace("<price>", price.ToString());
            payload = payload.Replace("<minPrice>", minPrice.ToString());
            payload = payload.Replace("<time>", time.Substring(0, time.Length - 3));
            payload = payload.Replace("<treatment>", treatment);

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(expoPushEndpoint, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Push notification response: " + responseContent);
            }

        }


    }
}
