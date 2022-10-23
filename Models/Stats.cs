using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitto_Sms_Delivery.Models
{
    internal class Stats
    {
        public string Sender { get; set; }

        public int NumberOfMessages { get; set; }

        public decimal? TotalPrice { get; set; }

        public string? Country { get; set; }
    }
}
