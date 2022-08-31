using EvoEvents.API.Requests.Reservations;
using FluentValidation;
using Infrastructure.Utilities.Errors.ErrorMessages;
using Microsoft.AspNetCore.Mvc;

namespace EvoEvents.API.Requests.Events
{
    public class ViewEventRequest
    {
        [FromRoute]
        public int Id { get; set; }
        [FromBody]
        public EmailModel EmailModel { get; set; }
    }

    public class ViewEventRequestValidator : AbstractValidator<ViewEventRequest>
    {
        public ViewEventRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .GreaterThan(0).WithMessage(EventErrorMessage.EventNotFound);
            RuleFor(e => e.EmailModel)
              .NotEmpty()
              .SetValidator(new EmailModelValidator());
        }
    }
}