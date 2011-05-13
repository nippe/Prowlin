using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Prowlin.UnitTests
{
    public class ProwlClientTests
    {

        [Fact]
        public void instasiating_ProwlClient_sholud_work() {
            Configuration config = new Configuration();
            ProwlClient prowlClient = new ProwlClient();
        }


        /***
         * IProwlClient c = new ProwlClient(Configuration)
         * c.Send(Notification)
         */

    }
}
