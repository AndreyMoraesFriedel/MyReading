using AutoMapper;
using MyReading.API.Domain.DTOs;
using MyReading.API.Domain.Model;

namespace MyReading.API.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Book, BookDTO>();
        }
    }
}
