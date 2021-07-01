using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PickfordsIntranet.ViewModels.SuperAdmin
{
    public class MonthStarVM
    {
        public int Id { get; set; }
        public string MonthName { get; set; }
        public byte MonthNumber { get; set; }
        public int Year { get; set; }

        [MaxLength(200, ErrorMessage = "The month name for this set of stars should not be greater than 200 characters.")]
        [Required(ErrorMessage = "Please enter the month name for this set of stars.")]
        [Remote(action: "ValidatNewMonthName", controller: "SuperAdmin", AdditionalFields = "Id")]
        public string NewMonthName { get; set; }

        //[Required(ErrorMessage = "Please enter the month or date for this set of stars.")]
        //[Remote(action: "ValidateMonthYear", controller: "SuperAdmin", AdditionalFields = "Id")]
        //public string MonthYear { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public List<StarValueType> StarValueTypes { get; set; }

        public MonthStarDetails Star1 { get; set; }
        public MonthStarDetails Star2 { get; set; }
        public MonthStarDetails Star3 { get; set; }
        public MonthStarDetails Star4 { get; set; }

        public MonthStarDetails Star5 { get; set; }
        public MonthStarDetails Star6 { get; set; }
        public MonthStarDetails Star7 { get; set; }
        public MonthStarDetails Star8 { get; set; }

        public MonthStarDetails Star9 { get; set; }
        public MonthStarDetails Star10 { get; set; }
        public MonthStarDetails Star11 { get; set; }
        public MonthStarDetails Star12 { get; set; }


    }

    public class StarValueType
    {
        public string Value { get; set; }
        public string ImgSource { get; set; }
        public string Description { get; set; }
    }

}
