# Mitto_Sms_Delivery

When the application is launched, the user has the option to enter the details of the sms message in the format of Sender, Recipient and Text of the message. The sender and recipient must be entered with the prefix of the country code but without the + sign (e.g. 381 for Serbia) The message must be in quotation marks if it contains more than one word (e.g. Hello and "Hello world")

The user has the option to enter the sms price for each country by entering the key "pricing" and the price menu will open.
The user must then enter the country code (maximum three letters e.g 381), the name of the country and the price of the SMS message, which must have three decimal places ( e.g. 0.011).
To exit pricing mode, type "quit-pricing"

The user can request statistics by country or by sender.
For statistics by country, enter "stats" or "stats -c"
For statistics per sender enter "stats -s". By default the application will automatically print the statistics of the top 10 senders every minute.

A help label can be displayed at any time by typing "help" or "-h"

To exit the app, type quit. This can only be done if you are not in the price menu. In that case, exit pricing mode first with "quit-pricing" and then exit the application with the keyword "quit".

The application will print top 10 senders statistic on every minut
