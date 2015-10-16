using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareAssembly
{
    public class Client : MarshalByRefObject
    {
        private int count = 0;

        // 方式1：供远程对象调用
        public int Add(int x, int y)
        {
            // 当有服务端调用时，打印下面一行
            Console.WriteLine("Add callback: x={0}, y={1}.", x, y);
            return x + y;
        }

        // 方式1：供远程对象调用
        public int Count
        {
            get
            {
                count++;
                return count;
            }
        }

        // 方式2：订阅事件，供远程对象调用
        public void OnNumberChanged(string serverName, int mycount)
        {
            Console.WriteLine("OnNumberChanged callback:");
            Console.WriteLine("ServerName={0}, Server.Count={1}", serverName, mycount);
        }
    }
}
