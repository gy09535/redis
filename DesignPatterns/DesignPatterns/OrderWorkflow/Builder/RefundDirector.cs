using DesignPatterns.ApplyWorkflow.Builder;
using DesignPatterns.ApplyWorkflow.Handelers;
using DesignPatterns.OrderWorkflow.Handelers;

namespace DesignPatterns.OrderWorkflow.Builder
{
    public class RefundDirector
    {
        #region Method
        /// <summary>
        ///指导者构造者构造职责链
        /// </summary>
        /// <param name="builder"></param>
        public void Construction(IBuilder builder)
        {
            //构造退款处理链
            var objA = new SubmitDepositHandel();
            var objB = new RefundHandelB();
            builder.BuilderHandel(objA);
            builder.BuilderHandel(objB);

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
