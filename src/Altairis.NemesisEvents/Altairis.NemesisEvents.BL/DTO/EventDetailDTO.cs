using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.DTO {
    public class EventDetailDTO : IEntity<int> {

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateBegin { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        [Required]
        public int VenueId { get; set; }

        [Required]
        public bool UseRegistration { get; set; }

        [Required]
        public bool AllowRegistration { get; set; }

        [MaxLength(100), Url]
        public string RegistrationUrl { get; set; }

        [MaxLength(100), Url]
        public string VideoUrl { get; set; }

        [MaxLength(100), Url]
        public string InfoUrl { get; set; }

        [Required, MaxLength(100)]
        public string OrganizerName { get; set; }

        public string AdmissionFee { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public IList<int> TagIds { get; } = new List<int>();

        public IList<AttachmentDTO> Attachments { get; } = new List<AttachmentDTO>();

    }

}
