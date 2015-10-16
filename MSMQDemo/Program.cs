using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;

namespace MSMQDemo
{
    class Program
    {

        protected static string LocalQueue = @".\private$\CSMSMQ";

        protected static string TecentQueue = @"FormatName:DIRECT=TCP:10.1.205.117\private$\TecentCSMSMQ";
        static void Main(string[] args)
        {
            try
            {
                if (!MessageQueue.Exists(LocalQueue))
                {
                    var localQueue = MessageQueue.Create(LocalQueue);
                    localQueue.Send("hello word");
                }
                var newQueue = new MessageQueue(LocalQueue)
                {
                    Formatter = new XmlMessageFormatter()
                };
                var message = newQueue.Receive();
                if (message != null) Console.WriteLine(message.Body);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
