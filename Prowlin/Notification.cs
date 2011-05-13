using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prowlin
{
    public class Notification
    {
        private string _url = string.Empty;

        public NotificationPriority Priority {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Url {
            get { return _url; }
            set {
                if(value.Length > 512)
                {
                    throw new ArgumentException("Url can maximum be 512 characters");
                }
                _url = value;
            }
        }

        
        public string Event {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        public string Description {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
