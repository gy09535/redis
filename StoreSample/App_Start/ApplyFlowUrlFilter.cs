using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using DesignPatterns.ApplyWorkflow.Builder;
using Microsoft.Ajax.Utilities;
using StoreSample.Controllers;

namespace StoreSample
{
    /// <summary>
    /// 用于全局配置申请流的Url跳转
    /// </summary>
    public class ApplyFlowUrlFilter : ActionFilterAttribute
    {
        /// <summary>
        /// action执行之前根据具体的Url配置跳转页面
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
        }
    }
}