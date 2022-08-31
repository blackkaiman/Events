using FluentValidation;
using Infrastructure.Utilities.Errors.ErrorMessages;
using Infrastructure.Utilities.RegEx;

namespace EvoEvents.API.Requests.Reservations
{
    public class EmailModel
    {
        public string UserEmail { get; set; }
    }

    public class EmailModelValidator : AbstractValidator<EmailModel>
    {
        public EmailModelValidator()
        {
            RuleFor(e => e.UserEmail).NotEmpty()
                .Matches(RegularExpression.EmailFormat).WithMessage(UserErrorMessage.EmailFormat);
        }
    }
}