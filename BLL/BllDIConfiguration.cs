using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DAL;
using BLL.JWTAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using BLL.Validators;

namespace BLL
{
    public class BllDIConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            DalDIConfiguration.ConfigureServices(services, configuration);

            services.AddScoped<RegisterModelValidator>();
            services.AddScoped<GenreModelValidator>();
            services.AddScoped<UpdateBookModelValidator>();
            services.AddScoped<CreateBookModelValidator>();
            services.AddScoped<ReviewModelValidator>();
            services.AddScoped<CommentModelValidator>();
            services.AddScoped<ChapterModelValidator>();
            services.AddScoped<ValidatorRepo>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IChapterService, ChapterService>();
            services.AddScoped<IImageService, ImageService>();
            

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;  //Delete before production
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddScoped<IMapper, Mapper>(implementationFactory =>
            {
                var profile = new AutoMapperProfile();
                var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
                return new Mapper(config);
            });
        }
    }
}
