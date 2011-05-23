using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prowlin
{
    public class ProwlClient
    {
        private readonly IHttpInterface _httpInterface;

        public ProwlClient() {
            _httpInterface = new HttpInterface();
        }


        public ProwlClient(IHttpInterface httpInterface) {
            _httpInterface = httpInterface;
        }

        public int SendNotification(INotification notification) {
            //Validate Object

            var context = new ValidationContext(notification, null, null);
            var results = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(notification, context, results, true);
            
            if(valid == false) {
                foreach (var validationResult in results) {
                    throw new ArgumentException(validationResult.ErrorMessage);
                }
            }


            //Send Prowl Notification
            return _httpInterface.SendNotification(notification);

            //Return no of messages left
            //_httpInterface.SendNotification();
        }

        public VerificationResult SendVerification(IVerification verification) {
            var context = new ValidationContext(verification, null, null);
            var results = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(verification, context, results, true);

            if (valid == false) {
                foreach (var validationResult in results) {
                    throw new ArgumentException(validationResult.ErrorMessage);
                }
            }

            return _httpInterface.SendVerification(verification);
        }

        public RetrieveTokenResult RetreiveToken(RetrieveToken retrieveToken) {
            var context = new ValidationContext(retrieveToken, null, null);
            var results = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(retrieveToken, context, results, true);

            if (valid == false)
            {
                foreach (var validationResult in results)
                {
                    throw new ArgumentException(validationResult.ErrorMessage);
                }
            }

            return _httpInterface.RetrieveToken(retrieveToken);


        }
    }
}