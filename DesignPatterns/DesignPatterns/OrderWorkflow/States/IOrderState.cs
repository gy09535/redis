using DesignPatterns.ApplyWorkflow.Handelers;
using DesignPatterns.OrderWorkflow.Handelers;

namespace DesignPatterns.OrderWorkflow.States
{
    public interface IOrderState
    {
        void ProcessHandel(ApplyContext orderContext);
    }
}
