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
    public class AuthorMapper
    {
        public static MapperConfiguration Mapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AuthorId))
                    .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.BirthDate))
                    .ForMember(dest => dest.CiudadProcedencia, opt => opt.MapFrom(src => src.City))
                    .ForMember(dest => dest.CorreoElectronico, opt => opt.MapFrom(src => src.Email))
                    .ReverseMap();
            });
        }
    }
}
