using System;

namespace DesignPatterns.ApplyWorkflow.Handelers
{
    /// <summary>
    /// 处理链处理排序
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class OrderAttribute : Attribute
    {
        private readonly int _order;
        public int Order
        {
            get { return _order; }
        }
        public OrderAttribute(int order)
        {
            _order = order;
        }
    }
}
