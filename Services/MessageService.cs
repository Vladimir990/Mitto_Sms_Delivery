using Mitto_Sms_Delivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitto_Sms_Delivery
{
    internal class MessageService : IMessageService
    {

        private List<Message> messagesList = new List<Message>();
        private List<Price> priceList = new List<Price>();

        public MessageService()
        {
            var t = new System.Timers.Timer(60000) { Enabled = true };
            t.Elapsed += (sender, args) => {
                PrintTopTenSenders();
            };
        }


        public void SplitMessage(string? _message)
        {
            var result = Split(_message);

            if (result?.Count == 3) MapMessageToObject(result);

            else WriteError();

        }

        private void MapMessageToObject(List<string> splittedList)
        {
            Message message = new Message()
            {
                Sender = splittedList[0],
                Recipient = splittedList[1],
                MessageText = splittedList[2]
            };

            messagesList.Add(message);
        }

        public void PrintTopTenSenders()
        {
            if (priceList.Count > 0 && messagesList.Count > 0) 
            {
                CheckCountry();
            }
            var count = messagesList.GroupBy(x => new { x.Sender, x.Price })
                .Select(x => new { x.Key.Sender, Count = x.Count(), x.Key.Price?.SmsPrice }).OrderByDescending(x => x.Count).Take(10);

            

            

            Console.WriteLine(" \n\n\nTop 10 senders at: " + DateTime.UtcNow);
            Console.WriteLine("================================================");

            if (count.Count() == 0) Console.WriteLine("No data so far");

            foreach (var message in count)
            {
                Console.WriteLine(message.Sender + " " + message.Count + " " + message.SmsPrice * message.Count);
            }

            Console.WriteLine("================================================ \n\n\n");
        }

        private void WriteError()
        {
            Console.WriteLine("\n\n=====================================================================================");
            Console.WriteLine("Enter the sender, recipient and message in the exact order with a space in between.");
            Console.WriteLine("If the message contains more than one word, it should be enclosed in quotation marks.");
            Console.WriteLine("===================================================================================== \n\n");
        }

        private void WritePriceError()
        {
            Console.WriteLine("\n\n==================================================================================================");
            Console.WriteLine("Enter the country number, country name and SMS price in the exact order with a space in between.");
            Console.WriteLine("                 The country number must contain a maximum of three digits");
            Console.WriteLine("         and the price of the SMS must be in decimal format with three decimal places");
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>> TO EXIT PRICE INPUT TYPE => quit-pricing <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            Console.WriteLine("================================================================================================== \n\n");
        }

        private List<string>? Split(string? input)
        {
            return input?.Split('"')
                .Select((element, index) => index % 2 == 0
                ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                : new string[] { element })
                .SelectMany(element => element).ToList();
        }

        public void InputPrices(string? _message)
        {
            var result = Split(_message);
            if (result?.Count == 3 && CheckPrice(result[2]) == 3 && (result[0].Length <= 3 && result[0].Length > 0)) MapPriceToObject(result);
            else WritePriceError();

        }

        private void MapPriceToObject(List<string> splittedList)
        {
            decimal pr;
            Decimal.TryParse(splittedList[2], out pr);
            Price price = new Price()
            {
                CountryCode = splittedList[0],
                CountryName = splittedList[1],
                SmsPrice = pr
            };

            priceList.Add(price);
        }

        private void CheckCountry() 
        {
            foreach (var m in messagesList)
            {
                foreach(var p in priceList)
                    if (m.Sender.StartsWith(p.CountryCode))
                    {
                        m.Price = p;
                    }
            }
        }

        private int CheckPrice(string price)
        {
            var i = price.Length - price.LastIndexOf(".") - 1;
            return i;
        }

        public void GenerateStats()
        {
            List<Stats> stats = new List<Stats>();

            if (priceList.Count > 0 && messagesList.Count > 0)
            {
                CheckCountry();
            }

            var count = messagesList.GroupBy(x => new { x.Price?.CountryName, x.Sender, x.Price?.SmsPrice })
                .Select(x => new { Count = x.Count(), x.Key.CountryName, x.Key.SmsPrice }).OrderByDescending(x => x.Count);

            foreach (var m in count)
            {
                if (m.CountryName != null)
                {
                    Stats st = new Stats()
                    {
                        Country = m.CountryName,
                        NumberOfMessages = m.Count,
                        TotalPrice = m.Count * m.SmsPrice

                    };

                    stats.Add(st);
                }
            }

            PrintStats(stats);
            
        }

        private void PrintStats(List<Stats> stats)
        {
            Console.WriteLine(" \n\nStatistic by country");
            Console.WriteLine("================================================");

            if (stats.Count > 0)
            {
                foreach (Stats st in stats)
                {
                    Console.WriteLine(st.Country + " " + st.NumberOfMessages + " " + st.TotalPrice);
                }
            }
            else
            {
                Console.WriteLine("No data");
            }

            Console.WriteLine("================================================\n");

        }

        public void PrintHelp()
        {
            Console.WriteLine("\n====================================================\n");
            Console.WriteLine("pricing           -----> enter pricing mode");
            Console.WriteLine("quit-pricing      -----> exit pricing mode");
            Console.WriteLine("stats -s          -----> print statistics by sender");
            Console.WriteLine("stats -c or stats -----> print statistics by country");
            Console.WriteLine("quit              -----> exit the application");
            Console.WriteLine("\n====================================================\n");
        }

    }
}
