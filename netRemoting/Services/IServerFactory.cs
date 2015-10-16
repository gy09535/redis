using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemotingApp;

namespace Services
{
    public interface IServerFactory
    {
        IDemoClass GetDemoClass();
    }
}
