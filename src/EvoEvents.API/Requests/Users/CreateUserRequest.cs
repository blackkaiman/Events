using FluentValidation;
using Infrastructure.Utilities.Errors.ErrorMessages;
using Infrastructure.Utilities.RegEx;

namespace EvoEvents.API.Requests.Users
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
    }

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .Length(7, 74).WithMessage(UserErrorMessage.EmailFormat)
                .Matches(RegularExpression.EmailFormat).WithMessage(UserErrorMessage.EmailFormat);
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .Length(2, 100).WithMessage(UserErrorMessage.FirstNameFormat)
                .Matches(RegularExpression.AlphaWhiteSpacesDash).WithMessage(UserErrorMessage.FirstNameFormat);
            RuleFor(u => u.LastName)
                .NotEmpty()
                .Length(2, 100).WithMessage(UserErrorMessage.LastNameFormat)
                .Matches(RegularExpression.AlphaWhiteSpacesDash).WithMessage(UserErrorMessage.LastNameFormat);
            RuleFor(u => u.Company)
                .NotEmpty()
                .Length(2, 100).WithMessage(UserErrorMessage.CompanyFormat)
                .Matches(RegularExpression.Alphanumeric).WithMessage(UserErrorMessage.CompanyFormat);
            RuleFor(u => u.Password)
                .NotEmpty()
                .Length(2, 20).WithMessage(UserErrorMessage.PasswordFormat)
                .Matches(RegularExpression.NoWhiteSpaces).WithMessage(UserErrorMessage.PasswordFormat);
        }
    }
}