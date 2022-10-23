using Microsoft.Extensions.DependencyInjection;
using Mitto_Sms_Delivery;

var serviceProvider = new ServiceCollection()
            .AddSingleton<IMessageService, MessageService>()
            .BuildServiceProvider();


var messageService = serviceProvider.GetService<IMessageService>();

Console.WriteLine("=======================Welcome to Mitto SMS delivery==============================");
Console.WriteLine("Enter the sender, recipient and message in the exact order with a space in between");
Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>> TO EXIT TYPE => quit <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>> FOR INSERT PRICE TYPE => pricing <<<<<<<<<<<<<<<<<<<<<<<<<<");
Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>> FOR HELP TYPE -h or help <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
Console.WriteLine("==================================================================================\n");

while (true)
{
    try
    {
        string? input = Console.ReadLine();
        if (input == "quit")
            break;

        else if (input == "pricing")
        {
            while(true)
            {
                Console.WriteLine("Enter the price in the format: country code, country name and SMS price");
                Console.WriteLine(">");
                string? priceInput = Console.ReadLine();
                if (priceInput == "quit-pricing")
                    break;

                if (priceInput == "help" || priceInput == "-h")
                {
                    messageService?.PrintHelp();
                }

                messageService?.InputPrices(priceInput);
            }
        }

        else if(input == "stats" || input == "stats -c")
        {
            messageService?.GenerateStats();
        }

        else if(input == "stats -s")
        {
            messageService?.PrintTopTenSenders();
        }

        else if (input == "help" || input == "-h")
        {
            messageService?.PrintHelp();
        }
        else
        {
            messageService?.SplitMessage(input);
        }
    }
    catch (ArgumentOutOfRangeException aoorex)
    {
        Console.WriteLine(aoorex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
