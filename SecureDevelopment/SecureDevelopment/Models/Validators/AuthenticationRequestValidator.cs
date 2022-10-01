using FluentValidation;
using SecureDevelopment.Models.Requests;

namespace SecureDevelopment.Models.Validators
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>   
    {
        public AuthenticationRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotNull()
                .Length(5, 255)
                .EmailAddress();


            RuleFor(x => x.Password)
                .NotNull()
                .Length(5, 50);

        }
    }
}
