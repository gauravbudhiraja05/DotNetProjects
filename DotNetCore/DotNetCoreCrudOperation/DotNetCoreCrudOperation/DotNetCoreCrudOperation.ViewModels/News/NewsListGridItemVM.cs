using PickfordsIntranet.ViewModels.Auth;
using System.Collections.Generic;

namespace PickfordsIntranet.ViewModels.News
{
    /// <summary>
    /// Admin user viewmodel for Admin Grid Row item
    /// </summary>
    public class NewsListGridItemVM
    {
        public List<NewsListGridItems> AllNewsListGridItems { get; set; }
        public List<DepartmentVM> AllDepartments { get; set; }
        public DepartmentVM SelectedDepartmentDetails;
        public string UserType { get; set; }
    }

    public class NewsListGridItems
    {
        public int Id { get; set; }
        public string NewsCode { get; set; }
        public string Title { get; set; }
        public string DepartmentName { get; set; }
        public string PublishDate { get; set; }
        public string CreationDate { get; set; }
        public string AuthorName { get; set; }
        public string PublishDateDisplay { get; set; }
        public string CreationDateDisplay { get; set; }
    }
}
