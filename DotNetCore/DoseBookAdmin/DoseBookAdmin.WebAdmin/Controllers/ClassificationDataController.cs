using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.ViewModels.ClassificationData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class ClassificationDataController : Controller
    {

        /// <summary>
        /// IDoseDataService data member
        /// </summary>
        private IClassificationDataService _classificationDataService;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<ClassificationDataController> _logger;

        /// <summary>
        /// DoctorController constructor
        /// </summary>
        /// <param name="enduserService"></param>
        public ClassificationDataController(IClassificationDataService classificationDataService, ILogger<ClassificationDataController> logger)
        {
            _classificationDataService = classificationDataService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_classificationDataService.GetAllClassificationTypes());
        }

        public ActionResult GetClassificationDataByClassificationTypeWise(int id)
        {
            ClassificationTypeListGridItemVM classificationTypeListGridItemVM = _classificationDataService.GetClassificationDataByClassificationTypeWise(id);
            return PartialView("_ClassificationDataList", classificationTypeListGridItemVM);
        }       
    }
}
