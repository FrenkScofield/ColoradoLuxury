namespace ColoradoLuxury.Middleware.Extension
{
    public static class CreateRandomGuidMiddlewareExtension
    {
        public static IApplicationBuilder CreateUniqueKey(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CreateRandomGuidMiddleware>();
        }
    }
}
