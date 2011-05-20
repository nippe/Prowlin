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
        public void reqeust_method_should_be_post_for_add()
        {
            HttpInterface httpInterface = new HttpInterface();

            HttpWebRequest httpWebRequest = httpInterface.BuildRequest("http://www.nnihlen.com/blog/", Method.Add, null);

            Assert.Equal("POST", httpWebRequest.Method);
        }

        [Fact]
        public void reqeust_method_should_be_get_for_verify()
        {
            HttpInterface httpInterface = new HttpInterface();
            
            HttpWebRequest httpWebRequest = httpInterface.BuildRequest("http://www.nnihlen.com/blog/", Method.Verify, null);

            Assert.Equal("GET", httpWebRequest.Method);
        }


 



    }
}
