using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;
using Altairis.NemesisEvents.DAL;
using Riganti.Utils.Infrastructure.AutoMapper;
using AutoMapper;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Mapping {
    public class EventMapping : IMapping {

        private IUnitOfWorkProvider uowProvider;

        public EventMapping(IUnitOfWorkProvider uowProvider) {
            this.uowProvider = uowProvider;
        }

        public void Map(IMapperConfigurationExpression cfg) {
            // Public
            cfg.CreateMap<Event, PublicUpcomingEventDTO>()
                .ForMember(e => e.HasAdmissionFee, m => m.MapFrom(e => !string.IsNullOrEmpty(e.AdmissionFee)))
                .ForMember(e => e.Tags, m => m.MapFrom(e => e.EventTags.Select(x => x.Tag.Name)));
            cfg.CreateMap<Event, PublicArchiveEventDTO>()
                .ForMember(e => e.Tags, m => m.MapFrom(e => e.EventTags.Select(x => x.Tag.Name)))
                .ForMember(e => e.HasVideo, m => m.MapFrom(e => !string.IsNullOrEmpty(e.VideoUrl)))
                .ForMember(e => e.HasSlides, m => m.MapFrom(e => e.Attachments.Any(a => a.Type == AttachmentType.Slides)))
                .ForMember(e => e.HasDemo, m => m.MapFrom(e => e.Attachments.Any(a => a.Type == AttachmentType.Demo)))
                .ForMember(e => e.HasPhotos, m => m.MapFrom(e => e.Attachments.Any(a => a.Type == AttachmentType.Photo)))
                .ForMember(e => e.HasOtherAttachments, m => m.MapFrom(e => e.Attachments.Any(a => a.Type == AttachmentType.Other)));

            // Admin
            cfg.CreateMap<Event, EventDTO>()
                .ForMember(e => e.HasAdmissionFee, m => m.MapFrom(e => !string.IsNullOrEmpty(e.AdmissionFee)));
            cfg.CreateMap<Event, OrganizedEventDTO>()
                .ForMember(e => e.HasVideo, m => m.MapFrom(e => !string.IsNullOrEmpty(e.VideoUrl)))
                .ForMember(e => e.HasSlides, m => m.MapFrom(e => e.Attachments.Any(a => a.Type == AttachmentType.Slides)))
                .ForMember(e => e.HasDemo, m => m.MapFrom(e => e.Attachments.Any(a => a.Type == AttachmentType.Demo)))
                .ForMember(e => e.HasPhotos, m => m.MapFrom(e => e.Attachments.Any(a => a.Type == AttachmentType.Photo)))
                .ForMember(e => e.HasOtherAttachments, m => m.MapFrom(e => e.Attachments.Any(a => a.Type == AttachmentType.Other)));
            cfg.CreateMap<Event, EventDetailDTO>()
                .ForMember(e => e.TagIds, m => m.MapFrom(e => e.EventTags.Select(t => t.TagId)));
            cfg.CreateMap<EventDetailDTO, Event>()
                .ForMember(e => e.EventTags, m => m.DropAndCreateCollection(uowProvider, e => e.TagIds, e => new EventTag { TagId = e }))
                .ForMember(e => e.InvitationSent, m => m.Ignore())                              // Do not automap invitation state
                .AfterMap((s, d) => d.InvitationSent = s.Id == 0 ? false : d.InvitationSent);   // Persist invitation state for existing entries
        }

    }
}
