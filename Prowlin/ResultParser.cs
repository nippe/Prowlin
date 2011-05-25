using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Prowlin
{
    public class ResultParser
    {
        public static void ParseResultBase(XDocument resultDocument, ResultBase resultBase) {
            if (resultDocument != null) {
                if (resultDocument.Descendants("error").Count() > 0) {
                    string errMsg = resultDocument.Descendants("error").ElementAt(0).Attribute("code").Value;
                    throw new ApplicationException(errMsg);
                }

                resultBase.ResultCode = resultDocument.Descendants("success").ElementAt(0).Attribute("code").Value;

                int remaingCount= -1;
                int.TryParse(resultDocument.Descendants("success").ElementAt(0).Attribute("remaining").Value,
                             out remaingCount);
                resultBase.RemainingMessageCount = remaingCount;

                resultBase.TimeStamp = resultDocument.Descendants("success").ElementAt(0).Attribute("resetdate").Value;
            }
        }

        public static void ParseTokenResult(XDocument resultDocument, RetrieveTokenResult retrieveTokenResult) {
            retrieveTokenResult.Token = resultDocument.Descendants("retrieve").ElementAt(0).Attribute("token").Value;
            retrieveTokenResult.Url = resultDocument.Descendants("retrieve").ElementAt(0).Attribute("url").Value;
        }

        public static void ParseApikeyResult(XDocument xDoc, RetrieveApikeyResult retrieveApikeyResult) {
            retrieveApikeyResult.ApiKey = xDoc.Descendants("retrieve").First().Attribute("apikey").Value;
            
        }
    }
}
