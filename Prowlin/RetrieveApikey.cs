using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Prowlin
{
    public class RetrieveApikey
    {
        [Required]
        [StringLength(40, MinimumLength = 40)]
        public string ProviderKey { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 40)]
        public string Token { get; set; }
    }
}
