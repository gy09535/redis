using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using MessageContract;

namespace ServiceContract
{
    public interface ICallBackSample
    {
        [OperationContract]
        void CallBack(MessageSample message);
    }
}
