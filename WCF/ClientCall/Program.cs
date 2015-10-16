using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using DataContract;
using ServiceContract;

namespace ClientCall
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceCall();
            }
            catch (Exception ex)
            {
                Console.WriteLine("服务调用失败: "+ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        static private void ServiceCall()
        {
            var context = new InstanceContext(new ServiceCallBack());
            #region 代码绑定
            /*  using (var channelfactory = new DuplexChannelFactory<IServiceSample>(context, new NetTcpBinding(){Security ={Mode = SecurityMode.None} }))
            {
                var services = channelfactory.CreateChannel(new EndpointAddress("net.tcp://localhost:1211/ServiceSample"));
                if (services != null)
                {
                    services.UserMessage(new UserBo() { Name = "guyang", Description = "This is just test for wcf servces" });
                    Console.WriteLine("服务已经调用正在做其他的事情");
                    Console.WriteLine("主线程名称:" + Thread.CurrentThread.Name);
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }
                Console.WriteLine("服务调用失败");
                Console.ReadLine();
            }
           */
            #endregion

            #region 配置文件绑定
            using (var channelfactory = new DuplexChannelFactory<IServiceSample>(context, "Client"))
            {
                var services = channelfactory.CreateChannel();
                if (services != null)
                {
                    services.UserMessage(new UserBo() { Name = "guyang", Description = "This is just test for wcf servces" });
                    Console.WriteLine("服务已经调用正在做其他的事情");
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                    Console.ReadLine();
                }
                Console.WriteLine("服务调用失败");
                Console.ReadLine();
            }
            #endregion


        }
    }
}
