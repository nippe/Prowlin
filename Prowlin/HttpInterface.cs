using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Prowlin;

namespace Prowlin
{

  public struct Method
  {
      public static string Add = "add";
      public static string Verify   = "verify";
      public static string GetToken = "retrieve/token";
      public static string GetApiKey = "addretrieve/apikey";

  }

    public class HttpInterface : IHttpInterface
    {
        private readonly string BASE_URL = " https://api.prowlapp.com/publicapi/";
        //private readonly string BASE_URL = " https://prowl.weks.net/publicapi/";

        public int SendNotification(INotification notification) {

            Dictionary<string, string > parameters = new Dictionary<string, string>();
            parameters.Add("apikey", notification.Keys);
            parameters.Add("application", notification.Application);
            parameters.Add("description", notification.Description);
            parameters.Add("event", notification.Event);
            parameters.Add("priority", Convert.ToInt32(notification.Priority).ToString());
            parameters.Add("url", notification.Url);

            HttpWebRequest httpWebRequest = BuildRequest(BASE_URL, Method.Add, parameters);

            WebResponse response = default(WebResponse);

            try {
                response = httpWebRequest.GetResponse();
            }
            catch (TimeoutException e) {
                throw new TimeoutException("Timeout delivery uncertain");
            }
            XDocument resultDocument = XDocument.Load(response.GetResponseStream());
            int remaingNoOfMessages = 0;
            
            if (resultDocument != null) {
                if(resultDocument.Descendants("error").Count() > 0 ) {
                    string errMsg = resultDocument.Descendants("error").ElementAt(0).Attribute("code").Value;
                    throw new ApplicationException(errMsg);
                }

                int.TryParse(resultDocument.Descendants("success").ElementAt(0).Attribute("remaining").Value,
                             out remaingNoOfMessages);

            }


            return remaingNoOfMessages;
        }

        public void SendVerification() {
            
        }



        public string BuildParameterString(Dictionary<string, string> parameters) {
            IList<string> s = new List<string>(parameters.Count);
            foreach (var parameter in parameters) {
               s.Add( string.Format("{0}={1}", parameter.Key, 
                   HttpUtility.UrlEncode( parameter.Value)) );
            }

            return string.Join("&", s.ToArray());
        }


        public HttpWebRequest BuildRequest(string baseUrl, string method, Dictionary<string, string> parameters) {

            string httpVerb;

            if(method == Method.Add) {
                httpVerb = "POST";
            }
            else {
                httpVerb = "GET";
            }


            Uri uri = new Uri(BuildRequestUrl(BASE_URL, method, parameters));

            HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
            request.Method = httpVerb;
            //request.Timeout = 10*1000; //10 seconds
            request.ContentType = "application/x-www-form-urlencoded";

            return request;
        }



        public string BuildRequestUrl(string baseUrl, string method, Dictionary<string, string> parameters) {
            StringBuilder sb = new StringBuilder();

            sb.Append(baseUrl);

            if(baseUrl.EndsWith("/") == false) {
                sb.Append("/");
            }
            sb.Append(method);

            if(parameters != null) {
                sb.Append("?");
                sb.Append(this.BuildParameterString(parameters));
            }

            return sb.ToString();
        }
    }
}
