using System.ComponentModel.DataAnnotations;

namespace DatingApp.DTOs
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassWord { get; set; }
    }
}
