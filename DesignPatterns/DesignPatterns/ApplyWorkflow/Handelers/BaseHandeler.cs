using System;
using System.Linq;
using DesignPatterns.OrderWorkflow.Handelers;

namespace DesignPatterns.ApplyWorkflow.Handelers
{
    public abstract class BaseHandeler : IComparable
    {
        #region Property
        protected BaseHandeler Haneler;
        #endregion

        #region Method
        /// <summary>
        /// 设置处理链的下一个处理链
        /// </summary>
        /// <param name="handeler"></param>
        public void SetHaners(BaseHandeler handeler)
        {
            this.Haneler = handeler;
        }

        ///  <summary>
        /// 处理当前链的逻辑 
        ///  </summary>
        public abstract void ProcessHandel(ApplyContext applyContext);

        /// <summary>
        /// 对处理链进行排序
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            var others = (ApplyWorkflow.Handelers.OrderAttribute)obj.GetType().GetCustomAttributes(typeof(ApplyWorkflow.Handelers.OrderAttribute), false).FirstOrDefault();
            var current = (ApplyWorkflow.Handelers.OrderAttribute)this.GetType().GetCustomAttributes(typeof(ApplyWorkflow.Handelers.OrderAttribute), false).FirstOrDefault();
            return others != null && (current != null && current.Order > others.Order) ? 1 : -1;
        }
        #endregion
    }
}
