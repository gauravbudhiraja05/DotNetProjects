namespace PickfordsIntranet.ViewModels.FAQS
{
    using System;
    /// <summary>
    /// Admin user viewmodel for Admin Grid Row item
    /// </summary>
    public class FaqsGridItemVM
    {
        public int Id {get;set;}
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime PublishDate { get; set; }
        public string AuthorName { get; set; }
        public string CreationDateString { get; set; }
        public string PublishDateString { get; set; }
        public int CreatedBy { get; set; }
        public bool ISEditable_Deletable { get; set; }
    }
}
