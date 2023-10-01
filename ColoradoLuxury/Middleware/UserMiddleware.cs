using ColoradoLuxury.Interface;
using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using ColoradoLuxury.Services;
using Microsoft.EntityFrameworkCore;
using UAParser;

namespace ColoradoLuxury.Middleware
{
    public static class UserMiddleware
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        ////private readonly RequestDelegate _next;
        //public UserMiddleware(RequestDelegate next)
        //{
        //    _next = next;
        //    //_httpContextAccessor = httpContextAccessor;

        //}

        //public static void GetCurrentUser(this WebApplication app, HttpContext context)
        //{
        //    ApplicationUsers user = new ApplicationUsers();

        //    //Client Browser info
        //    var uaParser = Parser.GetDefault();
        //    ClientInfo c = uaParser.Parse(context.Request.Headers["User-Agent"].ToString());
        //    user.Browser = c.UA.ToString();
        //    user.ClientIpAddress = context.Connection.LocalIpAddress.ToString();
        //    SetClientInformations.UsersLog(user);
        //}
      
        //public async Task InvokeAsync(HttpContext context, ColoradoContext dbContext)
        //{
        //    try
        //    {
        //        ApplicationUsers user = new ApplicationUsers();

        //        //Client Browser info
        //        var uaParser = Parser.GetDefault();
        //        ClientInfo c = uaParser.Parse(context.Request.Headers["User-Agent"].ToString());
        //        user.Browser = c.UA.ToString();
        //        user.ClientIpAddress = context.Connection.LocalIpAddress.ToString();
        //        SetClientInformations.UsersLog(dbContext, user);
        //    }
        //    catch (Exception)
        //    {

        //        throw new Exception();
        //    }

        //}

        
    }
}
