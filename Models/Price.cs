using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitto_Sms_Delivery.Models
{
    internal class Price
    {
        [MaxLength(3)]
        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public decimal SmsPrice { get; set; }
    }
}
