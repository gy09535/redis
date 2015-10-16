using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketAsyncClient
{
    class Program
    {

        public class MyObject
        {
            #region 类字段

            private Socket _Socket;
            private string _MyName = "";
            private byte[] _Buffer;


            #endregion

            #region 类属性

            public Socket Socket
            {
                get { return _Socket; }
                set { _Socket = value; }
            }


            public byte[] Buffer
            {
                get { return _Buffer; }
                set { _Buffer = value; }
            }


            public string MyName
            {
                get { return _MyName; }
                set { _MyName = value; }
            }

            #endregion

            #region 构造函数

            public MyObject(Socket socket, string myName)
            {
                _Socket = socket;
                _MyName = myName;
            }
            public MyObject(Socket socket, string myName, byte[] buffer)
            {
                _Socket = socket;
                _MyName = myName;
                _Buffer = buffer;
            }

            #endregion
        }
        static void Main(string[] args)
        {
            var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(new IPEndPoint(IPAddress.Parse("10.1.200.83"), 8596));
            server.Listen(1);

            server.BeginAccept(new AsyncCallback(AcceptComplete), new MyObject(server, "开始连接了"));

            Console.ReadLine();
            Thread.Sleep(TimeSpan.FromHours(1));
        }

        static void AcceptComplete(IAsyncResult ar)
        {
            var myObj = (MyObject)ar.AsyncState;
            var serverSk = myObj.Socket.EndAccept(ar);

            var buffer = new byte[2048]; //虽然不知道会收到多少，但是多了总比少了好。

            //  serverSk.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(RecievedComplete), new MyObject(serverSk, "开始接收了", buffer));
            //Console.WriteLine(System.Text.Encoding.UTF8.GetString(myObj.Buffer));
            Console.WriteLine("Send Message");
            var datas = System.Text.Encoding.UTF8.GetBytes(Console.ReadLine() ?? string.Empty);
            serverSk.BeginSend(datas, 0, datas.Length, SocketFlags.None, SendComplete, new MyObject(serverSk, "开始发送了", datas));

        }

        static void SendComplete(IAsyncResult ar)
        {
            var myObj = (MyObject)ar.AsyncState;
            var sk = myObj.Socket;

            var sended = sk.EndSend(ar);

            Console.WriteLine("Send OK");
            sk.BeginReceive(myObj.Buffer, 0, myObj.Buffer.Length, SocketFlags.None, RecievedComplete, new MyObject(sk, "开始发送了", myObj.Buffer));
        }

        static void RecievedComplete(IAsyncResult ar)
        {

            var myObj = (MyObject)ar.AsyncState;
            var sk = myObj.Socket;

            var recieved = sk.EndReceive(ar);

            Console.WriteLine(System.Text.Encoding.UTF8.GetString(myObj.Buffer));
            var date = System.Text.Encoding.UTF8.GetBytes(Console.ReadLine() ?? string.Empty);
            sk.BeginSend(date, 0, date.Length, SocketFlags.None, SendComplete, myObj);
        }
    }
}


