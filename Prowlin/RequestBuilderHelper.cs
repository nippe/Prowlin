using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Prowlin
{
    public class RequestBuilderHelper
    {
        public Dictionary<string, string> BuildDictionaryForNotificataion(INotification notification)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("apikey", notification.Keys);
            parameters.Add("application", notification.Application);
            parameters.Add("description", notification.Description);
            parameters.Add("event", notification.Event);
            parameters.Add("priority", Convert.ToInt32(notification.Priority).ToString());
            parameters.Add("url", notification.Url);
            return parameters;
        }


        public string BuildParameterString(Dictionary<string, string> parameters)
        {
            IList<string> s = new List<string>(parameters.Count);
            foreach (var parameter in parameters)
            {
                s.Add(string.Format("{0}={1}", parameter.Key,
                    HttpUtility.UrlEncode(parameter.Value)));
            }

            return string.Join("&", s.ToArray());
        }

        

        public string BuildRequestUrl(string baseUrl, string method, Dictionary<string, string> parameters) {
            StringBuilder sb = new StringBuilder();

            sb.Append(baseUrl);

            if(baseUrl.EndsWith("/") == false) {
                sb.Append("/");
            }
            sb.Append(method);

            if(parameters != null && parameters.Count > 0) {
                sb.Append("?");
                sb.Append(this.BuildParameterString(parameters));
            }

            return sb.ToString();
        }

        public Dictionary<string, string> BuildDictionaryForVerification(IVerification verification) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("apikey", verification.ApiKey);
            parameters.Add("application", verification.ProviderKey);
            return parameters;
        }

        public Dictionary<string, string> BuildDictionaryForRetreiveToken(RetrieveToken retrieveToken) {
            Dictionary<string,string > parameters = new Dictionary<string, string>(1);
            parameters.Add("providerkey", retrieveToken.ProviderKey);
            return parameters;
        }
    }
}
