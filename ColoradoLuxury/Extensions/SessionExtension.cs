using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ColoradoLuxury.Extensions
{
    public static class SessionExtension
    {
        public static void SetObjectsession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        //get session
        public static T? GetObjectsession<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }


        public static void SetSessionString(this ISession session, string key, string value) => session.SetString(key, value);

        //get session
        public static string GetSessionString(this ISession session, string key) => session.GetString(key);


    }
}
