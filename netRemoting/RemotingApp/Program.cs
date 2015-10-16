using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Text;
using System.Runtime.Remoting.Channels.Tcp;

namespace RemotingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //1.传引用封送（CAO客户端激活).同一进程不同应用程序域中,如果在不同应用程序,不同进程中又该怎么写,客户端激活存在的缺点?
                //2.调用的过程是什么?由此你联想到了什么(举一反三)?通过她来做一些什么有意义的事情?
                AppDomain domain = AppDomain.CreateDomain("NewDomain");
                DemoClass objDemoClass = (DemoClass)domain.CreateInstanceAndUnwrap("Services", "RemotingApp.DemoClass");

                ////为何显示的域名是当前域名?
                ////答案 传值封送. 
                objDemoClass.ShowAppDomain();
                objDemoClass.ShowCount("Gy");
                objDemoClass.ShowCount("Gy");
                Console.ReadLine();
                #region 答案
                /*答案:              
             *    1.调用过程,客户端通过代理和服务端进行通信,代理知道远程对象的位置，当客户端使用这个对象的时候
             * 托管环境(CLR)会创建透明代理(真正对用户来将是绝缘的看不见的),通过透明代理将调用信息转换为消息,再又真实代理对
             * 消息进行编码,将编码消息发送给远程服务器通道,远程服务器接受到消息创建对象,进行调用.
             * */

                RemotingConfiguration.ApplicationName = "DemoApp";
                ServerActivatedSingleton();
                RegisterServerActivatedSingleton();

                //ServcerActivatedSingleCall();
                //RegisterServerActivatedSingleCall();

                //Type t = typeof(DemoClass);
                //RemotingConfiguration.RegisterActivatedServiceType(t);

                #endregion
                Console.WriteLine("服务已开启,安任意键退出.....");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();


        }


        /// <summary>
        /// 服务端激活单体
        /// </summary>
        private static void ServerActivatedSingleton()
        {
            //单体需要考虑的问题?
            IChannelReceiver receiver = new TcpChannel(8501);
            ChannelServices.RegisterChannel(receiver, false);
        }


        private static void RegisterServerActivatedSingleton()
        {
            Type t = typeof(DemoClass);
            RemotingConfiguration.RegisterWellKnownServiceType(t, "ServerSingleton", WellKnownObjectMode.Singleton);
        }

        private static void ServcerActivatedSingleCall()
        {
            //singleCall 存在的问题?
            IChannelReceiver receiver = new HttpChannel(8502);
            ChannelServices.RegisterChannel(receiver, false);

            IServerChannelSinkProvider provider = new BinaryServerFormatterSinkProvider();
            IDictionary prValues = new Hashtable();
            prValues["name"] = "CustomPort";
            prValues["port"] = 8053;

            IChannel channel = new TcpChannel(prValues, null, provider);
            ChannelServices.RegisterChannel(channel, false);
        }


        private static void RegisterServerActivatedSingleCall()
        {
            Type t = typeof(DemoClass);
            RemotingConfiguration.RegisterWellKnownServiceType(t, "SingleCall", WellKnownObjectMode.SingleCall);
        }
    }
}
