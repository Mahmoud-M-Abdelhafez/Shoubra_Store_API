using System.ComponentModel.DataAnnotations;

namespace WebAppStore.ViewModels

{
    public class EditUserVM
    {
         public string Id { get; set; }
        [Required]
        public string? Name { get; set; }
       
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? Address { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }
}
