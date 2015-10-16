using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreSample.Controllers
{
    public class DealManageController : Controller
    {
        //
        // GET: /DealManage/

        public ActionResult OrderManage()
        {
            return View();
        }

        public ActionResult AccountBalance()
        {
            return View();
        }


        public ActionResult MessageManage()
        {
            return View();
        }
    }

}
