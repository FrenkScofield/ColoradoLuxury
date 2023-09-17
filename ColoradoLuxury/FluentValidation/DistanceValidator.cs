using ColoradoLuxury.Models.VM;
using FluentValidation;

namespace ColoradoLuxury.FluentValidation
{
    public class DistanceValidator : AbstractValidator<GetMileAndTime>
    {
        public DistanceValidator()
        {
            RuleFor(x => x).Must(NotAllPropertiesZeroOrNegative).WithMessage("It was detected incompatibility for Mile, Hours, Minutes. Please ensure that your informations!");
        }

        private bool NotAllPropertiesZeroOrNegative(GetMileAndTime model)
        {
            return model.Mile > 0 && (model.Hours >= 0 && model.Minutes > 0) || (model.Hours > 0 && model.Minutes >= 0);
        }
    }
}
