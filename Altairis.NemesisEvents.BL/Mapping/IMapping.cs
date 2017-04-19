using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Altairis.NemesisEvents.BL.Mapping
{
    public interface IMapping
    {

        void Map(IMapperConfigurationExpression cfg);

    }
}
