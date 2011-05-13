using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Prowlin
{
    public class Notification
    {

        public NotificationPriority Priority { get; set; }

        [StringLength(512)]
        public string Url { get; set; }
//        {
//            get { return _url; }
//            set {
//                if(value.Length > 512)
//                {
//                    throw new ArgumentException("Url can maximum be 512 characters");
//                }
//                _url = value;
//            }
//        }

        [Required]
        [StringLength(1024)]
        public string Event { get; set; }

        [Required]
        [StringLength(10000)]
        public string Description { get; set; }
    }
}
