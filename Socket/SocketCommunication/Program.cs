using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace SocketCommunication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient();
                var serverIp = IPAddress.Parse("10.1.205.117");
                client.Connect(serverIp, 8500);
                Console.WriteLine("正在连接服务器....");

                Console.WriteLine("正在监听");
                NetworkStream stream = client.GetStream();
                string msg = "\"Welcome To TraceFact.Net\"";
                byte[] buBytes = Encoding.Unicode.GetBytes(msg);

                stream.Write(buBytes, 0, buBytes.Length);
                Console.WriteLine(msg);
                Console.WriteLine("发送数据成功");
                while (true)
                {
                    Console.WriteLine("正在接收数据");
                    var revicesBytes = new byte[1000];
                    stream.Read(revicesBytes, 0, revicesBytes.Length);
                    Console.WriteLine(Encoding.Unicode.GetString(revicesBytes));
                    Console.WriteLine("输入Q退出聊天其他内容发送数据继续");
                    var flag = Console.ReadLine();
                    if (flag == null || flag.Contains("Q"))
                    {
                        stream.Close();
                        stream.Dispose();
                        return;
                    }
                    buBytes = Encoding.Unicode.GetBytes(flag);
                    stream.Write(buBytes, 0, buBytes.Length);
                    Console.WriteLine("发送数据成功");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        public static IPAddress GetServerIp()
        {
            var ipHost = Dns.GetHostEntry(Dns.GetHostName());
            return ipHost.AddressList[0];
        }
    }
}
