using System.ComponentModel.DataAnnotations;

namespace LoginDB.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
