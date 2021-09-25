using Dream14.WebAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Dream14.WebAdmin.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// Get Dashboard view action
        /// </summary>
        /// <returns>Dashboard View</returns>
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return PartialView("_ChangePassword");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
