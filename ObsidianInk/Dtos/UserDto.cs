using System.ComponentModel.DataAnnotations;

namespace ObsidianInk.Dtos
{
    public class UserDto
    {
        [Required, StringLength(100, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Phone, StringLength(15)]
        public string Phone { get; set; } = string.Empty;
        public object UserId { get; set; }
    }
}
