using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketAsyncServer
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
    class Program
    {
        static void Main(string[] args)
        {
            var clientSk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            clientSk.BeginConnect(new IPEndPoint(IPAddress.Parse("10.1.200.83"), 8596), ConnectComplete, new MyObject(clientSk, "开始连接了"));

            Console.ReadLine();
            Thread.Sleep(TimeSpan.FromHours(1));
        }

        static void ConnectComplete(IAsyncResult ar)
        {
            var myObj = (MyObject)ar.AsyncState;
            var clientSk = myObj.Socket;
            var msg = Console.ReadLine();

            Console.WriteLine("开始发送数据");
            var datas = System.Text.Encoding.UTF8.GetBytes(msg ?? string.Empty);
            clientSk.BeginSend(datas, 0, datas.Length, SocketFlags.None, new AsyncCallback(SendComplete), new MyObject(clientSk, "开始发送了", datas));


            var buffer = new byte[2048]; //虽然不知道会收到多少，但是多了总比少了好。

            clientSk.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(RecievedComplete), new MyObject(clientSk, "开始接收了", buffer));

        }

        static void SendComplete(IAsyncResult ar)
        {
            var myObj = (MyObject)ar.AsyncState;
            var sk = myObj.Socket;

            var sended = sk.EndSend(ar);

            Console.WriteLine(System.Text.Encoding.UTF8.GetString(myObj.Buffer));
        }

        static void RecievedComplete(IAsyncResult ar)
        {

            var myObj = (MyObject)ar.AsyncState;
            var sk = myObj.Socket;

            var recieved = sk.EndReceive(ar);

            Console.WriteLine(System.Text.Encoding.UTF8.GetString(myObj.Buffer));
            sk.BeginSend(myObj.Buffer, 0, myObj.Buffer.Length, SocketFlags.None, new AsyncCallback(SendComplete), sk);
        }
    }
}
