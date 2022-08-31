using AutoFixture;
using EvoEvents.API.Requests.Users;
using FluentValidation.TestHelper;
using Infrastructure.Utilities;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Validators.Users
{
    [TestFixture]
    public class CreateUserRequestValidatorTests
    {
        private CreateUserRequestValidator _validator;
        private IFixture _fixture;
        private CreateUserRequest _request;

        [SetUp]
        public void Init()
        {
            _validator = new CreateUserRequestValidator();
            _fixture = new Fixture();
            SetupRequest();
        }

        private void SetupRequest()
        {
            _request = new CreateUserRequest
            {
                FirstName = "ASfdaf",
                LastName = "Snoi",
                Email = "abc@osif.com",
                Company = "IAL",
                Password = "parola"
            };
        }

        [Test]
        public void WhenFirstNameMissing_ShouldReturnError()
        {
            _request.FirstName = null;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.FirstName);
        }

        [Test]
        public void WhenLastNameMissing_ShouldReturnError()
        {
            _request.LastName = null;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.LastName);
        }

        [Test]
        public void WhenCompanyMissing_ShouldReturnError()
        {
            _request.Company = null;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Company);
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
        public void WhenFirstNameTooShort_ShouldReturnError()
        {
            _request.FirstName = PrimitiveGenerator.Alpha(1);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.FirstName);
        }

        [Test]
        public void WhenLastNameTooShort_ShouldReturnError()
        {
            _request.LastName = PrimitiveGenerator.Alpha(1);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.LastName);
        }

        [Test]
        public void WhenCompanyTooShort_ShouldReturnError()
        {
            _request.Company = PrimitiveGenerator.Alpha(1);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Company);
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
        public void WhenFirstNameTooLong_ShouldReturnError()
        {
            _request.FirstName = PrimitiveGenerator.Alpha(101);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.FirstName);
        }

        [Test]
        public void WhenEmailTooLong_ShouldReturnError()
        {
            _request.Email = PrimitiveGenerator.Alphanumeric(50) + "@" + PrimitiveGenerator.Alpha(50) + ".com";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Email);
        }

        [Test]
        public void WhenCompanyTooLong_ShouldReturnError()
        {
            _request.Company = PrimitiveGenerator.Alphanumeric(101);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Company);
        }

        [Test]
        public void WhenLastNameTooLong_ShouldReturnError()
        {
            _request.LastName = PrimitiveGenerator.Alpha(101);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.LastName);
        }

        [Test]
        public void WhenPasswordTooLong_ShouldReturnError()
        {
            _request.Password = PrimitiveGenerator.Alphanumeric(101);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Password);
        }

        [Test]
        public void WhenFirstNameHasWrongFormat_ShouldReturnError()
        {
            _request.FirstName = PrimitiveGenerator.Alpha(10) + "1";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.FirstName);
        }

        [Test]
        public void WhenLastNameHasWrongFormat_ShouldReturnError()
        {
            _request.LastName = PrimitiveGenerator.Alphanumeric(10) + "1";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.LastName);
        }

        [Test]
        public void WhenEmailHasWrongFormat_ShouldReturnError()
        {
            _request.Email = PrimitiveGenerator.Alphanumeric(10);

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Email);
        }

        [Test]
        public void WhenCompanyHasWrongFormat_ShouldReturnError()
        {
            _request.Company = PrimitiveGenerator.Alpha(10) + "!";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Company);
        }

        [Test]
        public void WhenPasswordHasWrongFormat_ShouldReturnError()
        {
            _request.Password = PrimitiveGenerator.Alpha(10) + " ";

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.Password);
        }
    }
}
