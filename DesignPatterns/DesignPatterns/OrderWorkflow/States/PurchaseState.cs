using DesignPatterns.ApplyWorkflow.Builder;
using DesignPatterns.ApplyWorkflow.Handelers;
using DesignPatterns.OrderWorkflow.Builder;
using DesignPatterns.OrderWorkflow.Handelers;

namespace DesignPatterns.OrderWorkflow.States
{
    public class PurchaseState : IOrderState
    {
        public void ProcessHandel(ApplyContext orderContext)
        {
            //构造指导者
            var director = new PurchaseDirector();
            //构建制造者
            var handelBuilder = new ApplyBuilder();
            //指导者指导构造者构造责任链
            director.Construction(handelBuilder);
            //获取构造责任链
            var handel = handelBuilder.GetHanders();
            //责任链开始执行订购工作流 
            //orderContext.State = new RefundState();
            //handel.ProcessHandel(orderContext);
        }
    }
}
