using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UAParser;

namespace ColoradoLuxury.Services
{
    public static class SetClientInformations
    {
        private static ColoradoContext _httpContext;

        public static void Configure(ColoradoContext httpContext)
        {
            _httpContext = httpContext;
        }


        public static void UsersLog(ApplicationUsers user)
        {
            try
            {

                _httpContext.ApplicationUsers.Add(user);
                _httpContext.SaveChanges();
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
    }
}
