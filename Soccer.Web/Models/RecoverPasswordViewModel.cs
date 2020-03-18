using System.ComponentModel.DataAnnotations;

namespace Soccer.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
