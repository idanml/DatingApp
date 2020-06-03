using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
   public class UserToRegisterDto
   {
      [Required]
      public string UserName { get; set; }
      [Required]
      [StringLength(8, MinimumLength = 4, ErrorMessage = "Password Between 4 to 8 char")]
      public string password { get; set; }
   }
}