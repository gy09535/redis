using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDemo
{
    class Program
    {
        /*
         * 1.应用程序域的加载概念.
         * 借例: IIS 加载过程解释何为应用程序域   
         *     1. web  接受到请求检查访问类型
         *        (1) 静态类型直接返回.
         *        (2) 动态类型经过ISAPI(动态链接库)创建工作进程(asp.net进程) 加载CLR(公共语言运行时.net最核心的一部分),创建
         *     应用程序域(所有的请求处理都在一个应用程序域中),加载httpruntime,进行请求处理.
         *     
         * 2.如何实现跨应用程序域通信(WCF).
         *     (1) msmq,采用消息队列达到应用程序域直接通信.
         *     (2) webservices,采用web服务SOA的通信协议来进行通信.
         *     (3) remoting,采用对远程对象的引用来进行跨应用程序域通信. 
         */
        /// <summary>
        /// 例如何采用进行跨应用程序域通信.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //获取当前应用程序域
            try
            {
                AppDomain currentDomain = AppDomain.CurrentDomain;

                #region 默认应用程序域中创建对象
                //创建对象
                DemoClass objDemoClass = new DemoClass();

                // objDemoClass = (DemoClass)AppDomain.CurrentDomain.CreateInstance("AppDemo", "AppDemo.DemoClass").Unwrap();
                // objDemoClass = (DemoClass)AppDomain.CurrentDomain.CreateInstanceAndUnwrap("AppDemo", "AppDemo.DemoClass"); 
                #endregion

                #region 新应用程序域中创建对象
                //传值封送,将当前对象在新应该程序域中的状态通过序列化从新的应用程序域中传入到当前应用程序域中.

                AppDomain domain = AppDomain.CreateDomain("NewDomain");
                objDemoClass = (DemoClass)domain.CreateInstanceAndUnwrap("AppDemo", "AppDemo.DemoClass");

                //为何显示的域名是当前域名?
                //答案 传值封送. 
                objDemoClass.ShowAppDomain();
                objDemoClass.ShowCount("Gy");
                objDemoClass.ShowCount("Gy");

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.ReadLine();
        }
    }
}
