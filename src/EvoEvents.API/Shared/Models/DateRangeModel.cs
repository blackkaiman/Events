using FluentValidation;
using Infrastructure.Utilities.Errors.ErrorMessages;
using System;

namespace EvoEvents.API.Shared.Models
{
    public class DateRangeModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class DateRangeModelValidator : AbstractValidator<DateRangeModel>
    {
        public DateRangeModelValidator()
        {
            RuleFor(e => e.FromDate)
                 .NotEmpty().WithMessage(EventErrorMessage.FromDateNull)
                 .Must(BeAValidDate).WithMessage(EventErrorMessage.FromDateValue);
            RuleFor(e => e.ToDate)
                .NotEmpty().WithMessage(EventErrorMessage.ToDateNull)
                .Must(BeAValidDate).WithMessage(EventErrorMessage.ToDateValue)
                .GreaterThanOrEqualTo(fd => fd.FromDate).WithMessage(EventErrorMessage.FromDateGraterThenToDate);
        }

        private bool BeAValidDate(DateTime date)
        {
            return date.ToUniversalTime() > DateTime.UtcNow;
        }
    }
}