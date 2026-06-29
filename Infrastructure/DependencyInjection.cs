using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Infrastructure.JwtService;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            MongoConfiguration.Configure();

            services.Configure<MongoOptions>(configuration.GetSection(MongoOptions.SectionName));

            services.AddScoped<MongoDbContext>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddScoped<IUserRepository, MongoUserRepository>();
            services.AddScoped<IProjectRepository, MongoProjectRepository>();
            services.AddScoped<ITaskRepository, MongoTaskRepository>();


            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.AddSingleton<ITokenService, TokenService.JwtService>();

            return services;
        }
    }
}
