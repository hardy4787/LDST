using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LDST.Api.Filters;

public class AccessActionFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var currentUserId = context.HttpContext.Request.Headers.First(x=>x.Key == "username");
        var user = context.HttpContext.User;
        var email = user.Claims.Single(x=>x.Type == JwtRegisteredClaimNames.Name).Value;

        Console.WriteLine($"User name: {email}");
    }
}