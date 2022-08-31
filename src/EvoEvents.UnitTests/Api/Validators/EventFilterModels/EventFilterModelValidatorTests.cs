using EvoEvents.API.Shared.Models;
using EvoEvents.Data.Models.Events;
using FluentValidation.TestHelper;
using Infrastructure.Utilities;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Validators.EventFilterModels
{
    [TestFixture]
    public class EventFilterModelValidatorTests
    {
        private EventFilterModelValidator _validator;
        private EventFilterModel _model;

        [SetUp]
        public void Init()
        {
            _validator = new EventFilterModelValidator();
            SetupRequest();
        }

        private void SetupRequest()
        {
            _model = new EventFilterModel
            {
                Email = "ion@evozon.com",
                Attending = true,
                EventType = EventType.Talk
            };
        }

        [Test]
        public void WhenEmailTooLong_ShouldReturnError()
        {
            _model.Email = PrimitiveGenerator.Alphanumeric(50) + "@" + PrimitiveGenerator.Alpha(50) + ".com";

            _validator.TestValidate(_model).ShouldHaveValidationErrorFor(request => request.Email);
        }

        [Test]
        public void WhenEmailTooShort_ShouldReturnError()
        {
            _model.Email = PrimitiveGenerator.Alphanumeric(1) + "@.com";

            _validator.TestValidate(_model).ShouldHaveValidationErrorFor(request => request.Email);
        }

        [Test]
        public void WhenEmailHasWrongFormat_ShouldReturnError()
        {
            _model.Email = PrimitiveGenerator.Alphanumeric(10);

            _validator.TestValidate(_model).ShouldHaveValidationErrorFor(request => request.Email);
        }

        [Test]
        public void WhenEventTypeNotInEnumBounds_ShouldReturnError()
        {
            _model.EventType = (EventType)1000;

            _validator.TestValidate(_model).ShouldHaveValidationErrorFor(request => request.EventType);
        }
    }
}