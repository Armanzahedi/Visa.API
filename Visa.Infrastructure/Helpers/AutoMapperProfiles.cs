using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visa.Core.Models;
using Visa.Infrastructure.Dtos;
using Visa.Infrastructure.Dtos.Article;

namespace Visa.Infrastructure.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ForMember(u=>u.Avatar,opt=>opt.MapFrom(m=>m.Avatar != null ? $"/Images/UserAvatar/{m.Avatar}" : null));
            CreateMap<UserDto, User>();
            CreateMap<Article, ArticleListDto>()
                .ForMember(u => u.Image, opt => opt.MapFrom(m => m.Image != null ? $"/Images/Article/{m.Image}" : null))
                .ForPath(u => u.Author.Name, opt => opt.MapFrom(m =>$"{m.User.FirstName} {m.User.LastName}"))
                .ForPath(u => u.Author.Avatar, opt => opt.MapFrom(m => $"/Images/UserAvatar/{m.User.Avatar}"));
            CreateMap<ArticleTagDto, ArticleTag>().ReverseMap();
            CreateMap<ArticleHeadLineDto, ArticleHeadLine>().ReverseMap();
            CreateMap<ArticleCommentsDto, ArticleComment>().ReverseMap();
            CreateMap<Article, ArticleDetailDto>().ForMember(u => u.Image, opt => opt.MapFrom(m => m.Image != null ? $"/Images/Article/{m.Image}" : null))
                .ForPath(u => u.Author.Name, opt => opt.MapFrom(m => $"{m.User.FirstName} {m.User.LastName}"))
                .ForPath(u => u.Author.Avatar, opt => opt.MapFrom(m => $"/Images/UserAvatar/{m.User.Avatar}"));
        }
    }
}
