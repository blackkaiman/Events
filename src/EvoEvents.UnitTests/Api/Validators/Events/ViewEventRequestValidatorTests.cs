using EvoEvents.API.Requests.Events;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Validators.Events
{
    [TestFixture]
    public class ViewEventRequestValidatorTests
    {
        private ViewEventRequestValidator _validator;
        private ViewEventRequest _request;

        [SetUp]
        public void Init()
        {
            _validator = new ViewEventRequestValidator();
            SetupRequest();
        }

        private void SetupRequest()
        {
            _request = new ViewEventRequest
            {
                Id=1
            };
        }

        [Test]
        public void WhenIdMissing_ShouldReturnError()
        {
            _request = new ViewEventRequest();

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Id);
        }

        [Test]
        public void WhenIdNegative_ShouldReturnError()
        {
            _request.Id = -1;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Id);
        }
    }
}