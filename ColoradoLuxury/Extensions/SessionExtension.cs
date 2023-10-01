using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ColoradoLuxury.Extensions
{
    public static class SessionExtension
    {
        public static void SetObjectsession(this HttpContext context, string key, object value)
        {
            context.Session.SetString(key, JsonConvert.SerializeObject(value));
        }

        //get session
        public static T? GetObjectsession<T>(this HttpContext context, string key)
        {
            var value = context.Session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        //set session string
        public static void SetSessionString(this HttpContext context, string key, string value) => context.Session.SetString(key, value);

        //get session string
        public static string GetSessionString(this HttpContext context, string key) => context.Session.GetString(key);

        //set session int
        public static void SetSessionInt32(this HttpContext context, string key, int value) => context.Session.SetInt32(key, value);

        //get session int
        public static int? GetSessionInt32(this HttpContext context, string key) => context.Session.GetInt32(key);


    }
}
