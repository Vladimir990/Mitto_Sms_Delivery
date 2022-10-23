using Mitto_Sms_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitto_Sms_Delivery
{
    internal class Message
    {
        public string Sender { get; set; }

        public string Recipient { get; set; }

        public string MessageText { get; set; }

        public Price? Price { get; set; }


    }
}
