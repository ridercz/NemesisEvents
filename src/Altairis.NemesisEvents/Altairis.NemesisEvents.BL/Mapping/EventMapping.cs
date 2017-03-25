using System.Linq;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.DAL;
using Riganti.Utils.Infrastructure.AutoMapper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;

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
                .ForMember(e => e.HasAdmissionFee, m => m.MapFrom(e => !string.IsNullOrEmpty(e.AdmissionFee)))
                .ForMember(e => e.HasOtherAttachments, m => m.MapFrom(e => e.Attachments.Any(a => a.Type == AttachmentType.Other)));
            cfg.CreateMap<Event, EventDetailDTO>()
                .BeforeMap((s, d) => {  // EF Core does not support lazy loading yet
                    EntityFrameworkUnitOfWork.TryGetDbContext(uowProvider).Entry(s).Collection(x => x.Attachments).Load();
                    EntityFrameworkUnitOfWork.TryGetDbContext(uowProvider).Entry(s).Collection(x => x.EventTags).Load();
                })
                .ForMember(e => e.TagIds, m => m.MapFrom(e => e.EventTags.Select(t => t.TagId)));
            cfg.CreateMap<EventDetailDTO, Event>()
                .BeforeMap((s, d) => {  // EF Core does not support lazy loading yet
                    EntityFrameworkUnitOfWork.TryGetDbContext(uowProvider).Entry(d).Collection(x => x.Attachments).Load();
                    EntityFrameworkUnitOfWork.TryGetDbContext(uowProvider).Entry(d).Collection(x => x.EventTags).Load();
                })
                .ForMember(e => e.EventTags, m => m.DropAndCreateCollection(uowProvider, e => e.TagIds, e => new EventTag { TagId = e }))
                .ForMember(e => e.Venue, m => m.Ignore()) 
                .ForMember(e => e.Owner, m => m.Ignore())    
                .ForMember(e => e.Attachments, m => m.Ignore())
                .ForMember(e => e.InvitationSent, m => m.Ignore())                              // Do not automap invitation state
                .AfterMap((s, d) =>
                {
                    // Persist invitation state for existing entries
                    d.InvitationSent = s.Id == 0 ? false : d.InvitationSent;

                    // save attachments (we cannot use SyncCollectionByKey because of EF Core bugs)
                    foreach (var existing in d.Attachments)
                    {
                        var sourceItem = s.Attachments.FirstOrDefault(e => e.Id == existing.Id);
                        if (sourceItem == null)
                        {
                            EntityFrameworkUnitOfWork.TryGetDbContext(uowProvider)
                                .Set<Attachment>().Remove(existing);
                        }
                        else
                        {
                            Mapper.Map(sourceItem, existing);
                            EntityFrameworkUnitOfWork.TryGetDbContext(uowProvider)
                                .Entry(existing).State = EntityState.Modified;
                        }
                    }
                    foreach (var addedItem in s.Attachments.Where(a => a.Id == 0))
                    {
                        EntityFrameworkUnitOfWork.TryGetDbContext(uowProvider)
                            .Set<Attachment>().Add(Mapper.Map<Attachment>(addedItem));
                    }
                });   
        }

    }
}
