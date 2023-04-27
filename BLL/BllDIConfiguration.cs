﻿using Microsoft.Extensions.Configuration;
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
            services.AddScoped<ValidatorRepo>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGenreService, GenreServices>();
            services.AddScoped<IAuthService, AuthService>();
            

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;  //Delete before production
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidateIssuer = true,
                    //ValidateAudience = true,
                    //ValidateLifetime = true,
                    //ValidIssuer = AuthOptions.ValidIssuer,
                    //ValidAudience = AuthOptions.ValidAudience,
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
