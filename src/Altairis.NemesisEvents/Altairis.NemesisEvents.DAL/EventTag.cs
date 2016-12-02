using System.ComponentModel.DataAnnotations;

namespace Altairis.NemesisEvents.DAL {
    public class EventTag {

        [Key]
        public int Id { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }

    }
}