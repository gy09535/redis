using DesignPatterns.ApplyWorkflow.Handelers;
using DesignPatterns.OrderWorkflow.Handelers;

namespace DesignPatterns.ApplyWorkflow.Builder
{
    /// <summary>
    /// 指导者
    /// </summary>
    public class PurchaseDirector
    {
        #region Method
        /// <summary>
        ///指导者构造者构造职责链
        /// </summary>
        /// <param name="builder"></param>
        public void Construction(IBuilder builder)
        {
            //构造审核通过处理链
            var approve = new ApproveHandel();
            //完善资料处理链
            var complete = new CompleteDataHandel();
            //提交保证金
            var submit = new SubmitDepositHandel();

            builder.BuilderHandel(approve);
            builder.BuilderHandel(complete);
            builder.BuilderHandel(submit);

            var listHandel = builder.Handers;
            //使用默认排序接口对责任类排序
            listHandel.Sort();

            //设置责任链的处理顺序
            for (var i = 0; i < listHandel.Count - 1; i++)
            {
                var obj = listHandel[i];
                var objNext = listHandel[i + 1];
                obj.SetHaners(objNext);
            }
        }
        #endregion
    }
}
