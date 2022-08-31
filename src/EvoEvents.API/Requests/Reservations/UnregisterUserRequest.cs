using FluentValidation;
using Infrastructure.Utilities.Errors.ErrorMessages;
using Microsoft.AspNetCore.Mvc;

namespace EvoEvents.API.Requests.Reservations
{
    public class UnregisterUserRequest
    {
        [FromRoute]
        public int EventId { get; set; }
        [FromBody]
        public EmailModel EmailModel { get; set; }    
    }

    public class UnregisterUserRequestValidator : AbstractValidator<UnregisterUserRequest>
    {
        public UnregisterUserRequestValidator()
        {
            RuleFor(e => e.EventId).NotEmpty()
                .GreaterThan(0).WithMessage(EventErrorMessage.EventNotFound);
            RuleFor(e => e.EmailModel)
               .NotEmpty()
               .SetValidator(new EmailModelValidator()) ;
        }
    }
}