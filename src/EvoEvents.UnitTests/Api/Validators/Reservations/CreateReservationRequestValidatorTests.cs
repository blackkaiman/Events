using EvoEvents.API.Requests.Events.Reservations;
using FluentValidation.TestHelper;
using Infrastructure.Utilities;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Validators.Reservations
{
    [TestFixture]
    public class CreateReservationRequestTests
    {
        private CreateReservationRequestValidator _validator;
        private CreateReservationRequest _request;

        [SetUp]
        public void Init()
        {
            _validator = new CreateReservationRequestValidator();
            SetupRequest();
        }

        private void SetupRequest()
        {
            _request = new CreateReservationRequest
            {
                EventId = 1,
                RegistrationInformation = new RegistrationInformation
                {
                    UserEmail = "a@abc.com",
                    AccompanyingPerson = "a@abcd.com"
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
            _request.RegistrationInformation.UserEmail = null;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.RegistrationInformation.UserEmail);
        }

        [Test]
        public void WhenEmailHasWrongFormat_ShouldReturnError()
        {
            _request.RegistrationInformation.UserEmail = PrimitiveGenerator.Alphanumeric(10);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.RegistrationInformation.UserEmail);
        }

        [Test]
        public void WhenAccompanyingPersonEmailHasWrongFormat_ShouldReturnError()
        {
            _request.RegistrationInformation.AccompanyingPerson = PrimitiveGenerator.Alphanumeric(10);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.RegistrationInformation.AccompanyingPerson);
        }
    }
}