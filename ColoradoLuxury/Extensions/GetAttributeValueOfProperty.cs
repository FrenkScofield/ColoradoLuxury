using ColoradoLuxury.Models.BLL;
using System.ComponentModel;
using System.Reflection;

namespace ColoradoLuxury.Extensions
{
    public static class GetAttributeValueOfProperty
    {
        public static Dictionary<string, string> GetAttributeValue()
        {
            var myObject = new ApiSettingsDetail(); // Replace YourClass with your class name
            Dictionary<string, string> apiKeySettingsPropertyKeyValues = new Dictionary<string, string>();
            var properties = myObject.GetType().GetProperties();
            var propertyDescriptions = new Dictionary<string, string>();

            foreach (var property in properties)
            {
                var descriptionAttribute = property.GetCustomAttribute<DescriptionAttribute>();
                if (descriptionAttribute != null)
                {
                    apiKeySettingsPropertyKeyValues.Add(property.Name, descriptionAttribute.Description);
                }
            }

            return apiKeySettingsPropertyKeyValues;

        }
    }
}
