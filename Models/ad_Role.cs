using System.ComponentModel.DataAnnotations;

namespace My_Port.Models
{
    public class ad_Role
    {
        [Key]
        public int RoleId { get; set; }
        public required string RoleName { get; set; }
        public string? RoleDescription { get; set; }
    }
}
