using FluentValidation;
using Infrastructure.Utilities.Errors.ErrorMessages;
using Infrastructure.Utilities.RegEx;

namespace EvoEvents.API.Requests.Users
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage(UserErrorMessage.WrongCredentials)
                .Length(7, 74).WithMessage(UserErrorMessage.WrongCredentials)
                .Matches(RegularExpression.EmailFormat).WithMessage(UserErrorMessage.WrongCredentials);
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage(UserErrorMessage.WrongCredentials)
                .Length(2, 20).WithMessage(UserErrorMessage.WrongCredentials)
                .Matches(RegularExpression.NoWhiteSpaces).WithMessage(UserErrorMessage.WrongCredentials);
        }
    }
}