using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayoutInMvc.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        [Route("Employees")]
        public ActionResult GetList()
        {
            return View();
        }

        [Route("NewEmployees")]
        public ActionResult AddEmployee()
        {
            return View();
        }
    }
}