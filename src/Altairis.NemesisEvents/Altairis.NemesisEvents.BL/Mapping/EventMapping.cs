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
            cfg.CreateMap<Event, UpcomingEventDTO>()
                .ForMember(e => e.IsFree, m => m.MapFrom(e => !string.IsNullOrEmpty(e.AdmissionFee)))
                .ForMember(e => e.Tags, m => m.MapFrom(e => e.EventTags.Select(x => x.Tag.Name)));
        }

    }
}
