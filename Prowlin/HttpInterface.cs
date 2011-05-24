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
      public static string GetApiKey = "retrieve/apikey";

  }

    public class HttpInterface : IHttpInterface
    {
        private readonly string BASE_URL = " https://api.prowlapp.com/publicapi/";


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

        

        public VerificationResult SendVerification(IVerification verification) {
            RequestBuilderHelper requestBuilderHelper = new RequestBuilderHelper();

            string http = GetHttpMethod(Method.Verify);

            Dictionary<string, string> parameters = requestBuilderHelper.BuildDictionaryForVerification(verification);

            HttpWebRequest httpWebRequest = BuildRequest(BASE_URL, Method.Verify, parameters);

            WebResponse response = default(WebResponse);

            try {
                response = httpWebRequest.GetResponse();
            }
            catch (TimeoutException e) {
                throw new TimeoutException("Timeout delivery uncertain");
            }

            XDocument resultDocument = XDocument.Load(response.GetResponseStream());
            int remaingNoOfMessages = 0;
            string timestamp = string.Empty;
            string returnCode = string.Empty;
            string errMsg = string.Empty;

            if (resultDocument != null)
            {
                if (resultDocument.Descendants("error").Count() > 0)
                {
                    errMsg = resultDocument.Descendants("error").ElementAt(0).Attribute("code").Value;
                }

                int.TryParse(resultDocument.Descendants("success").ElementAt(0).Attribute("remaining").Value,
                             out remaingNoOfMessages);
                timestamp = resultDocument.Descendants("success").ElementAt(0).Attribute("resetdate").Value;

                returnCode = resultDocument.Descendants("success").ElementAt(0).Attribute("code").Value;
            }

            return new VerificationResult()
                       {
                           ResultCode = returnCode,
                           RemainingMessageCount = remaingNoOfMessages,
                           TimeStamp = timestamp,
                           ErrorMessage = errMsg
                       };
        }

        public RetrieveTokenResult RetrieveToken(RetrieveToken retrieveToken) {
            RequestBuilderHelper requestBuilderHelper = new RequestBuilderHelper();

            Dictionary<string, string> parameters = requestBuilderHelper.BuildDictionaryForRetreiveToken(retrieveToken);

            HttpWebRequest httpWebRequest = BuildRequest(BASE_URL, Method.GetToken, parameters);
            WebResponse response = default(WebResponse);

            try
            {
                response = httpWebRequest.GetResponse();
            }
            catch (TimeoutException e)
            {
                throw new TimeoutException("Timeout delivery uncertain");
            }

            XDocument resultDocument = XDocument.Load(response.GetResponseStream());
            int remaingNoOfMessages = 0;
            string timestamp = string.Empty;
            string returnCode = string.Empty;
            string errMsg = string.Empty;
            string token = string.Empty;
            string url = string.Empty;

            if (resultDocument != null)
            {
                if (resultDocument.Descendants("error").Count() > 0)
                {
                    errMsg = resultDocument.Descendants("error").ElementAt(0).Attribute("code").Value;
                }

                int.TryParse(resultDocument.Descendants("success").ElementAt(0).Attribute("remaining").Value,
                             out remaingNoOfMessages);
                timestamp = resultDocument.Descendants("success").ElementAt(0).Attribute("resetdate").Value;
                returnCode = resultDocument.Descendants("success").ElementAt(0).Attribute("code").Value;
                token = resultDocument.Descendants("retrieve").ElementAt(0).Attribute("token").Value;
                url = resultDocument.Descendants("retrieve").ElementAt(0).Attribute("url").Value;

            }

            return new RetrieveTokenResult()
            {
                ResultCode = returnCode,
                RemainingMessageCount = remaingNoOfMessages,
                TimeStamp = timestamp,
                Token = token,
                Url = url
            };
        
        
        }

        public RetrieveApikeyResult RetrieveApikey(RetrieveApikey retrieveApikey) {
            RequestBuilderHelper requestBuilderHelper = new RequestBuilderHelper();
            Dictionary<string, string> parameters = requestBuilderHelper.BuildDictionaryForRetreiveApiKey(retrieveApikey);
            WebResponse response = default(WebResponse);

            HttpWebRequest request = BuildRequest(BASE_URL, Method.GetApiKey, parameters);

            try {
                response = request.GetResponse();
            }
            catch (TimeoutException) {
                throw new TimeoutException("Timeout delivery uncertain");
            }

            XDocument resultDoc = XDocument.Load(response.GetResponseStream());

            string newKey = resultDoc.Descendants("retrieve").First().Attribute("apikey").Value;

            return new RetrieveApikeyResult(){ApiKey = newKey};

        }



        public HttpWebRequest BuildRequest(string baseUrl, string method, Dictionary<string, string> parameters) {

            RequestBuilderHelper requestBuilderHelper = new RequestBuilderHelper();

            Uri uri = new Uri(requestBuilderHelper.BuildRequestUrl(BASE_URL, method, parameters));

            HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
            request.Method = GetHttpMethod(method);
            //request.Timeout = 10*1000; //10 seconds
            request.ContentType = "application/x-www-form-urlencoded";

            return request;
        }

        private string GetHttpMethod(string method) {
            string httpVerb;
            if(method == Method.Add) {
                httpVerb = "POST";
            }
            else {
                httpVerb = "GET";
            }
            return httpVerb;
        }
    }
}
