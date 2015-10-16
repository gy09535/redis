using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {

            TcpListener listener = new TcpListener(IPAddress.Parse("10.1.205.117"), 8500);
            int count = 0;
            listener.Start();
            Console.WriteLine("正在接收数据");
            var tcpClient = listener.AcceptTcpClient();
            while (true)
            {
                Console.WriteLine("输入q退出是否退出程序......");
                string key = Console.ReadLine();
                if (key == "Q")
                {
                    return;
                }

                var fromClientStream = tcpClient.GetStream();
                byte[] buBytes = new byte[1000];

                //MemoryStream stream = new MemoryStream();
                fromClientStream.Read(buBytes, 0, 1000);
                //stream.Write(buBytes, 0, buBytes.Length);
                string msg = Encoding.Unicode.GetString(buBytes);
                Console.WriteLine(msg);
                Console.WriteLine("接收完毕可以发送消息");
                var sendMsg = Console.ReadLine();
                sendMsg = string.IsNullOrWhiteSpace(sendMsg) ? string.Empty : sendMsg;
                byte[] sendBytes = Encoding.Unicode.GetBytes(sendMsg);
                fromClientStream.Write(sendBytes, 0, sendBytes.Length);
                Console.WriteLine("发送成功");
            }
        }
        public static IPAddress GetServerIp()
        {
            var ipHost = Dns.GetHostEntry(Dns.GetHostName());
            return ipHost.AddressList[0];
        }
    }
}
