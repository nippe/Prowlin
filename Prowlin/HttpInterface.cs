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
        
            RequestBuilderHelper requestBuilderHelper = new RequestBuilderHelper();

            Dictionary<string, string> parameters = requestBuilderHelper.BuildDictionaryForNotificataion(notification);

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



       


        public HttpWebRequest BuildRequest(string baseUrl, string method, Dictionary<string, string> parameters) {

            RequestBuilderHelper requestBuilderHelper = new RequestBuilderHelper();
            string httpVerb;

            if(method == Method.Add) {
                httpVerb = "POST";
            }
            else {
                httpVerb = "GET";
            }


            Uri uri = new Uri(requestBuilderHelper.BuildRequestUrl(BASE_URL, method, parameters));

            HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
            request.Method = httpVerb;
            //request.Timeout = 10*1000; //10 seconds
            request.ContentType = "application/x-www-form-urlencoded";

            return request;
        }


    }
}
