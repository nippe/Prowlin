using System.ComponentModel.DataAnnotations;

namespace Prowlin
{
    public interface INotification
    {
        void AddApiKey(string key);

        [Required]
        string Keys { get; }

        [Required]
        [StringLength(256)]
        string Application { get; set; }

        NotificationPriority Priority { get; set; }

        [StringLength(512)]
        string Url { get; set; }

        [Required]
        [StringLength(1024)]
        string Event { get; set; }

        [Required]
        [StringLength(10000)]
        string Description { get; set; }
    }
}