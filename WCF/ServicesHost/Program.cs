using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using ServiceContract;
using Services;

namespace ServicesHost
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Wcf
            // 服务器基址  
            // 声明服务器主机  
            /*     var host = new ServiceHost(typeof(ServiceSample));
                  #region 代码发布
                  // 添加绑定和终结点  
                  // tcp绑定支持会话  
                  /*  var binding = new NetTcpBinding { Security = { Mode = SecurityMode.None } };
                    host.AddServiceEndpoint(typeof(IServiceSample), binding, "net.tcp://localhost:1211/ServiceSample");
                    // 添加服务描述  
                    try
                    {
                        // 打开服务  
                        host.Open();
                        Console.WriteLine("服务已启动。");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                    host.Close();
                    Console.WriteLine("服务已关闭");
            
                  #endregion
             

                  #region 配置文件发布
                  try
                  {
                      // 打开服务  
                      host.Open();
                      Console.WriteLine("服务已启动。");
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine(ex.Message);
                  }
                  Console.ReadKey();
                  host.Close();
                  Console.WriteLine("服务已关闭"); 
                  #endregion*/

            #endregion

        }
    }
}
