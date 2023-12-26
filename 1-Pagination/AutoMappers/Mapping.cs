using _1_Pagination.Models;
using _1_Pagination.Models.DTOs;
using AutoMapper;

namespace _1_Pagination.AutoMappers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
        }
    }
}
