using System;
using DesignPatterns.ApplyWorkflow.Builder;
using DesignPatterns.OrderWorkflow.Handelers;

namespace DesignPatterns.ApplyWorkflow.Handelers
{
    /// <summary>
    /// 提交保证金处理
    /// </summary>
    [Order(3)]
    class SubmitDepositHandel : BaseHandeler
    {
        public override void ProcessHandel(ApplyContext applyContext)
        {
            if (applyContext.States == ApplyStates.SubmitDeposit)
            {
                Console.WriteLine("保证金已经提交");
            }
            if (Haneler != null)
            {
                Haneler.ProcessHandel(applyContext);
            }
        }
    }
}
