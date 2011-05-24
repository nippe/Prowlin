using System;
using Prowlin.UnitTests.Fakes;
using Xunit;

namespace Prowlin.UnitTests
{
    public class ProwlClientTests
    {
        [Fact]
        public void ProwlClient_should_take_IHttpNotfication_in_constructor() {
            IHttpInterface httpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(httpInterface);
        }


        [Fact]
        public void SendVerification_on_httpInterface_should_be_called_when_sending_notification() {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var n = new Notification
                        {
                            Application = "app",
                            Description = "foo",
                            Event = "event",
                            Priority = NotificationPriority.Normal
                        };
            n.AddApiKey("aaaaaaaaaabbbbbbbbbbccccccccccdddddddddd");

            prowlClient.SendNotification(n);

            Assert.True(fakeHttpInterface.SendNotificationsCalled);
        }


        [Fact]
        public void validation_should_pass_with_correct_notifictaion_object() {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var n = new Notification
                        {
                            Application = "app",
                            Description = "foo",
                            Event = "event",
                            Priority = NotificationPriority.Normal,
                            Url = "http://www.nnihle.com/blog"
                        };
            n.AddApiKey("aaaaaaaaaabbbbbbbbbbccccccccccdddddddddd");

            prowlClient.SendNotification(n);

            Assert.True(fakeHttpInterface.SendNotificationsCalled);
        }


        [Fact]
        public void no_application_should_not_pass_validation_and_throw_ArgumentException() {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var n = new Notification
                        {
                            Description = "foo",
                            Event = "event",
                            Priority = NotificationPriority.Normal,
                            Url = "http://www.nnihle.com/blog"
                        };
            n.AddApiKey("aaaaaaaaaabbbbbbbbbbccccccccccdddddddddd");

            Assert.Throws(typeof (ArgumentException), delegate { prowlClient.SendNotification(n); });
        }


        [Fact]
        public void Application_with_more_than_256_characters_should_throw_ArgumentException() {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var appStr = new string('n', 257);
            var n = new Notification
                        {
                            Application = appStr,
                            Description = "foo",
                            Event = "event",
                            Priority = NotificationPriority.Normal,
                            Url = "http://www.nnihle.com/blog"
                        };
            n.AddApiKey("aaaaaaaaaabbbbbbbbbbccccccccccdddddddddd");

            Assert.Throws(typeof (ArgumentException), delegate { prowlClient.SendNotification(n); });
        }

        [Fact]
        public void no_Event_should_not_pass_validation_and_throw_ArgumentException() {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var n = new Notification
                        {
                            Application = "Application",
                            Description = "foo",
                            Priority = NotificationPriority.Normal,
                            Url = "http://www.nnihle.com/blog"
                        };
            n.AddApiKey("aaaaaaaaaabbbbbbbbbbccccccccccdddddddddd");

            Assert.Throws(typeof (ArgumentException), delegate { prowlClient.SendNotification(n); });
        }


        [Fact]
        public void Event_with_more_than_1024_characters_should_not_pass_validation_and_throw_ArgumentException() {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var evt = new string('n', 2000);

            var n = new Notification
                        {
                            Application = "Application",
                            Description = "foo",
                            Event = evt,
                            //Toooo long
                            Priority = NotificationPriority.Normal,
                            Url = "http://www.nnihle.com/blog"
                        };
            n.AddApiKey("aaaaaaaaaabbbbbbbbbbccccccccccdddddddddd");

            Assert.Throws(typeof (ArgumentException), delegate { prowlClient.SendNotification(n); });
        }


        [Fact]
        public void Url_with_more_than_512_characters_should_not_pass_validation_and_throw_ArgumentException() {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var url = new string('n', 513);

            var n = new Notification
                        {
                            Application = "Application",
                            Description = "foo",
                            Event = "evnet",
                            Priority = NotificationPriority.Normal,
                            Url = url
                        };
            n.AddApiKey("aaaaaaaaaabbbbbbbbbbccccccccccdddddddddd");

            Assert.Throws(typeof (ArgumentException), delegate { prowlClient.SendNotification(n); });
        }

        [Fact]
        public void Verification_apikey_not_present_throws_validation_ArgumentException() {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var verification = new Verification
                                   {
                                       ProviderKey = "1234567890123456789012345678901234567890"
                                   };

            Assert.Throws(typeof (ArgumentException),
                          delegate { prowlClient.SendVerification(verification); });
        } 
        
        [Fact]
        public void Verification_apikey_wrong_length_throws_validation_ArgumentException() {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var verification = new Verification
                                   {
                                       ApiKey = "12345678678901234567890"
                                   };

            Assert.Throws(typeof (ArgumentException),
                          delegate { prowlClient.SendVerification(verification); });
        }  
        

        [Fact]
        public void Verification_ProviderKey_wrong_length_throws_validation_ArgumentException() {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var verification = new Verification
                                   {
                                       ApiKey = "123456789012345678901234567890",
                                       ProviderKey = "ewrwe"
                                   };

            Assert.Throws(typeof (ArgumentException),
                          delegate { prowlClient.SendVerification(verification); });
        }

        [Fact]
        public void RetrieveToken_should_call_RetriveToken_on_HttpInterface() {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var retriveTokenMessage = new RetrieveToken()
                                          {
                                              ProviderKey = "1234567890123456789012345678901234567890"
                                          };

            prowlClient.RetreiveToken(retriveTokenMessage);

            Assert.True(fakeHttpInterface.RetrieveTokenCalled);
        }

        [Fact]
        public void RetrieveToken_WithShortProviderKey_ShouldTrhowArgumentException() {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var retriveTokenMessage = new RetrieveToken()
            {
                ProviderKey = "12345678901234567890123456789012345678"
            };

            Assert.Throws(typeof(ArgumentException), delegate
                                                         {
                                                              prowlClient.RetreiveToken(retriveTokenMessage);
                                                         });
           
            Assert.False(fakeHttpInterface.RetrieveTokenCalled);
        } 
        
        
        [Fact]
        public void RetrieveToken_WithLongProviderKey_ShouldTrhowArgumentException() {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var retriveTokenMessage = new RetrieveToken()
            {
                ProviderKey = "12345678901234567890123456789012345678901290"
            };

            Assert.Throws(typeof(ArgumentException), delegate
                                                         {
                                                              prowlClient.RetreiveToken(retriveTokenMessage);
                                                         });
           
            Assert.False(fakeHttpInterface.RetrieveTokenCalled);
        }


        [Fact]
        public void SendVerification_should_call_SendVerification_on_HttpInterface()
        {
            var fakeHttpInterface = new FakeHttpInterface();
            var prowlClient = new ProwlClient(fakeHttpInterface);

            var verification = new Verification
            {
                ApiKey = "1234567890123456789012345678901234567890",
                ProviderKey = "ewrwe"
            };

            prowlClient.SendVerification(verification);
            Assert.True(fakeHttpInterface.SendVerificationCalled);
        }

        [Fact]
        public void RetrieveApiKey_should_call_RetrieveApikey_on_HttpInterface() {
            FakeHttpInterface fakeHttpInterface = new FakeHttpInterface();
            ProwlClient client = new ProwlClient(fakeHttpInterface);

            RetrieveApikey retrieveApikey = new RetrieveApikey()
                                                {
                                                    ProviderKey = "12345678901234567890123456789012345678901290",
                                                    Token = "12345678901234567890123456789012345678901290"
                                                };
            client.RetrieveApikey(retrieveApikey);
        }

        [Fact]
        public void RetrieveApiKey_should_return_correct_fake_data()
        {
            FakeHttpInterface fakeHttpInterface = new FakeHttpInterface();
            ProwlClient client = new ProwlClient(fakeHttpInterface);

            RetrieveApikey retrieveApikey = new RetrieveApikey()
            {
                ProviderKey = "1234567890123456789012345678901234567890",
                Token =       "1234567890123456789012345678901234567890"
            };
            RetrieveApikeyResult retrieveApikeyResult = client.RetrieveApikey(retrieveApikey);

            Assert.Equal("0987654321098765432109876543210987654321", retrieveApikeyResult.ApiKey);
        }
    }
}