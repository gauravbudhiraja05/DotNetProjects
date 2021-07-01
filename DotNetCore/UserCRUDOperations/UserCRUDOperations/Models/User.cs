using System.ComponentModel.DataAnnotations;

namespace UserCRUDOperations.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }

    }
}
