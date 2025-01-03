using System.ComponentModel.DataAnnotations;

namespace Login_using_auth.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        //[StringLength(50, MinimumLength = 3)]
        public string? Name { get; set; }

        public string? Username { get; set; }


        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be 8-15 characters long and contain at least one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }
    }
}
