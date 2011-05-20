using System.ComponentModel.DataAnnotations;

namespace Prowlin
{
    public interface IVerification
    {
        [Required]
        [StringLength(40, MinimumLength = 40)]
        string ApiKey { get; set; }

        [StringLength(40, MinimumLength = 40)]
        string ProviderKey { get; set; }
    }
}