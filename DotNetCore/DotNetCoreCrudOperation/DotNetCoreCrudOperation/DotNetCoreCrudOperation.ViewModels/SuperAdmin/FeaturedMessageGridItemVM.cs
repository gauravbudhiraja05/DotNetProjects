namespace PickfordsIntranet.ViewModels.SuperAdmin
{
    /// <summary>
    /// Featured Message Grid Item represents a single row data
    /// </summary>
    public class FeaturedMessageGridItemVM
    {
        public int Id { get; set; }
        public string MessageCode { get; set; }
        public string Content { get; set; }
        public string CreationDate { get; set; }
        public bool Live { get; set; }
        public string AuthorName { get; set; }
    }
}
