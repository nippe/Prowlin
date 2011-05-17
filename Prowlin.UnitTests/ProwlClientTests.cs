using System;
using System.Net;
using Prowlin.UnitTests.Fakes;
using Xunit;
using Prowlin;

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

            string appStr = new string('n', 257);
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
                            Event = evt, //Toooo long
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

    }
}