using System.ComponentModel.DataAnnotations;

namespace My_Port.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [StringLength(50)]
        public required string EmployeeName { get; set; }
        [StringLength(50)]
        public string? Department { get; set; }
        [StringLength(30)]
        public string? Email { get; set; }
        [StringLength(30)]
        public required string Designation { get; set; }
    }
}
