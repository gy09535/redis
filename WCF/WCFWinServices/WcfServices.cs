using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using ServiceContract;
using Services;

namespace WCFWinServices
{
    public partial class WcfServices : ServiceBase
    {
        public WcfServices()
        {
            InitializeComponent();
        }

        private Thread _thread;

        private ServiceHost _host;
        protected override void OnStart(string[] args)
        {
            _thread = new Thread(
                () =>
                {
                    // 服务器基址  
                    // 声明服务器主机  
                    _host = new ServiceHost(typeof(ServiceSample));
                    #region 代码调用
                    // 添加绑定和终结点  
                    // tcp绑定支持会话  
                  /*  var binding = new NetTcpBinding { Security = { Mode = SecurityMode.None } };
                    _host.AddServiceEndpoint(typeof(IServiceSample), binding, "net.tcp://localhost:1211/IServiceSample");
                    // 添加服务描述  
                    try
                    {
                        // 打开服务  
                        _host.Open();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } 
                   */
                    #endregion

                    #region 配置文件调用
                    try
                    {
                        // 打开服务  
                        _host.Open();
                        Console.WriteLine("服务已启动。");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    } 
                    #endregion
                }
                );
            _thread.Start();
        }

        protected override void OnStop()
        {
            _thread.Join();
            _host.Close();
        }
    }
}
