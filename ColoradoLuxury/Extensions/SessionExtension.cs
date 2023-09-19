using Newtonsoft.Json;

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
    }
}
