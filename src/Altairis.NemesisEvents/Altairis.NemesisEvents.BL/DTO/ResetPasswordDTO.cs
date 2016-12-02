using System.ComponentModel.DataAnnotations;

namespace Altairis.NemesisEvents.BL.DTO
{
    public class ResetPasswordDTO
    {

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordConfirmation { get; set; }

    }
}