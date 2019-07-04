using System;

namespace RabbitMqServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Receive1.ReceiveMessage();
        }
    }
}
