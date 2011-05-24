using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prowlin.UnitTests.Fakes
{
    public class FakeHttpInterface : IHttpInterface
    {
        private bool _sendNotificationsCalled = false;
        private bool _sendVerificationCalled = false;
        private bool _retrieveTokenCalled = false;
        private bool _retrieveApikeyCalled;

        public int SendNotification() {
            _sendNotificationsCalled = true;

            return 900;
        }

        public int SendNotification(INotification notification) {
            _sendNotificationsCalled = true;
            return 900;
        }

        public VerificationResult SendVerification(IVerification verification) {
            _sendVerificationCalled = true;
            return new VerificationResult()
                       {
                           ErrorMessage = string.Empty,
                           RemainingMessageCount = 999,
                           ResultCode = "200",
                           TimeStamp = "" //TODO: Get valid timestamp format
                       };
        }

        public RetrieveTokenResult RetrieveToken(RetrieveToken retrieveToken) {
            _retrieveTokenCalled = true;
            return new RetrieveTokenResult()
                       {
                           RemainingMessageCount = 999,
                           ResultCode = "200",
                           TimeStamp = "",
                           Token = "ASDFGASDFGASDFGASDFGASDFGASDFGASDFGASDFG",
                           Url = "http://www.prowlapp.com/"
                       };
        }

        public RetrieveApikeyResult RetrieveApikey(RetrieveApikey retrieveApikey) {
            _retrieveApikeyCalled = true;
            return new RetrieveApikeyResult() { ApiKey = "0987654321098765432109876543210987654321" };
        }


        public bool SendNotificationsCalled {
            get { return _sendNotificationsCalled; }
        }

        public bool SendVerificationCalled {
            get { return _sendVerificationCalled; }
        }

        public bool RetrieveTokenCalled {
            get { return _retrieveTokenCalled; }
        }

        public bool RetrieveApikeyCalled {
            get { return _retrieveApikeyCalled; }
        }
    }
}
