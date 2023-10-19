using AxisBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AxisBank.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserPanel()
        {
            return View();
        }

        public ActionResult CreateNewAccount()
        {
            ViewData["ROLEselectListItems"] = AppModels.RoleSelectListItemsByRoleId();
            return View();

        }

        [HttpPost]
        public ActionResult GetNewDetails(AxisBank_tblAllAccount all)
        {
            return View();
        }
    }
}