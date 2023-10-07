using ColoradoLuxury.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace ColoradoLuxury.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var getuser = context.HttpContext.GetSessionString("admin");

            if (getuser is null)
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
        }

    }
}
