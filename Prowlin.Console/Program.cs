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
            notification.AddApiKey("589a2d241e6ea26a11c994af835012eb3230f39f");

            ProwlClient prowlClient = new ProwlClient();
            prowlClient.SendNotification(notification);
        }
    }
}
