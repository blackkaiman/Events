using FluentValidation;

namespace EvoEvents.API.Requests.Versions
{
    public class GetVersionRequest
    {
        public string Name { get; set; }
    }

    public class GetVersionRequestValidator : AbstractValidator<GetVersionRequest>
    {
        public GetVersionRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
