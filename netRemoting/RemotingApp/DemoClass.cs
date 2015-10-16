//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace RemotingApp
//{
//    [Serializable]
//    public class DemoClass : MarshalByRefObject
//    {
//        private int _count = 0;

//        public DemoClass()
//        {
//            Console.WriteLine("------DemoClass Constructor--------");
//        }

//        public void ShowCount(string name)
//        {
//            _count++;
//            Console.WriteLine("{0},the count is {1}", name, _count);
//        }

//        public void ShowAppDomain()
//        {
//            Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
//        }



//        public int GetCount()
//        {
//            return _count;
//        }
//    }
//}
