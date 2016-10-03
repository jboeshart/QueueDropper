using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System.Configuration;

namespace QueueDropper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*************");
            Console.WriteLine("Welcome to the QueueDropper app!");
            Console.WriteLine("This little app will drop a message on an Azure Service Bus queue!");
            Console.WriteLine("*************");
            Console.WriteLine();
            Console.WriteLine("Enter 1 for one message, 2 for 20, or 3 for endless loop");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    OneMessage();
                    break;
                case "2":
                    TwentyMessages();
                    break;
                case "3":
                    EndlessMessages();
                    break;
                default:
                    Console.WriteLine("Sorry, invalid entry");
                    break;
            }
                   
            Console.WriteLine("Success!!");
            Console.ReadLine();
        }

        private static MessageSender SetupMessageSender()
        {
            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"]);
            MessageSender sender = factory.CreateMessageSender(ConfigurationManager.AppSettings["ServiceBusQueueName"]);
            return sender;
        }

        private static void OneMessage()
        {
            MessageSender messageSender = SetupMessageSender();
            Console.WriteLine("Please enter the message you want to drop on the queue");
            string message = Console.ReadLine();
            Console.WriteLine("Writing message: " + message);
            BrokeredMessage bm = new BrokeredMessage(message);
            messageSender.Send(bm);
        }

        private static void TwentyMessages()
        {
            MessageSender messageSender = SetupMessageSender();
            for (int i = 1; i <= 20; i++)
            {
                Console.WriteLine("Writing message {0}", i);
                string message = "twentymessage" + i;
                BrokeredMessage bm = new BrokeredMessage(message);
                messageSender.Send(bm);
            }
        }

        private static void EndlessMessages()
        {
            MessageSender messageSender = SetupMessageSender();
            int i = 1;
            while (true)
            {
                Console.WriteLine("Writing message {0}", i);
                string message = "endlessmessage" + i;
                BrokeredMessage bm = new BrokeredMessage(message);
                messageSender.Send(bm);
                i++;
            }
        }
    }
}
