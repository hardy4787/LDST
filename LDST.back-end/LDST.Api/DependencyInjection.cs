using LDST.Api.Common.Errors;
using LDST.Api.Common.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;

namespace LDST.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            //services.AddSwaggerGen(s =>
            //{
            //    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        Description = "JWT Authorization header using the bearer scheme",
            //        Name = "Authorization",
            //        In = ParameterLocation.Header,
            //        Type = SecuritySchemeType.ApiKey,
            //        Scheme = JwtBearerDefaults.AuthenticationScheme,
            //    });
            //    s.AddSecurityRequirement(new OpenApiSecurityRequirement()
            //    {
            //        {
            //            new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference
            //                {
            //                    Type = ReferenceType.SecurityScheme,
            //                    Id = JwtBearerDefaults.AuthenticationScheme
            //                },
            //                Name = JwtBearerDefaults.AuthenticationScheme,
            //                In = ParameterLocation.Header,

            //            },
            //            new List<string>()
            //        }
            //    });
            //});
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSingleton<ProblemDetailsFactory, AppProblemDetailsFactory>();
            services.AddMappings();
            return services;
        }
    }
}
