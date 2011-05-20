using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prowlin;
using Xunit;

namespace Prowlin.UnitTests
{
    public class RequestBuilderHelperTests
    {
        [Fact]
        public void should_build_dictionary_correctly_from_notification() {
            Notification notif = new Notification()
                                     {
                                         Application = "app",
                                         Description = "descr",
                                         Event = "evt",
                                         Priority = NotificationPriority.Emergency,
                                         Url = "http://www.nnihlen.com/blog"

                                     };
            notif.AddApiKey("asdf");

            RequestBuilderHelper helper = new RequestBuilderHelper();
            Dictionary<string, string> dict = helper.BuildDictionaryForNotificataion(notif);

            Assert.True(dict.ContainsKey("application"));
            Assert.Equal("app", dict["application"]);

            Assert.True(dict.ContainsKey("description"));
            Assert.Equal("descr", dict["description"]);

            Assert.True(dict.ContainsKey("event"));
            Assert.Equal("evt", dict["event"]);

            Assert.True(dict.ContainsKey("priority"));
            Assert.Equal("2", dict["priority"]);

            Assert.True(dict.ContainsKey("url"));
            Assert.Equal("http://www.nnihlen.com/blog", dict["url"]);

            Assert.True(dict.ContainsKey("apikey"));
            Assert.Equal("asdf", dict["apikey"]);


        }


        [Fact]
        public void properties_should_be_request_formated()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            d.Add("url", "foo");
            d.Add("event", "yes");

            RequestBuilderHelper helper= new RequestBuilderHelper();

            Assert.Equal("url=foo&event=yes", helper.BuildParameterString(d));
        }

        [Fact]
        public void value_should_be_url_encoded()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("url", "http://www.nnihlen.com/blog");

            RequestBuilderHelper helper = new RequestBuilderHelper();

            Assert.Equal("url=http%3a%2f%2fwww.nnihlen.com%2fblog", helper.BuildParameterString(dict));

        }


        [Fact]
        public void reqeust_uri_should_build_correctly_for_add_with_trailing_slash()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            RequestBuilderHelper helper = new RequestBuilderHelper();
            string url = helper.BuildRequestUrl("http://www.nnihlen.com/blog/", Method.Add, d);

            Assert.Equal("http://www.nnihlen.com/blog/add", url);
        }

        [Fact]
        public void reqeust_uri_should_build_correctly_for_verify_with_trailing_slash()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            RequestBuilderHelper helper = new RequestBuilderHelper();
            string url = helper.BuildRequestUrl("http://www.nnihlen.com/blog/", Method.Verify, d);

            Assert.Equal("http://www.nnihlen.com/blog/verify", url);
        }

        [Fact]
        public void reqeust_uri_should_build_correctly_for_add_without_trailing_slash()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            RequestBuilderHelper helper = new RequestBuilderHelper();
            string url = helper.BuildRequestUrl("http://www.nnihlen.com/blog", Method.Add, d);

            Assert.Equal("http://www.nnihlen.com/blog/add", url);
        }


        [Fact]
        public void reqeust_uri_should_build_correctly_for_add_with_parameters_trailing_slash()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("url", "foo");
            d.Add("event", "yes");

            RequestBuilderHelper helper = new RequestBuilderHelper();

            string url = helper.BuildRequestUrl("http://www.nnihlen.com/blog/", Method.Add, d);


            Assert.Equal("http://www.nnihlen.com/blog/add?url=foo&event=yes", url);
        }




    }
}
