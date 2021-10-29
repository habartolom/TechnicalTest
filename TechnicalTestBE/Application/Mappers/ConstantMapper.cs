using AutoMapper;

using Domain.Dtos;

using Persistence.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class ConstantMapper
    {
        public static MapperConfiguration Mapper()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<Constant, ConstantDto>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Value))
                .ReverseMap();
            });
        }
    }
}
