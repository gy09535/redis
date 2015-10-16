using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DataContract;
using MessageContract;

namespace ServiceContract
{
    [ServiceContract(CallbackContract = typeof(ICallBackSample))]
    public interface IServiceSample
    {
        [OperationContract(IsOneWay = true)]
        void UserMessage(UserBo dContract);
    }
}
