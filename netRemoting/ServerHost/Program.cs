using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace ServerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            RemotingConfiguration.ApplicationName = "CallbackRemoting";

            // 设置formatter
            BinaryServerFormatterSinkProvider formatter = new BinaryServerFormatterSinkProvider();
            formatter.TypeFilterLevel = TypeFilterLevel.Full;

            // 设置通道名称和端口
            IDictionary propertyDic = new Hashtable();
            propertyDic["name"] = "CustomTcpChannel";
            propertyDic["port"] = 8502;

            // 注册通道
            IChannel tcpChnl = new TcpChannel(propertyDic, null, formatter);
            ChannelServices.RegisterChannel(tcpChnl, false);

            // 注册类型
            Type t = typeof(Server);
            RemotingConfiguration.RegisterWellKnownServiceType(
                t, "ServerActivated", WellKnownObjectMode.Singleton);

            Console.WriteLine("Server running, model: Singleton\n");
            Console.ReadKey();
        }
    }
}
