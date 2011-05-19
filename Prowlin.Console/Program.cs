using System;
using Args;


namespace Prowlin.Console
{
    [ArgsModel(SwitchDelimiter = "-")]
    public class ArgumentsObject
    {
        public string Application = string.Empty;
        public string Description = string.Empty;
        public string Event = string.Empty;
        public string Key = string.Empty;
        public string Priority = string.Empty;
        public string Url = string.Empty;
        public bool Help = false;
    }

    internal class Program
    {
        private static void Main(string[] args) {
            var p = new Program();
            p.Run(args);
        }

        private void Run(string[] args) {
            ArgumentsObject arguments = Configuration.Configure<ArgumentsObject>().CreateAndBind(args);

            if(arguments.Help) {
                ShowHelp();
                return;
            }

            if (string.IsNullOrEmpty(arguments.Key)) {
                System.Console.WriteLine("ApiKey requried");
                return;
            }

            if (string.IsNullOrEmpty(arguments.Event)) {
                System.Console.WriteLine("Event is required");
                return;
            }
  
            if (string.IsNullOrEmpty(arguments.Application)) {
                System.Console.WriteLine("Application is required");
                return;
            }

            var notification = new Notification
                                   {
                                       Application = arguments.Application,
                                       Description = arguments.Description,
                                       Event = arguments.Event,
                                       Url = arguments.Url
                                   };


            switch (arguments.Priority.ToLower()) {
                case "verylow":
                    notification.Priority = NotificationPriority.VeryLow;
                    break;
                case "moderate":
                    notification.Priority = NotificationPriority.Moderate;
                    break;
                case "high":
                    notification.Priority = NotificationPriority.High;
                    break;
                case "emergency":
                    notification.Priority = NotificationPriority.Emergency;
                    break;
                default:
                    notification.Priority = NotificationPriority.Normal;
                    break;
            }


            foreach (string s in arguments.Key.Split(new[] {',', ';'})) {
                notification.AddApiKey(s);
            }


            var prowlClient = new ProwlClient();
            int remaingMessages = prowlClient.SendNotification(notification);

            System.Console.WriteLine("Remaing number of messages: {0}", remaingMessages);
        }

        private void ShowHelp() {
            System.Console.WriteLine("Prowlin version {0}", "0.8.0.0" );
            System.Console.WriteLine("" );
            System.Console.WriteLine("USAGE:" );
            System.Console.WriteLine("\t\t> Prowlin -k one_apikey -e \"event X\" -a \"Application Y\"" );
            System.Console.WriteLine("\t\t> Prowlin -k apikey_one,apikey_two,... -e \"event X\" -a \"Application Y\"" );
            System.Console.WriteLine("\tOptions:" );
            System.Console.WriteLine("\t-k, -key\tAPIKEY(s)" );
            System.Console.WriteLine("" );
            System.Console.WriteLine("" );
            System.Console.WriteLine("" );
        }
    }
}