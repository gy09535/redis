using System;
using DesignPatterns.ApplyWorkflow.Handelers;

namespace DesignPatterns.OrderWorkflow.Handelers
{
    [ApplyWorkflow.Handelers.OrderAttribute(1)]
    public class RefundHandelB : BaseHandeler
    {
        public override void ProcessHandel(ApplyContext orderContext)
        {
            Console.WriteLine("拒绝 handeler B 正在处理");
            if (Haneler != null)
            {
                Haneler.ProcessHandel(orderContext);
            }
        }
    }
}
