using FluentValidation;
using Infrastructure.Utilities.Errors.ErrorMessages;
using Infrastructure.Utilities.RegEx;

namespace EvoEvents.API.Requests.Events.Reservations
{
    public class RegistrationInformation
    {
        public string UserEmail { get; set; }
        public string AccompanyingPerson { get; set; }
    }

    public class RegistrationInformationValidator : AbstractValidator<RegistrationInformation>
    {
        public RegistrationInformationValidator()
        {  
            RuleFor(e => e.AccompanyingPerson)
                .NotEqual(e => e.UserEmail).WithMessage(ReservationErrorMessage.DuplicateEmail)
                .Matches(RegularExpression.EmailFormat).WithMessage(UserErrorMessage.EmailFormat);
            RuleFor(e => e.UserEmail)
                .NotEmpty()
                .MinimumLength(7).WithMessage(UserErrorMessage.EmailLength)
                .Matches(RegularExpression.EmailFormat).WithMessage(UserErrorMessage.EmailFormat);
        }
    }
}