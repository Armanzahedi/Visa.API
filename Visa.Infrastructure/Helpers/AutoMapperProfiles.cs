using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visa.Core.Models;
using Visa.Infrastructure.Dtos;
using Visa.Infrastructure.Dtos.Article;
using Visa.Infrastructure.Dtos.Gallery;
using Visa.Infrastructure.Dtos.OurTeam;
using Visa.Infrastructure.Dtos.Partner;
using Visa.Infrastructure.Dtos.Service;
using Visa.Infrastructure.Dtos.StaticContent;

namespace Visa.Infrastructure.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ForMember(u => u.Avatar, opt => opt.MapFrom(m => m.Avatar != null ? $"/Files/UserAvatar/{m.Avatar}" : null));
            CreateMap<UserDto, User>();
            CreateMap<Article, ArticleListDto>()
                .ForMember(u => u.Image, opt => opt.MapFrom(m => m.Image != null ? $"/Files/Article/{m.Image}" : null))
                .ForPath(u => u.Author.Name, opt => opt.MapFrom(m => $"{m.User.FirstName} {m.User.LastName}"))
                .ForPath(u => u.Author.Avatar, opt => opt.MapFrom(m => $"/Files/UserAvatar/{m.User.Avatar}"));
            CreateMap<ArticleTagDto, ArticleTag>().ReverseMap();
            CreateMap<ArticleHeadLineDto, ArticleHeadLine>().ReverseMap();
            CreateMap<ArticleCommentsDto, ArticleComment>().ReverseMap();
            CreateMap<Article, ArticleDetailDto>().ForMember(u => u.Image, opt => opt.MapFrom(m => m.Image != null ? $"/Files/Article/{m.Image}" : null))
                .ForPath(u => u.Author.Name, opt => opt.MapFrom(m => $"{m.User.FirstName} {m.User.LastName}"))
                .ForPath(u => u.Author.Avatar, opt => opt.MapFrom(m => $"/Files/UserAvatar/{m.User.Avatar}"));
            CreateMap<StaticContentDetail, ContentDetailDto>().ForMember(u => u.Image, opt => opt.MapFrom(m => m.Image != null ? $"/Files/Content/{m.Image}" : null));
            CreateMap<OurTeam, OurTeamListDto>().ForMember(u => u.Image, opt => opt.MapFrom(m => m.Image != null ? $"/Files/OurTeam/{m.Image}" : null));
            CreateMap<Partner, PartnerListDto>().ForMember(u => u.Image, opt => opt.MapFrom(m => m.Image != null ? $"/Files/Partner/{m.Image}" : null));
            CreateMap<Gallery, GalleryDto>().ForMember(u => u.Image, opt => opt.MapFrom(m => m.Image != null ? $"/Files/Gallery/{m.Image}" : null));
            CreateMap<Service, ServicesListDto>().ForMember(u => u.Thumbnail, opt => opt.MapFrom(m => m.Thumbnail != null ? $"/Files/Service/Thumbnail/{m.Thumbnail}" : null));
            CreateMap<Service, ServicesDetailDto>()
                .ForMember(u => u.Thumbnail, opt => opt.MapFrom(m => m.Thumbnail != null ? $"/Files/Service/Thumbnail/{m.Thumbnail}" : null))
                .ForMember(u => u.Image, opt => opt.MapFrom(m => m.Thumbnail != null ? $"/Files/Service/Image/{m.Image}" : null))
                .ForMember(u => u.File, opt => opt.MapFrom(m => m.File != null ? $"/Files/Service/File/{m.File}" : null));
            CreateMap<ServiceInclude, ServiceIncludesDto>();
        }
    }
}
