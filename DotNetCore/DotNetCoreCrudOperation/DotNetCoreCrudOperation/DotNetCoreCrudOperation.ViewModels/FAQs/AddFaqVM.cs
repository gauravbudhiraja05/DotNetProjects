using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PickfordsIntranet.ViewModels.Global;

namespace PickfordsIntranet.ViewModels.FAQs
{
    public class AddFaqVM
    {
        public int QuestionId { get; set; }

        [MaxLength(300, ErrorMessage = "The question text should not be greater than 300 characters.")]
        [Required(ErrorMessage = "Please enter the question text.")]
        public string QuestionText { get; set; }

        //[Required(ErrorMessage = "Enter the answer text.")]
        public string AnswerText { get; set; }

        [MaxLength(40, ErrorMessage = "The author name should not be greater than 40 characters.")]
        [Required(ErrorMessage = "Please enter the author name.")]
        public string AuthorName { get; set; }

        public string DocumentIds { get; set; }

        public List<DocumentId> ListOfDocumentId { get; set; }

        public List<DocumentDetail> AttachDocuments { get; set; }

        public string CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

       

        public DateTime PublishDate { get; set; }
        [Required(ErrorMessage = "Please enter the publish date.")]
        public string PublishDateString { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }

        public string Area { get; set; }

        public bool IsReadableOnly { get; set; }

    }
    public class DocumentId
    {

        public int IntegerDocumentId { get; set; }
    }
}
