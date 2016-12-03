using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.DAL {
    public class Attendee : IEntity<int> {

        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }

        public DateTime DateRegistered { get; set; }

        public DateTime? DateCancelled { get; set; }

    }
}
