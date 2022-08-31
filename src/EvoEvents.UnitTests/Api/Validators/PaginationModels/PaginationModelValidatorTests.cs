using EvoEvents.API.Shared.Models;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Validators.PaginationModels
{
    [TestFixture]
    public class PaginationModelValidatorTests
    {
        private PaginationModelValidator _validator;
        private PaginationModel _model;

        [SetUp]
        public void Init()
        {
            _validator = new PaginationModelValidator();
            SetupRequest();
        }

        private void SetupRequest()
        {
            _model = new PaginationModel
            {
                PageNumber = 1,
                ItemsPerPage = 5
            };
        }

        [Test]
        public void WhenPageNumberLowerThanZero_ShouldReturnError()
        {
            _model.PageNumber = -1;

            _validator.TestValidate(_model).ShouldHaveValidationErrorFor(request => request.PageNumber);
        }

        [Test]
        public void WhenItemsPerPateLowerThanZero_ShouldReturnError()
        {
            _model.ItemsPerPage = -1;

            _validator.TestValidate(_model).ShouldHaveValidationErrorFor(request => request.ItemsPerPage);
        }
    }
}