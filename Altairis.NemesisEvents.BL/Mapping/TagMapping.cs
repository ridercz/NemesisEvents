﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.DAL;
using AutoMapper;

namespace Altairis.NemesisEvents.BL.Mapping
{
    public class TagMapping : IMapping {
        public void Map(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<Tag, TagDTO>();
        }
    }
}
