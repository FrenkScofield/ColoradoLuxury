using System.ComponentModel;

namespace ColoradoLuxury.Models.BLL
{
    public class ApiSettingsDetail: AbstractEntity
    {
        [Description("Secretkey")]
        public string Secretkey { get; set; }

        [Description("Publickey")]
        public string Publickey { get; set; }

    }
}
