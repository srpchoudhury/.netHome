using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace customvalidation.Models
{
    public class Employee
    {
        [Required]
        public string Name { get; set; }
        [RudraValidation(ErrorMessage ="Nitish is required")]
        public string message { get; set; }
    }
}