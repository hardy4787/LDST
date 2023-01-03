using LDST.Api;
using LDST.Api.Middlewares;
using LDST.Application;
using LDST.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseMiddleware<ValidationExceptionMiddleware>();
    app.UseAuthentication();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    app.UseHttpsRedirection();
    app.UseCors("AllowAllOrigins");
    app.MapControllers();
    app.Run();
}