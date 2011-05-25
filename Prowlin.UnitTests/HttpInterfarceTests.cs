using System.Net;
using Xunit;

namespace Prowlin.UnitTests
{
    public class HttpInterfarceTests
    {
        [Fact]
        public void reqeust_method_should_be_post_for_add() {
            var httpInterface = new HttpInterface();
            HttpWebRequest httpWebRequest = httpInterface.BuildRequest("http://www.nnihlen.com/blog/", Method.Add, null);

            Assert.Equal("POST", httpWebRequest.Method);
        }


        [Fact]
        public void reqeust_method_should_be_get_for_verify() {
            var httpInterface = new HttpInterface();
            HttpWebRequest httpWebRequest = httpInterface.BuildRequest("http://www.nnihlen.com/blog/", Method.Verify,
                                                                       null);
            Assert.Equal("GET", httpWebRequest.Method);
        }


        [Fact]
        public void retrieveToken_method_should_be_get_for_verify() {
            var httpInterface = new HttpInterface();
            HttpWebRequest httpWebRequest = httpInterface.BuildRequest("http://www.nnihlen.com/blog/", Method.GetToken,
                                                                       null);

            Assert.Equal("GET", httpWebRequest.Method);
        }
    }
}