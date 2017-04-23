using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account\Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
    }
}