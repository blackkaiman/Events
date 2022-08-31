using EvoEvents.Data.Models.Events;
using FluentValidation;
using Infrastructure.Utilities.Errors.ErrorMessages;
using Infrastructure.Utilities.RegEx;

namespace EvoEvents.API.Shared.Models
{
    public class EventFilterModel
    {
        public string Email { get; set; }
        public bool Attending { get; set; }
        public EventType EventType { get; set; }
    }
    public class EventFilterModelValidator : AbstractValidator<EventFilterModel>
    {
        public EventFilterModelValidator()
        {
            RuleFor(x => x.Email)
                .Length(7, 74).WithMessage(UserErrorMessage.EmailFormat)
                .Matches(RegularExpression.EmailFormat).WithMessage(UserErrorMessage.EmailFormat);
            RuleFor(x => x.EventType)
                .IsInEnum();
        }
    }
}