using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Prowlin.UnitTests
{
    public class ConfigurationTests
    {

        [Fact]
        public void added_apikeys_should_be_retreived_as_comma_separated_string() {
            string key1 = "qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfg";
            string key2 = "mnbvcxzasdfghjklpoiuytrewq1234567890zxcv";
            
            Configuration c = new Configuration();
            c.AddApiKey(key1);
            c.AddApiKey(key2);

            Assert.Equal(key1 + "," + key2, c.Keys);
        }

        [Fact]
        public void Application_property_should_work() {
            Configuration conf = new Configuration();
            conf.Application = "AppName";

            Assert.Equal("AppName", conf.Application);
        }
    }
}
