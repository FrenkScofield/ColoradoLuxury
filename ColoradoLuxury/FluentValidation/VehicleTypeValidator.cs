using ColoradoLuxury.Models.BLL;
using FluentValidation;

namespace ColoradoLuxury.FluentValidation
{
    public class VehicleTypeValidator:AbstractValidator<VehicleType>
    {
        public VehicleTypeValidator()
        {
            this.RuleFor(x => x.TypeName).NotNull().NotEmpty();
        }
    }
}
