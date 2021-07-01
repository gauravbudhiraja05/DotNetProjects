using System;

namespace SampleCoreEFProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Manufacturer { get; set; }
        public string ShippingNo { get; set; }
        public int SerialNo { get; set; }
        public string BatchNo { get; set; }
        public decimal MRP { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
