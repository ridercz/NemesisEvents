using System.ComponentModel.DataAnnotations;

namespace Altairis.NemesisEvents.BL.DTO
{
    public class ChangePasswordDTO
    {

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(12)]
        public string NewPassword { get; set; }

        [Required]
        public string NewPasswordConfirmation { get; set; }

    }
}