using System.ComponentModel.DataAnnotations;
using Prowlin.Interfaces;

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