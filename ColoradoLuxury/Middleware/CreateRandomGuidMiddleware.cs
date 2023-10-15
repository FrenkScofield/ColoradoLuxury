using ColoradoLuxury.Extensions;
using System.Globalization;

namespace ColoradoLuxury.Middleware
{
    public class CreateRandomGuidMiddleware
    {
        private readonly RequestDelegate _next;

        public CreateRandomGuidMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.GetSessionString("activeUniqueKey") == null)
            {
                Guid guid = Guid.NewGuid();
                context.SetSessionString("activeUniqueKey", guid.ToString());
            }

            await _next(context);
        }
    }
}
