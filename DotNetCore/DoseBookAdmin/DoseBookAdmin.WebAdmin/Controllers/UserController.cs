using DoseBookAdmin.Core.DomainServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// IDoctorService data member
        /// </summary>
        private IUserService _userService;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<UserController> _logger;

        /// <summary>
        /// UserController constructor
        /// </summary>
        /// <param name="enduserService"></param>
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_userService.GetAllUsers());
        }
    }
}
