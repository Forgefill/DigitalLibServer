using AutoMapper;
using BLL.Model;
using DAL.Entities;

namespace BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, User>();
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();
            CreateMap<Book, BookInfoModel>().ForMember(x=>x.AverageScore, opt => opt.MapFrom(src => src.Reviews.Average(c => c.Score)));
            CreateMap<RegisterModel, UserModel>();
            CreateMap<Book, BookModel>()
                .ForMember(x => x.AverageScore, opt => opt.MapFrom(src => src.Reviews.Average(c => c.Score)))
                .ForMember(x=>x.AuthorId, opt=>opt.MapFrom(src=>src.Author.Username));
            CreateMap<Image, ImageModel>();
        }
    }
}
