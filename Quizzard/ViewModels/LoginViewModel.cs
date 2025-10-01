using System.ComponentModel.DataAnnotations;

namespace Quizzard.ViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings= false, ErrorMessage = "Please provide a valid user name")]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a valid password")]

        public string Password { get; set; }

    }
}
