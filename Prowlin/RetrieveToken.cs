using System.ComponentModel.DataAnnotations;

namespace Prowlin
{
    public class RetrieveToken
    {
        [Required]
        [StringLength(40, MinimumLength = 40)]
        public string ProviderKey { get; set; }
    }
}