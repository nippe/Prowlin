using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;


namespace Prowlin.UnitTests
{
    
    public class NotificationTests
    {

        [Fact]
        public void this_should_work()
        {
            Assert.True(true);
        }

        [Fact]
        public void assigning_values_to_properties_should_wrok() {
            
            Notification notification = new Notification();
            
            notification.Priority = NotificationPriority.Normal;
            notification.Url = "http://nnihlen.com/blog";
            notification.Event = "Test event";
            notification.Description = "Description of test event...";
        }

        [Fact]
        public void added_apikeys_should_be_retreived_as_comma_separated_string()
        {
            string key1 = "qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfg";
            string key2 = "mnbvcxzasdfghjklpoiuytrewq1234567890zxcv";

            Notification notification = new Notification();
            notification.AddApiKey(key1);
            notification.AddApiKey(key2);

            Assert.Equal(key1 + "," + key2, notification.Keys);
        }

        [Fact]
        public void Application_property_should_work()
        {
            Notification notification = new Notification();
            notification.Application = "AppName";

            Assert.Equal("AppName", notification.Application);
        }


        [Fact]
        public void not_setting_priority_should_default_to_normal_priority() {
            Notification n = new Notification();

            Assert.Equal(NotificationPriority.Normal, n.Priority);
        }


        //[Fact]
        //public void assigning_Url_more_than_512_characters_should_throw_exception() {
        //    string s = new string('n', 600);

        //    Notification notification = new Notification();

        //    Assert.Throws<ArgumentException>(
        //        delegate
        //            {
        //                notification.Url = s;
        //            });
        //}

      
            

    }
}
