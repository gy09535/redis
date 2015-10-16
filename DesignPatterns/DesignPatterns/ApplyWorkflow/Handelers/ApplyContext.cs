using DesignPatterns.ApplyWorkflow.Builder;
using DesignPatterns.OrderWorkflow.States;

namespace DesignPatterns.ApplyWorkflow.Handelers
{
    public class ApplyContext
    {
        /// <summary>
        /// 申请的状态
        /// </summary>
        public ApplyStates States { get; set; }
    }
}
