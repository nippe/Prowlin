using System;
using Prowlin;

namespace Prowlin.Console
{
    class Program
    {
        static void Main(string[] args) {

            Program p = new Program();
            p.Run(args);

        }

        private void Run(string[] args) {
            INotification notification = new Prowlin.Notification()
                                             {
                                                 Application = "Prowlin.Console",
                                                 Description = "Testing",
                                                 Event = "Some Event",
                                                 Priority = NotificationPriority.High,
                                                 Url = "http://www.nnihlen.com/blog"
                                             };
            notification.AddApiKey("<your-apikey(s)-goes here>");

            ProwlClient prowlClient = new ProwlClient();
            int remaingMessages = prowlClient.SendNotification(notification);

            System.Console.WriteLine("Remaing number of messages: {0}", remaingMessages.ToString());
        }
    }
}
