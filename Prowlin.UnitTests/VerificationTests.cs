using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Prowlin.UnitTests
{
    public class VerificationTests
    {

        [Fact]
        public void verification_should_have_two_properties() {
            Verification verification = new Verification();

            verification.ApiKey = "1234567890123456789012345678901234567890";
            verification.ProviderKey = "1234567890123456789012345678901234567890";

            Assert.Equal("1234567890123456789012345678901234567890", verification.ApiKey);
            Assert.Equal("1234567890123456789012345678901234567890", verification.ProviderKey);
        }
        

        [Fact]
        public void verification_with_no_providerkey_should_return_null() {
            Verification verification = new Verification();

            verification.ApiKey = "1234567890123456789012345678901234567890";

            Assert.Equal("1234567890123456789012345678901234567890", verification.ApiKey);
            Assert.Null(verification.ProviderKey);
        }
        


    }
}
