using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Text;
using RemotingApp;

namespace ClientCall
{
    class Program
    {
        private const string _ip = "10.1.200.39";

        static void Main(string[] args)
        {
            try
            {
                // ClientActivated();
                ServerSingleCall();
                //ServerSingleton();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }


        private static void RegisterServerSingleCall()
        {
            RemotingConfiguration.RegisterWellKnownClientType(typeof(DemoClass), "tcp://" + _ip + ":8501/DemoApp/SingleCall");
        }

        private static void ServerSingleCall()
        {
            RegisterServerSingleCall();
            Console.WriteLine("----开始调用服务端SingleCall激活服务----------");

            const string url = "tcp://" + _ip + ":8501/DemoApp/SingleCall";

            DemoClass objClass1 = (DemoClass)Activator.GetObject(typeof(DemoClass), url);
            objClass1.ShowAppDomain();
            objClass1.ShowCount("Gy");

            DemoClass objClass2 = (DemoClass)RemotingServices.Connect(typeof(DemoClass), url);
            objClass1.ShowAppDomain();
            objClass1.ShowCount("Gy");
        }


        private static void RegisterServerSingleton()
        {
            RemotingConfiguration.RegisterWellKnownClientType(typeof(DemoClass), "tcp://" + _ip + ":8501/DemoApp/ServerSingleton");
        }

        private static void ServerSingleton()
        {
            RegisterServerSingleton();
            Console.WriteLine("----开始调用服务端ServerSingleton激活服务----------");

            const string url = "tcp://" + _ip + ":8501/DemoApp/ServerSingleton";

            DemoClass objClass1 = (DemoClass)Activator.GetObject(typeof(DemoClass), url);
            objClass1.ShowAppDomain();
            objClass1.ShowCount("Gy");

            DemoClass objClass2 = (DemoClass)RemotingServices.Connect(typeof(DemoClass), url);
            objClass1.ShowAppDomain();
            objClass1.ShowCount("Gy");

        }


        private static void RegisterClientActivated()
        {
            RemotingConfiguration.RegisterActivatedClientType(typeof(DemoClass), "tcp://" + _ip + ":8501/");
        }

        private static void ClientActivated()
        {
            RegisterClientActivated();
            Console.WriteLine("----开始调用客户端激活服务服务----------");
            /*DemoClass objClass = new DemoClass();
            objClass.ShowAppDomain();
            objClass.ShowCount("Gy");*/

            DemoClass objClass1 = (DemoClass)Activator.CreateInstance(typeof(DemoClass), null, new object[] { new UrlAttribute("tcp://" + _ip + ":8501/DemoApp/") });
            objClass1.ShowAppDomain();
            objClass1.ShowCount("Gy");
        }
    }
}
