using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentTracker.Site.Controllers {
    public class AccountController : Controller {
        public ActionResult Login(string ReturnUrl) {
            return RedirectToAction("Logon", "User", new { ReturnUrl = ReturnUrl });
        }
    }
}
