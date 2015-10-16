using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemotingApp
{

    public interface IDemoClass
    {
        void ShowCount(string name);
        void ShowAppDomain();
        int GetCount();
    }
}
