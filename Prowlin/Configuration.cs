using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Prowlin
{
    public class Configuration
    {
        private IList<string> _apiKeys = new List<string>();

        public void AddApiKey(string key) {
            _apiKeys.Add(key);
        }

        [Required]
        public string Keys {
            get { return string.Join(",", _apiKeys.ToArray()); }
        }

        [Required]
        [StringLength(256)]
        public string Application { get; set; }
    }
}
