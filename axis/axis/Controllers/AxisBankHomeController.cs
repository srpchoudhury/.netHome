using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace axis.Controllers
{
    public class AxisBankHomeController : Controller
    {
        // GET: AxisBankHome
        public ActionResult Landing()
        {
            return View();
        }


        //Login Form Action
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }
    }
}