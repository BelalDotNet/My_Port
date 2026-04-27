using System.ComponentModel.DataAnnotations;

namespace My_Port.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        public required string UserId { get; set; }
        public required string RoleId { get; set; }
    }
}
