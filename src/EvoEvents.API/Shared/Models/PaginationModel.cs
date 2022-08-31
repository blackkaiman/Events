using FluentValidation;
using Infrastructure.Utilities.Errors.ErrorMessages;

namespace EvoEvents.API.Shared.Models
{
    public class PaginationModel
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
    public class PaginationModelValidator : AbstractValidator<PaginationModel>
    {
        public PaginationModelValidator()
        {
            RuleFor(e => e.PageNumber)
                .GreaterThan(0).WithMessage(PaginationErrorMessage.InvalidPageNumber);
            RuleFor(e => e.ItemsPerPage)
                .GreaterThan(0).WithMessage(PaginationErrorMessage.InvalidPageSize);
        }
    }
}