using axis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace axis.Controllers
{
    public class UserController : Controller
    {
        UserRegistrationModel userRegistrationModel = new UserRegistrationModel();
        // GET: User
        public ActionResult UserPanel()
        {
            return View(userRegistrationModel);
        }
    }
}