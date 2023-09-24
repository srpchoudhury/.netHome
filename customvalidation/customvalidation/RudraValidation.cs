using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace customvalidation
{
    public class RudraValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           if(value != null)
            {
                string message = value.ToString();
                if (message.Contains("nitish"))
                {
                    return ValidationResult.Success;
                }
             

            }
            ErrorMessage = ErrorMessage ?? validationContext.DisplayName + "field must contain nitish";
            return new ValidationResult(ErrorMessage);
        }
    }
}