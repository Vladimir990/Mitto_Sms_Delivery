using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitto_Sms_Delivery
{
    internal interface IMessageService
    {
        void SplitMessage(string _message);
        void InputPrices(string? _message);
        void GenerateStats();
        void PrintTopTenSenders();
        void PrintHelp();
    }
}
