using LDST.Api;
using LDST.Application;
using LDST.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseAuthentication();
    // app.UseAuthorization();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}