using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;

namespace Prowlin.UnitTests
{
    public class HttpInterfarceTests
    {
        [Fact]
        public void properties_should_be_request_formated() {
            Dictionary<string, string> d = new Dictionary<string, string>();

            d.Add("url", "foo");
            d.Add("event", "yes");

            HttpInterface httpInterface = new HttpInterface();

            Assert.Equal("url=foo&event=yes", httpInterface.BuildParameterString(d));
        }

        [Fact]
        public void value_should_be_url_encoded() {
            Dictionary<string, string > dict = new Dictionary<string, string>();
            dict.Add("url", "http://www.nnihlen.com/blog");

            HttpInterface httpInterface = new HttpInterface();
            
            Assert.Equal("url=http%3a%2f%2fwww.nnihlen.com%2fblog", httpInterface.BuildParameterString(dict));

        }

        [Fact]
        public void reqeust_uri_should_build_correctly_for_add_with_trailing_slash()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            HttpInterface httpInterface = new HttpInterface();
            string url = httpInterface.BuildRequestUrl("http://www.nnihlen.com/blog/", Method.Add, d);

            Assert.Equal("http://www.nnihlen.com/blog/add", url);
        }


        [Fact]
        public void reqeust_uri_should_build_correctly_for_add_with_parameters_trailing_slash()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("url", "foo");
            d.Add("event", "yes");

            HttpInterface httpInterface = new HttpInterface();

            string url = httpInterface.BuildRequestUrl("http://www.nnihlen.com/blog/", Method.Add, d);


            Assert.Equal("http://www.nnihlen.com/blog/add?url=foo&event=yes", url);
        }




        [Fact]
        public void reqeust_method_should_be_post_for_add()
        {
            HttpInterface httpInterface = new HttpInterface();
            string url = httpInterface.BuildRequestUrl("http://www.nnihlen.com/blog/", Method.Add, null);

            HttpWebRequest httpWebRequest = httpInterface.BuildRequest("http://www.nnihlen.com/blog/", Method.Add, null);

            Assert.Equal("POST", httpWebRequest.Method);
        }



    }
}
