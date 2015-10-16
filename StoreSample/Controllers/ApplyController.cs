using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DesignPatterns.ApplyWorkflow.Builder;

namespace StoreSample.Controllers
{
    public class ApplyController : BaseController
    {
        //
        // GET: /Apply/

        /// <summary>
        /// 申请主页面
        /// </summary>
        /// <returns></returns>
        [HandleError]
        public ActionResult ApplyMain()
        {
            //如果状态是申请通过请求此页面将重定向到
            try
            {
                if (States != ApplyStates.Apply || States != ApplyStates.ApproveFail)
                {
                    throw new Exception("没有权限访问此页");
                }
                return View();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult ApplyApproving()
        {
            return View();
        }


        public ActionResult ApplyFail()
        {
            return View();
        }
    }
}
