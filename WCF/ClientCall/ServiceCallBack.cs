using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ServiceContract;

namespace ClientCall
{
    public class ServiceCallBack : ICallBackSample
    {

        public void CallBack(MessageContract.MessageSample message)
        {
             Console.WriteLine("这是WCF回调结果");
             Console.WriteLine("Message Header: "+ message.Header);
             Console.WriteLine("Message Text: "+ message.Text); 
        }
    }
}
