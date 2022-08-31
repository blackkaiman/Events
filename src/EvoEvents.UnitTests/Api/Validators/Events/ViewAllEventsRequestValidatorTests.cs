using EvoEvents.API.Requests.Events;
using NUnit.Framework;
using FluentValidation.TestHelper;
using EvoEvents.API.Shared.Models;
using EvoEvents.Data.Models.Events;

namespace EvoEvents.UnitTests.Api.Validators.Events
{
    [TestFixture]
    public class ViewAllEventsRequestValidatorTests
    {
        private ViewAllEventsRequestValidator _validator;
        private ViewAllEventsRequest _request;

        [SetUp]
        public void Init()
        {
            _validator = new ViewAllEventsRequestValidator();
            SetupRequest();
        }

        private void SetupRequest()
        {
            _request = new ViewAllEventsRequest
            {
                PaginationModel = new PaginationModel
                {
                    PageNumber = 1,
                    ItemsPerPage = 5
                },
                Filters = new EventFilterModel
                {
                    Email = "ion@evozon.com",
                    Attending = true,
                    EventType = EventType.Talk
                }
            };
        }

        [Test]
        public void WhenPaginationModelMissing_ShouldReturnError()
        {
            _request.PaginationModel = null;

            _validator.TestValidate(_request).ShouldHaveValidationErrorFor(request => request.PaginationModel);
        }
    }
}