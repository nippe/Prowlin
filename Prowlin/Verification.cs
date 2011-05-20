using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Prowlin
{
    public class Verification : IVerification
    {
        [Required]
        [StringLength(40, MinimumLength = 40)]
        public string ApiKey { get; set; }

        public string ProviderKey { get; set; }

    }
}