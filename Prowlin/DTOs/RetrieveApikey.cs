using System.ComponentModel.DataAnnotations;

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
