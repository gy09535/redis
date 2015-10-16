using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreSample.Controllers
{
    public class StoreManageController : BaseController
    {
        //
        // GET: /StoreManage/

        public ActionResult StoreMain()
        {
            return View();
        }

        public ActionResult CompleteInformation()
        {
            return View();
        }

    }
}
