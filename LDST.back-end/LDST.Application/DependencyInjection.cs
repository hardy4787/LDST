using System.Reflection;
using FluentValidation;
using LDST.Application.Authentication.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LDST.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
