using AutoFixture;
using EvoEvents.API.Requests.Users;
using FluentValidation.TestHelper;
using Infrastructure.Utilities;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Validators.Users
{
    [TestFixture]
    public class LoginRequestValidatorTests
    {
        private LoginRequestValidator _validator;
        private LoginRequest _request;

        [SetUp]
        public void Init()
        {
            _validator = new LoginRequestValidator();
            SetupRequest();
        }

        private void SetupRequest()
        {
            _request = new LoginRequest
            { 
                Email = "abc@osif.com",
                Password = "parola"
            };
        }

        [Test]
        public void WhenEmailMissing_ShouldReturnError()
        {
            _request.Email = null;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Email);
        }

        [Test]
        public void WhenPasswordMissing_ShouldReturnError()
        {
            _request.Password = null;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Password);
        }

        [Test]
        public void WhenEmailTooShort_ShouldReturnError()
        {
            _request.Email = PrimitiveGenerator.Alphanumeric(1) + "@.com";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Email);
        }

        [Test]
        public void WhenPasswordTooShort_ShouldReturnError()
        {
            _request.Password = PrimitiveGenerator.Alphanumeric(1);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Password);
        }

        [Test]
        public void WhenEmailTooLong_ShouldReturnError()
        {
            _request.Email = PrimitiveGenerator.Alphanumeric(50) + "@" + PrimitiveGenerator.Alpha(50) + ".com";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Email);
        }

        [Test]
        public void WhenPasswordTooLong_ShouldReturnError()
        {
            _request.Password = PrimitiveGenerator.Alphanumeric(101);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Password);
        }

        [Test]
        public void WhenEmailHasWrongFormat_ShouldReturnError()
        {
            _request.Email = PrimitiveGenerator.Alphanumeric(10);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Email);
        }

        [Test]
        public void WhenPasswordHasWrongFormat_ShouldReturnError()
        {
            _request.Password = PrimitiveGenerator.Alpha(10) + " ";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Password);
        }
    }
}
