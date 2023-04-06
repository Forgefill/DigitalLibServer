using AutoMapper;
using BLL.Model;
using DAL.Model.Entities;

namespace BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, User>();
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();
            CreateMap<Book, BookModel>();
            CreateMap<BookModel, Book>();
        }
    }
}
