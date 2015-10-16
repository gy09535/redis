using System.Collections.Generic;
using System.Linq;
using DesignPatterns.ApplyWorkflow.Handelers;
using DesignPatterns.OrderWorkflow.Handelers;

namespace DesignPatterns.ApplyWorkflow.Builder
{
    /// <summary>
    /// 责任链构造者
    /// </summary>
    public class ApplyBuilder : IBuilder
    {

        #region Property
        private readonly List<BaseHandeler> _handelers;
        public List<BaseHandeler> Handers
        {
            get { return _handelers; }
        }
        #endregion

        #region Method
        /// <summary>
        /// 构造处理链集合
        /// </summary>
        public ApplyBuilder()
        {
            _handelers = new List<BaseHandeler>();
        }

        /// <summary>
        /// 建立处理链
        /// </summary>
        /// <param name="obj"></param>
        public void BuilderHandel(BaseHandeler obj)
        {
            _handelers.Add(obj);
        }

        /// <summary>
        /// 将生产的处理链暴露出去
        /// </summary>
        /// <returns></returns>
        public BaseHandeler GetHanders()
        {
            return _handelers.FirstOrDefault();
        }
        #endregion
    }
}
