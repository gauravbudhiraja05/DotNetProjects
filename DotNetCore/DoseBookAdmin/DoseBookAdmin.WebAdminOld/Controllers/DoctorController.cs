using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.WebAdmin.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class DoctorController : Controller
    {
        /// <summary>
        /// IDoctorService data member
        /// </summary>
        private IDoctorService _doctorService;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<DoctorController> _logger;

        /// <summary>
        /// DoctorController constructor
        /// </summary>
        /// <param name="enduserService"></param>
        public DoctorController(IDoctorService doctorService, ILogger<DoctorController> logger)
        {
            _doctorService = doctorService;
            _logger = logger;
        }

        [AutoPopulateLoggingDetails]
        public IActionResult Index()
        {
            return View(_doctorService.GetAllDoctors());
        }
    }
}