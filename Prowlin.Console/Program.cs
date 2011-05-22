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
        public bool Verify = false;
        public string ProviderKey = string.Empty;
    }

    internal class Program
    {
        private static void Main(string[] args) {
            var p = new Program();
            p.Run(args);
        }

        private void Run(string[] args) {
            ArgumentsObject arguments = Configuration.Configure<ArgumentsObject>().CreateAndBind(args);

            var prowlClient = new ProwlClient();


            if(arguments.Help || args.Length == 0) {
                ShowHelp();
                return;
            }

            if (string.IsNullOrEmpty(arguments.Key)) {
                System.Console.WriteLine("ApiKey requried");
                return;
            }

            if(!arguments.Verify) {
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


                int remaingMessages = prowlClient.SendNotification(notification);

                System.Console.WriteLine("Remaing number of messages: {0}", remaingMessages);
            }
            else {
                IVerification v = new Verification();
                v.ApiKey = arguments.Key;
                v.ProviderKey = arguments.ProviderKey;
                System.Console.WriteLine("Sending verification");
                VerificationResult verificationResult = prowlClient.SendVerification(v);
                System.Console.WriteLine("Verification {3}\nVerification returned: {0}, no of messages left to send: {1}, reset time: {2}", 
                    verificationResult.ResultCode,
                    verificationResult.RemainingMessageCount.ToString(),
                    verificationResult.TimeStamp,
                    verificationResult.ResultCode == "200"?"OK":"NOT OK");
            }
        }

        private void ShowHelp() {
            System.Console.WriteLine("");
            System.Console.WriteLine("Prowlin version {0}", "0.8.0.0");
            System.Console.WriteLine("" );
            System.Console.WriteLine("USAGE:" );
            System.Console.WriteLine("\t> Prowlin -k one_apikey -e \"event X\" -a \"Application Y\"" );
            System.Console.WriteLine("\t> Prowlin -k apikey_one,apikey_two,... -e \"event X\" -a \"Application Y\"" );
            System.Console.WriteLine("");
            System.Console.WriteLine("   Options:");
            System.Console.WriteLine("\t-k, -key\t\tAPIKEY(s)\tProwl API key, one or many separated by commas" );
            System.Console.WriteLine("\t-a, -application\tAPPLICATION\tApplication" );
            System.Console.WriteLine("\t-e, -event\t\tEVENT\t\tEvent" );
            System.Console.WriteLine("\t-d, -description\tDESCRIPTION\tDescription" );
            System.Console.WriteLine("\t-p, -priority\t\tPRIORITY\tPriority - VERYLOW, MODERATE, NORMAL(default), HIGH, EMERGENCY" );
            System.Console.WriteLine("\t-u, -url\t\tURL\t\tUrl");
            System.Console.WriteLine("\t-h, -help\t\tHELP\t\tThis screen");
            System.Console.WriteLine("");
            System.Console.WriteLine("" );
            System.Console.WriteLine("" );
        }
    }
}