using System.ComponentModel.DataAnnotations;

namespace authentication_authorization.Models
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
