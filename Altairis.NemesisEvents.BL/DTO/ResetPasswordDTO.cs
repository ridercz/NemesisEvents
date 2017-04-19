using System.ComponentModel.DataAnnotations;

namespace Altairis.NemesisEvents.BL.DTO
{
    public class ResetPasswordDTO
    {
        [Required]
        [MinLength(12)]
        [Compare(nameof(PasswordConfirmation))]
        public string Password { get; set; }

        [Required]
        public string PasswordConfirmation { get; set; }

    }
}