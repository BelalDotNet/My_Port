using System.ComponentModel.DataAnnotations;

namespace My_Port.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string UserRole { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
