
namespace ColoradoLuxury.Extensions
{
    public static class GeneralExtension<T> where T : struct
    {
        public static string ToString(T value)
        {
            return value.ToString();
        }
    }
}
