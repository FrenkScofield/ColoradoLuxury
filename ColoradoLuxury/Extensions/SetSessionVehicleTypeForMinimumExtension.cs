using ColoradoLuxury.Models.BLL;
using System;

namespace ColoradoLuxury.Extensions
{
    public static class SetSessionVehicleTypeForMinimumExtension
    {
        public static void SetPermileValues(this List<VehicleType> vehicleTypes, HttpContext httpContext)
        {
            for (int i = 0; i < vehicleTypes.Count; i++)
            {
                httpContext.Session.SetString(vehicleTypes[i].TypeName.Replace(" ", "").ToLower(), vehicleTypes[i].PerMile.ToString());
            }
            
        }

        public static void SetByHourValues(this List<VehicleType> vehicleTypes, HttpContext httpContext)
        {
            for (int i = 0; i < vehicleTypes.Count; i++)
            {
                httpContext.Session.SetString(vehicleTypes[i].TypeName.Replace(" ", "").ToLower(), vehicleTypes[i].Hourly.ToString());
            }

        }
    }
}
