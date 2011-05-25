using System;
using Args;
using Prowlin.Interfaces;


namespace Prowlin.Console
{
    [ArgsModel(SwitchDelimiter = "-")]
    public class ArgumentsObject
    {
        public string   Application     = string.Empty;
        public string   Description     = string.Empty;
        public string   Event           = string.Empty;
        public string   Key             = string.Empty;
        public string   Priority        = string.Empty;
        public string   Url             = string.Empty;
        public bool     Help            = false;
        public bool     Verify          = false;
        public string   ProviderKey     = string.Empty;
        public bool     RetrieveToken   = false;
        public bool     NewKey          = false;
        public string   Token           = string.Empty;
    }

    internal class Program
    {
        private static void Main(string[] args) {
            var p = new Program();
            p.Run(args);
        }

        private void Run(string[] args) {
            ArgumentsObject arguments = Configuration.Configure<ArgumentsObject>().CreateAndBind(args);

            if(arguments.Help || args.Length == 0) {
                ShowHelp();
                return;
            }
            
            var prowlClient = new ProwlClient();
            
            if (arguments.RetrieveToken)
            {
                RetrieveToken retrieveToken = new RetrieveToken();
                retrieveToken.ProviderKey = arguments.ProviderKey;

                RetrieveTokenResult result = prowlClient.RetreiveToken(retrieveToken);
                System.Console.WriteLine("Token retreived\nToken: {0}\nUrl: {1}",
                    result.Token,
                    result.Url);

                return;
            }

            if (arguments.NewKey) {
                if(string.IsNullOrEmpty(arguments.Token) || string.IsNullOrEmpty(arguments.ProviderKey)) {
                    System.Console.WriteLine("ProviderKey and Token required for this operation.");
                    return;
                }
                RetrieveApikey retrieveApikey = new RetrieveApikey();
                retrieveApikey.Token = arguments.Token;
                retrieveApikey.ProviderKey = arguments.ProviderKey;

                RetrieveApikeyResult retrieveApikeyResult = prowlClient.RetrieveApikey(retrieveApikey);
                System.Console.WriteLine("New APIKEY: {0}");
                return;
            }

            if (string.IsNullOrEmpty(arguments.Key)) {
                System.Console.WriteLine("ApiKey requried");
                return;
            }

            if(arguments.Verify) {
                IVerification v = new Verification();
                v.ApiKey = arguments.Key;
                v.ProviderKey = arguments.ProviderKey;
                System.Console.WriteLine("Sending verification...");
                VerificationResult verificationResult = prowlClient.SendVerification(v);
                System.Console.WriteLine("Verification {3}\n\tVerification returned: {0} \n\tNumber of messages left to send: {1}\n\tReset UNIX timestamp: {2}",
                    verificationResult.ResultCode,
                    verificationResult.RemainingMessageCount.ToString(),
                    verificationResult.TimeStamp,
                    verificationResult.ResultCode == "200" ? "OK" : "NOT OK");
            }
            else {
                if (string.IsNullOrEmpty(arguments.Event))
                {
                    System.Console.WriteLine("Event is required");
                    return;
                }

                if (string.IsNullOrEmpty(arguments.Application))
                {
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


                switch (arguments.Priority.ToLower())
                {
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

                foreach (string s in arguments.Key.Split(new[] { ',', ';' }))
                {
                    notification.AddApiKey(s);
                }

                NotificationResult notificationResult= prowlClient.SendNotification(notification);

                System.Console.WriteLine("Remaing number of messages: {0}", notificationResult.RemainingMessageCount.ToString());
            }
        }

        private void ShowHelp() {
            System.Console.WriteLine("");
            System.Console.WriteLine("Prowlin version {0}", "0.8.0.0");
            System.Console.WriteLine("" );
            System.Console.WriteLine("USAGE:" );
            System.Console.WriteLine("\t> Prowlin -k one_apikey -e \"event X\" -a \"Application Y\"" );
            System.Console.WriteLine("\t> Prowlin -k apikey_one,apikey_two,... -e \"event X\" -a \"Application Y\"" );
            System.Console.WriteLine("\t> Prowlin -v -k apikey" );
            System.Console.WriteLine("");
            System.Console.WriteLine("   Options:");
            System.Console.WriteLine("\t-k, -key\t\tAPIKEY(s)\tProwl API key, one or many separated by commas" );
            System.Console.WriteLine("\t-a, -application\tAPPLICATION\tApplication" );
            System.Console.WriteLine("\t-e, -event\t\tEVENT\t\tEvent" );
            System.Console.WriteLine("\t-d, -description\tDESCRIPTION\tDescription" );
            System.Console.WriteLine("\t-p, -priority\t\tPRIORITY\tPriority - VERYLOW, MODERATE, NORMAL(default), HIGH, EMERGENCY" );
            System.Console.WriteLine("\t-u, -url\t\tURL\t\tUrl");
            System.Console.WriteLine("\t-h, -help\t\tHELP\t\tThis screen");
            System.Console.WriteLine("\t-v, -verify\t\tVERIFIY\t\tVerification of key used in combination with -k (APIKEY) ");
            System.Console.WriteLine("\t\t\t\t\t\tand (optional) -p (PROVIDER KEY)");
            System.Console.WriteLine("\t-r, -retrievetoken\tRETRIVE/TOKEN\tGet a registration token for use in retrieve/apikey and \n\t\t\t\t\t\tthe associated URL for the user to approve the request. Use together with -providerkey");
            System.Console.WriteLine("\t-pro, -providerkey\tPROVIDER KEY\tProviderKey  to use in conjunction with -r (retrieve/token)");
            System.Console.WriteLine("\t-n, -newkey\t\tGET APIKEY\tGet new api key  to use in conjunction with -t (token) and\n\t\t\t\t\t\t-pro (providerkey)");
            System.Console.WriteLine("");
            System.Console.WriteLine("" );
        }
    }
}