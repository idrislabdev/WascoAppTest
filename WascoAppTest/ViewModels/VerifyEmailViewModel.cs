using System.ComponentModel.DataAnnotations;

namespace WascoAppTest.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
