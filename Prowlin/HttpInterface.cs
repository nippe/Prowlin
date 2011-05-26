using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Prowlin;
using Prowlin.Interfaces;

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
        RequestBuilderHelper _requestBuilderHelper = new RequestBuilderHelper();

        public NotificationResult SendNotification(INotification notification) {

            Dictionary<string, string> parameters = _requestBuilderHelper.BuildDictionaryForNotificataion(notification);

            NotificationResult notificationResult = new NotificationResult();
            XDocument resultDocument = GetResultDocument(parameters, Method.Add, notificationResult);

            return notificationResult;
        }


        public VerificationResult SendVerification(IVerification verification) {
            Dictionary<string, string> parameters = _requestBuilderHelper.BuildDictionaryForVerification(verification);

            VerificationResult verificationResult = new VerificationResult();
            XDocument resultDocument = GetResultDocument(parameters, Method.Verify, verificationResult);
            return verificationResult;
        }


        public RetrieveTokenResult RetrieveToken(RetrieveToken retrieveToken) {
            Dictionary<string, string> parameters = _requestBuilderHelper.BuildDictionaryForRetreiveToken(retrieveToken);

            RetrieveTokenResult retrieveTokenResult = new RetrieveTokenResult();
            XDocument resultDocument = GetResultDocument(parameters, Method.GetToken, retrieveTokenResult);
            ResultParser.ParseTokenResult(resultDocument, retrieveTokenResult);

            return retrieveTokenResult;
        }


        public RetrieveApikeyResult RetrieveApikey(RetrieveApikey retrieveApikey) {
            Dictionary<string, string> parameters = _requestBuilderHelper.BuildDictionaryForRetreiveApiKey(retrieveApikey);

            RetrieveApikeyResult retrieveApikeyResult = new RetrieveApikeyResult();
            XDocument resultDoc = GetResultDocument(parameters, Method.GetApiKey, retrieveApikeyResult);
            ResultParser.ParseApikeyResult(resultDoc, retrieveApikeyResult);

            return retrieveApikeyResult;
        }


        private XDocument GetResultDocument(Dictionary<string, string> parameters, string method, ResultBase resultBase) {
            HttpWebRequest httpWebRequest = BuildRequest(BASE_URL, method, parameters);

            WebResponse response = default(WebResponse);

            try {
                response = httpWebRequest.GetResponse();
            }
            catch (TimeoutException e) {
                throw new TimeoutException("Timeout delivery uncertain");
            }
            XDocument resultDocument = XDocument.Load(response.GetResponseStream());
            ResultParser.ParseResultBase(resultDocument, resultBase);

            return resultDocument;
        }

        public HttpWebRequest BuildRequest(string baseUrl, string method, Dictionary<string, string> parameters) {

            Uri uri = new Uri(_requestBuilderHelper.BuildRequestUrl(BASE_URL, method, parameters));
            HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
            request.Method = GetHttpMethod(method);
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
