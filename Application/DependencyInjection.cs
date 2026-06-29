using Application.Common;
using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddScoped<IDomainEventDispatcher, DomainEventDispathcer>();
            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssemblies(assembly);
            });

            services.AddAutoMapper(conf => conf.AddMaps(assembly));

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
