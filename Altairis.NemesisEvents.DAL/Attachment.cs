using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.DAL {
    public class Attachment : IEntity<int> {

        [Key]
        public int Id { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(200)]
        public string StorageUrl { get; set; }

        public AttachmentType Type { get; set; }

    }

    public enum AttachmentType {
        Other = 0,
        Slides = 1,
        Demo = 2,
        Photo = 3
    }

}
