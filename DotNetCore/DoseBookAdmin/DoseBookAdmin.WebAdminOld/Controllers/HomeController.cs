using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoseBookAdmin.WebAdmin.Models;

namespace DoseBookAdmin.WebAdmin.Controllers
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

        /// <summary>
        /// Get Error view action
        /// </summary>
        /// <returns>Error View</returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
