using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DataContract;
using MessageContract;
using ServiceContract;

namespace Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ServiceSample : IServiceSample
    {
        [OperationBehavior]
        public void UserMessage(UserBo userBo)
        {
            var message = new MessageSample()
            {
                Header = "Wcf Back Message",
                Text = userBo.Name + "," + userBo.Description
            };
            //消息传递给客户端并且调用客户的回调方法处理结果
            var callBack = OperationContext.Current.GetCallbackChannel<ICallBackSample>();
            callBack.CallBack(message);
        }
    }
}
