using EvoEvents.API.Requests.Reservations;
using FluentValidation.TestHelper;
using Infrastructure.Utilities;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Validators.Reservations
{
    [TestFixture]
    public class UnregisterUserRequestTests
    {
        private UnregisterUserRequestValidator _validator;
        private UnregisterUserRequest _request;

        [SetUp]
        public void Init()
        {
            _validator = new UnregisterUserRequestValidator();
            SetupRequest();
        }

        private void SetupRequest()
        {
            _request = new UnregisterUserRequest
            {
                EventId = 2,
                EmailModel = new EmailModel
                {
                    UserEmail = "adelap@yahoo.com",
                }
            };
        }

        [Test]
        public void WhenEventIdNegative_ShouldReturnError()
        {
            _request.EventId = -1;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.EventId);
        }

        [Test]
        public void WhenEmailMissing_ShouldReturnError()
        {
            _request.EmailModel.UserEmail = null;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.EmailModel.UserEmail);
        }

        [Test]
        public void WhenEmailHasWrongFormat_ShouldReturnError()
        {
            _request.EmailModel.UserEmail = PrimitiveGenerator.Alphanumeric(10);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.EmailModel.UserEmail);
        }
    }
}