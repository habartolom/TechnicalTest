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
    public class BookMapper
    {
        public static MapperConfiguration Mapper()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<Book, BookDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BookId))
                    .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Title))
                    .ForMember(dest => dest.Anio, opt => opt.MapFrom(src => src.Year))
                    .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genre))
                    .ForMember(dest => dest.NoPaginas, opt => opt.MapFrom(src => src.Pages))
                    .ReverseMap();
            });
        }
    }
}
