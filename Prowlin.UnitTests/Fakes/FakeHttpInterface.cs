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

        public int SendNotification() {
            _sendNotificationsCalled = true;

            return 900;
        }

        public void SendVerification() {
            throw new NotImplementedException();
        }


        public bool SendNotificationsCalled {
            get { return _sendNotificationsCalled; }
        }

        public bool SendVerificationCalled {
            get { return _sendVerificationCalled; }
        }
    }
}
