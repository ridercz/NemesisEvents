using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;
using Altairis.NemesisEvents.DAL;
using AutoMapper;

namespace Altairis.NemesisEvents.BL.Mapping {
    public class EventMapping : IMapping {

        public void Map(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<Event, PublicUpcomingEventDTO>()
                .ForMember(e => e.IsFree, m => m.MapFrom(e => !string.IsNullOrEmpty(e.AdmissionFee)))
                .ForMember(e => e.Tags, m => m.MapFrom(e => e.EventTags.Select(x => x.Tag.Name)));
            cfg.CreateMap<Event, PublicArchiveEventDTO>()
                .ForMember(e => e.Tags, m => m.MapFrom(e => e.EventTags.Select(x => x.Tag.Name)))
                .ForMember(e => e.HasVideo, m => m.MapFrom(e => !string.IsNullOrEmpty(e.VideoUrl)))
                .ForMember(e => e.HasSlides, m => m.MapFrom(e => e.Attachments.Any(a => a.Type == AttachmentType.Slides)))
                .ForMember(e => e.HasDemo, m => m.MapFrom(e => e.Attachments.Any(a => a.Type == AttachmentType.Demo)))
                .ForMember(e => e.HasPhotos, m => m.MapFrom(e => e.Attachments.Any(a => a.Type == AttachmentType.Photo)))
                .ForMember(e => e.HasOtherAttachments, m => m.MapFrom(e => e.Attachments.Any(a => a.Type == AttachmentType.Other)));
        }

    }
}
