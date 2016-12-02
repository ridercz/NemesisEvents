using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Altairis.NemesisEvents.DAL {

    public class Event {
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

        public Venue Venue { get; set; }

        [Required]
        public bool UseRegistration { get; set; }

        [Required]
        public bool AllowRegistration { get; set; }

        [Required, MaxLength(100)]
        public string OrganizerName { get; set; }

        [Required]
        public int AdmissionFee { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public bool InvitationSent { get; set; }

        [Required, ForeignKey("Owner")]
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        public virtual ICollection<Attendee> Attendees { get; set; }

    }
}