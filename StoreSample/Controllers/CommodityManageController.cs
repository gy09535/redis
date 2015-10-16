using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreSample.Controllers
{
    public class CommodityManageController : BaseController
    {
        //
        // GET: /CommodityManage/

        #region NewCommodity
        public ActionResult AddCommodity()
        {
            return View();
        }

        public ActionResult CompleteData()
        {
            return View();
        }

        public ActionResult SubmitDeposit()
        {
            return View();
        }

        public ActionResult CompleteAddCommodity()
        {
            return View();
        }
        #endregion

        #region MyCommodity

        public ActionResult MyCommodity()
        {
            return View();
        }

        #endregion
    }
}
