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
