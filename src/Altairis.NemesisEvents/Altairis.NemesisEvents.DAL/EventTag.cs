using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.DAL {
    public class EventTag : IEntity<int> {

        [Key]
        public int Id { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }

    }
}