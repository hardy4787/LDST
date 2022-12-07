using LDST.Api.Common.Errors;
using LDST.Api.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LDST.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, AppProblemDetailsFactory>();
            services.AddMappings();
            return services;
        }
    }
}
