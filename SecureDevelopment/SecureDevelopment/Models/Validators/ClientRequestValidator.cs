using FluentValidation;
using SecureDevelopment.Models.Requests;

namespace SecureDevelopment.Models.Validators
{
    public class ClientRequestValidator : AbstractValidator<CreateClientRequest>
    {
        public ClientRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .Length(3, 255);

            RuleFor(x => x.Surname)
                .NotNull()
                .Length(3, 255);

            RuleFor(x => x.Patronymic)
                .NotNull()
                .Length(3, 255);
        }
    }
}
