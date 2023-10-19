using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxisBank.Models
{
    public class ForDeposite
    {
        public string AccountNo { get; set; }
        public string FirstName { get; set; }
        public double Balance { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}