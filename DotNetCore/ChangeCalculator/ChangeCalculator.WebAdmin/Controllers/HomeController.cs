using ChangeCalculator.Core.DomainServices;
using ChangeCalculator.ViewModels.Calculator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChangeCalculator.WebAdmin.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// IGBPService data member
        /// </summary>
        private IGBPService _GBPService;

        /// <summary>
        /// IGBPService data member
        /// </summary>
        private IUSDService _USDService;

        /// <summary>
        /// IGBPService data member
        /// </summary>
        private IEURService _EURService;


        private ILogger<HomeController> _logger;

        public HomeController(IGBPService GBPService, IUSDService USDService, IEURService EURService, ILogger<HomeController> logger)
        {
            _GBPService = GBPService;
            _USDService = USDService;
            _EURService = EURService;
            _logger = logger;
        }

        public ActionResult Index()
        {
            Calculator calculator = new Calculator();
            return View(calculator);
        }

        [HttpPost]
        public IActionResult CurrencyCalculator([Bind] Calculator calculator)
        {
            Calculator calc = new Calculator();
            if (calculator.CurrencyType == "GBP")
            {
                calc = _GBPService.GetGBPDenominations(calculator);
            }
            else if(calculator.CurrencyType == "USD")
            {
                calc = _USDService.GetUSDDenominations(calculator);
            }
            else if(calculator.CurrencyType == "EUR")
            {
                calc = _EURService.GetEURDenominations(calculator);
            }
            return View(calc);
        }

        [HttpGet]
        public IActionResult OtherSection()
        {
            return View();
        }
    }
}
