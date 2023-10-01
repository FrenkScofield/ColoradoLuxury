using ColoradoLuxury.Models.BLL;
using FluentValidation;

namespace ColoradoLuxury.FluentValidation
{
    public class ApiSettingsValidator:AbstractValidator<ApiSettingsDetail>
    {
        public ApiSettingsValidator()
        {
            RuleFor(x => x.Secretkey).NotNull();
            RuleFor(x => x.Publickey).NotNull();
        }
    }
}
