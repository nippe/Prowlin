using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Prowlin.Interfaces;

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

        public NotificationResult SendNotification(INotification notification) {
            ValidateObject(notification);
            return _httpInterface.SendNotification(notification);
        }

        public VerificationResult SendVerification(IVerification verification) {
            ValidateObject(verification);
            return _httpInterface.SendVerification(verification);
        }

        public RetrieveTokenResult RetreiveToken(RetrieveToken retrieveToken) {
            ValidateObject(retrieveToken);
            return _httpInterface.RetrieveToken(retrieveToken);
        }

        public RetrieveApikeyResult RetrieveApikey(RetrieveApikey retrieveApikey) {
            ValidateObject(retrieveApikey);

            return _httpInterface.RetrieveApikey(retrieveApikey);
        }

        private void ValidateObject(object objectToValidate) {
            var context = new ValidationContext(objectToValidate, null, null);
            var results = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(objectToValidate, context, results, true);

            if (valid == false) {
                foreach (var validationResult in results) {
                    throw new ArgumentException(validationResult.ErrorMessage);
                }
            }
        }
    }
}