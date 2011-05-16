using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Prowlin
{
    public class Notification : INotification
    {

        private IList<string> _apiKeys = new List<string>();

        public void AddApiKey(string key)
        {
            _apiKeys.Add(key);
        }

        [Required]
        public string Keys
        {
            get { return string.Join(",", _apiKeys.ToArray()); }
        }

        [Required]
        [StringLength(256)]
        public string Application { get; set; }

        public NotificationPriority Priority { get; set; }

        [StringLength(512)]
        public string Url { get; set; }


        [Required]
        [StringLength(1024)]
        public string Event { get; set; }

        [Required]
        [StringLength(10000)]
        public string Description { get; set; }
    }
}
