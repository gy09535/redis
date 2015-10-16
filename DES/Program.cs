using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DESSample
{
    class Program
    {
        static void Main(string[] args)
        {

            var helper = new DesHelper("ASDADQA2WADAXAFF");
            var text = helper.Encrypt("测试加密算法");
            Console.WriteLine(text);
            Console.WriteLine(helper.Decrypt(text));
            Console.ReadKey();
        }
    }
}
