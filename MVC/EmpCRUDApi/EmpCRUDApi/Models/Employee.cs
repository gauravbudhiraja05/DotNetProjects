namespace EmpCRUDApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public string DOB { get; set; }
        public string HighestQualification { get; set; }
    }
}