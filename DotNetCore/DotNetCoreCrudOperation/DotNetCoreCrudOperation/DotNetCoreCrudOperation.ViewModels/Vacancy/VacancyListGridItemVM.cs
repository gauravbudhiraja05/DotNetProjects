using PickfordsIntranet.ViewModels.Auth;
using System.Collections.Generic;

namespace PickfordsIntranet.ViewModels.Vacancy
{
    /// <summary>
    /// Admin user viewmodel for Admin Grid Row item
    /// </summary>
    public class VacancyListGridItemVM
    {
        public List<VacancyListGridItems> AllVacancyListGridItems { get; set; }
        public List<DepartmentVM> AllDepartments { get; set; }
        public DepartmentVM SelectedDepartmentDetails;
    }

    public class VacancyListGridItems
    {
        public int Id { get; set; }
        public string VacancyCode { get; set; }
        public string Title { get; set; }
        public string DepartmentName { get; set; }
        public string PublishDate { get; set; }
        public string CreationDate { get; set; }
        public string AuthorName { get; set; }
        public string PublishDateDisplay { get; set; }
        public string CreationDateDisplay { get; set; }
    }
}
