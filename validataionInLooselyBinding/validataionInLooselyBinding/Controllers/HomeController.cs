using System.Web.Mvc;
using validataionInLooselyBinding.Models;

namespace validataionInLooselyBinding.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubmitData(Employee emp)
        {
            if(ModelState.IsValid)
            {
                return View();
            }
            return View("Index");
        }
    }
}