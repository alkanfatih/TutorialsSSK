using AutoMapper;
using BlogProject.App.DTOs;
using BlogProject.Core.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.App.Mappers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AppUser, AppUserDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Article, ArticleDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
        }
    }
}
