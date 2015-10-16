using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppDemo
{
    [Serializable]
    public class DemoClass
    {
        private int _count = 0;

        public DemoClass()
        {
            Console.WriteLine("------DemoClass Constructor--------");
        }

        public void ShowCount(string name)
        {
            _count++;
            Console.WriteLine("{0},the count is {1}", name, _count);
        }

        public void ShowAppDomain()
        {
            Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
