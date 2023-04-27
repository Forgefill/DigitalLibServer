using AutoMapper;
using BLL.Model;
using BLL.Model.Book;
using BLL.Model.Chapter;
using BLL.Model.Comment;
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

            CreateMap<Comment, CommentModel>();
            CreateMap<CommentModel, Comment>();

            CreateMap<Genre, GenreModel>();
            CreateMap<GenreModel, Genre>();

            CreateMap<Chapter, ChapterModel>();
            CreateMap<ChapterModel, Chapter>();
        }
    }
}
