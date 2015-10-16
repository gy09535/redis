using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace StoreSample.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Login/

        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName)
        {
            try
            {
                //预留登陆验证
                //Create ticket
                var ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(30), false, "");
                //Encrypt the ticket.
                var encTicket = FormsAuthentication.Encrypt(ticket);
                //Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                //Redirect back to default or original URL.
                var requestUrl = FormsAuthentication.GetRedirectUrl(userName, true);

                return Redirect(requestUrl.Contains("weilvyou.ly.com") ? "" : requestUrl);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Login()
        {
            var lists = ManageNodes();
            var serializer = new JavaScriptSerializer();
            ViewBag.Nodes = serializer.Serialize(lists);
            return View();
        }

        [HttpPost]
        [HandleError]
        public ActionResult LoginOff()
        {
            try
            {
                FormsAuthentication.SignOut();
                //预留清空 Session 值
                return RedirectToAction("Login");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
