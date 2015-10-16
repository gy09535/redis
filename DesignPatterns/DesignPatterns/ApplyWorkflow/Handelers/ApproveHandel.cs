using System;
using DesignPatterns.ApplyWorkflow.Builder;
using DesignPatterns.OrderWorkflow.Handelers;

namespace DesignPatterns.ApplyWorkflow.Handelers
{
    [Order(1)]
    public class ApproveHandel : BaseHandeler
    {
        public override void ProcessHandel(ApplyContext applyContext)
        {
            if (applyContext.States == ApplyStates.Apply)
            {
                Console.WriteLine("同意申请");
                return;
            }
            Haneler.ProcessHandel(applyContext);
        }
    }
}
