using System.ComponentModel.DataAnnotations;

namespace My_Port.Models
{
    public class ad_Role
    {
        [Key]
        public int RoleId { get; set; }
        [StringLength(50)]
        public required string RoleName { get; set; }
        [StringLength(200)]
        public string? RoleDescription { get; set; }
    }
}
