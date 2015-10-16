using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using ShareAssembly;

namespace ClientCall
{
    class Program
    {
        static void Main(string[] args)
        {
            IChannel chnl = new TcpChannel(0);
            ChannelServices.RegisterChannel(chnl, false);

            // 注册类型
            Type t = typeof(Server);
            string url = "tcp://10.1.200.39:8502/CallbackRemoting/ServerActivated";
            RemotingConfiguration.RegisterWellKnownClientType(t, url);

            Server remoteServer = new Server(); // 创建远程对象
            Client localClient = new ShareAssembly.Client();  // 创建本地对象

            // 注册远程对象事件
            remoteServer.NumberChanged +=
                new NumberChangedEventHandler(localClient.OnNumberChanged);

            remoteServer.DoSomething();             // 触发事件
            remoteServer.GetCount(localClient);     // 调用GetCount()
            remoteServer.InvokeClient(localClient, 2, 5);// 调用InvokeClient()

            Console.ReadKey();  // 暂停客户端
        }
    }
}
