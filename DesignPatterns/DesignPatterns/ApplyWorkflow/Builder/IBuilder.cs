using System.Collections.Generic;
using DesignPatterns.ApplyWorkflow.Handelers;
using DesignPatterns.OrderWorkflow.Handelers;

namespace DesignPatterns.ApplyWorkflow.Builder
{

    /// <summary>
    /// 构造责任链
    /// </summary>
    public interface IBuilder
    {
        List<BaseHandeler> Handers { get; }
        void BuilderHandel(BaseHandeler obj);
        BaseHandeler GetHanders();
    }
}
