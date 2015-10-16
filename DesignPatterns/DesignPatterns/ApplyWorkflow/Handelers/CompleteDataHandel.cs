using System;
using DesignPatterns.ApplyWorkflow.Builder;
using DesignPatterns.OrderWorkflow.Handelers;

namespace DesignPatterns.ApplyWorkflow.Handelers
{
    [Order(2)]
    public class CompleteDataHandel : BaseHandeler
    {
        public override void ProcessHandel(ApplyContext applyContext)
        {
            if (applyContext.States == ApplyStates.CompleteData)
            {
                Console.WriteLine("资料已经完成");
                return;
            }
            if (Haneler != null)
            {
                Haneler.ProcessHandel(applyContext);
            }
        }
    }
}
