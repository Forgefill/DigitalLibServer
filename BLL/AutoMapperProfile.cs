using AutoMapper;
using BLL.Model;
using BLL.Model.Book;
using BLL.Model.Review;
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
            CreateMap<RegisterModel, UserModel>();
            
            CreateMap<CreateBookModel, Book>();
            CreateMap<UpdateBookModel, Book>();
            CreateMap<Book, BookModel>();
            CreateMap<Image, ImageModel>();

            CreateMap<Review, ReviewInfoModel>();
            CreateMap<ReviewModel, Review>();

            CreateMap<Genre, GenreModel>();
            CreateMap<GenreModel, Genre>();
        }
    }
}
