using System.ComponentModel.DataAnnotations;

namespace Login_using_auth.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //public string User { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
