using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Altairis.NemesisEvents.DAL {
    public class Attendee {

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
