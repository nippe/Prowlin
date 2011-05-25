using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace Prowlin.UnitTests
{
    public class ResultParserTests
    {
        private static readonly string _successfulResult = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                                           "<prowl>" +
                                                           "    <success code=\"200\" remaining=\"988\" resetdate=\"1450936800\" />" +
                                                           "</prowl>";

        private readonly string _successfulTokenResult = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                                           "<prowl>" +
                                                           "    <success code=\"200\" remaining=\"988\" resetdate=\"1450936800\" />" +
                                                           "    <retrieve token=\"1234567890123456789012345678901234567890\" url=\"http://www.nnihlen.com/blog\" />" +
                                                           "</prowl>";


        private readonly string _successfulApiKeyResult = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                                           "<prowl>" +
                                                           "    <success code=\"200\" remaining=\"988\" resetdate=\"1450936800\" />" +
                                                           "    <retrieve apikey=\"0987654321098765432109876543210987654321\" />" +
                                                           "</prowl>";

        private string _errorResult = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                      "<prowl>" +
                                      "	<error code=\"409\">NOT APPROVED</error>" +
                                      "</prowl>";


        [Fact]
        public void ParseResultBase_should_parse_ResultCode_200_correctly() {
            XDocument xDoc = XDocument.Parse(_successfulResult);
            NotificationResult notificationResult = new NotificationResult();
            ResultParser.ParseResultBase(xDoc, notificationResult);

            Assert.Equal("200", notificationResult.ResultCode);
        }  
        
        [Fact]
        public void ParseResultBase_should_parse_RemainingCount_988_correctly() {
            XDocument xDoc = XDocument.Parse(_successfulResult);
            NotificationResult notificationResult = new NotificationResult();
            ResultParser.ParseResultBase(xDoc, notificationResult);

            Assert.Equal(988, notificationResult.RemainingMessageCount);
        }


        [Fact]
        public void ParseResultBase_should_parse_TimeStamp_1450936800_correctly()
        {
            XDocument xDoc = XDocument.Parse(_successfulResult);
            NotificationResult notificationResult = new NotificationResult();
            ResultParser.ParseResultBase(xDoc, notificationResult);

            Assert.Equal("1450936800", notificationResult.TimeStamp);
        }

        [Fact]
        public void ParseResultBase_should_parse_ErrorMessage_as_null_when_success()
        {
            XDocument xDoc = XDocument.Parse(_successfulResult);
            NotificationResult notificationResult = new NotificationResult();
            ResultParser.ParseResultBase(xDoc, notificationResult);

            Assert.Null(notificationResult.ErrorMessage);
        }


        [Fact]
        public void ParseTokenResult_should_parse_Token_correctly_when_success()
        {
            XDocument xDoc = XDocument.Parse(_successfulTokenResult);
            RetrieveTokenResult notificationResult = new RetrieveTokenResult();
            ResultParser.ParseTokenResult(xDoc, notificationResult);

            Assert.Equal("1234567890123456789012345678901234567890", notificationResult.Token);
        }


        [Fact]
        public void ParseTokenResult_should_parse_Url_correctly_when_success()
        {
            XDocument xDoc = XDocument.Parse(_successfulTokenResult);
            RetrieveTokenResult notificationResult = new RetrieveTokenResult();
            ResultParser.ParseTokenResult(xDoc, notificationResult);

            Assert.Equal("http://www.nnihlen.com/blog", notificationResult.Url);
        }



        [Fact]
        public void ParseRetrieveApikeyResult_should_parse_Apikey_correctly_when_success()
        {
            XDocument xDoc = XDocument.Parse(_successfulApiKeyResult);
            RetrieveApikeyResult retrieveApikeyResult = new RetrieveApikeyResult();
            ResultParser.ParseApikeyResult(xDoc, retrieveApikeyResult);

            Assert.Equal("0987654321098765432109876543210987654321", retrieveApikeyResult.ApiKey);
        }


        [Fact]
        public void ParseResultBase_should_throw_ApplicationException_if_error_in_response() {
            XDocument xDoc = XDocument.Parse(_errorResult);
            RetrieveApikeyResult result = new RetrieveApikeyResult();

            Assert.Throws(typeof (ApplicationException), delegate
                                                             {
                                                                 ResultParser.ParseResultBase(xDoc, result);
                                                             });
        }


    }
}
