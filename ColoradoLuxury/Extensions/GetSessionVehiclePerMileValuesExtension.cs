using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.VM;
using System;

namespace ColoradoLuxury.Extensions
{
    public static class GetSessionVehiclePerMileValuesExtension
    {
        public static List<GetVehicleDistanceAmounts> GetPermileOrValues(this List<VehicleType> vehicleTypes, HttpContext httpContext)
        {
            List<GetVehicleDistanceAmounts> result = new List<GetVehicleDistanceAmounts>();
            for (int i = 0; i < vehicleTypes.Count; i++)
            {
                result.Add(new GetVehicleDistanceAmounts { Key = vehicleTypes[i].TypeName.Replace(" ", "").ToLower(), DistanceAmount = httpContext.Session.GetString(vehicleTypes[i].TypeName.Replace(" ", "").ToLower()), IsActive = vehicleTypes[i].IsActive });
            }
            return result;
        }

       
    }
}
