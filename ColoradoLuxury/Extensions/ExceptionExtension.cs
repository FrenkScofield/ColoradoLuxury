using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ColoradoLuxury.Extensions
{
    public static class ExceptionExtension
    {
        public static void Log(Exception ex, ColoradoContext context)
        {
            string? message = null;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            message  = ex.Message;

            ExceptionLog exceptionLog = new ExceptionLog()
            {
                Message = message
            };
            context.ExceptionLogs.Add(exceptionLog);
            context.SaveChanges();
            
        }

        //public static string GetIpAddress(HttpContext httpContext)
        //{
        //    string ip = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    if (string.IsNullOrEmpty(ip))
        //    {
        //        ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    return ip;
        //}
    }
}
