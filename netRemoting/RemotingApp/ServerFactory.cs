using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Services;

namespace RemotingApp
{
    public class ServerFactory : MarshalByRefObject, IServerFactory
    {
        public IDemoClass GetDemoClass()
        {
            return new DemoClass();
        }
    }
}
