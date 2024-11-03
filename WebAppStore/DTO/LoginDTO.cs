using System.ComponentModel.DataAnnotations;

namespace WebAppStore.DTO
{
    public class LoginDTO
    {
        [Required]
        public string Username{ get; set; }
        [Required]
        public string Password{ get; set; }
    }
}
