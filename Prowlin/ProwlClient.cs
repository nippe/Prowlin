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


        //Validate using DataAnnotations: http://stackoverflow.com/questions/2050161/validating-dataannotations-with-validator-class


        public void SendNotification(INotification notification) {
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

            //Return no of messages left
            _httpInterface.SendNotification();
        }
    }
}