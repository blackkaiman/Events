using EvoEvents.API.Requests.Events;
using EvoEvents.API.Shared.Models;
using EvoEvents.Data.Models.Events;
using FluentAssertions;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Extensions.EventExtensionsTests
{
    [TestFixture]
    public class ViewAllEventsRequestToQueryTests
    {
        [Test]
        public void ShouldReturnCorrectGetEventsQuery()
        {
            var request = new ViewAllEventsRequest
            {
                PaginationModel = new PaginationModel 
                {
                    PageNumber = 1,
                    ItemsPerPage = 4
                },
                Filters = new EventFilterModel
                {
                    Email = "radu@yahoo.com",
                    Attending = false,
                    EventType = EventType.Talk
                }
            };

            var result = request.ToQuery();

            result.PageNumber.Should().Be(request.PaginationModel.PageNumber);
            result.ItemsPerPage.Should().Be(request.PaginationModel.ItemsPerPage);
            result.Email.Should().Be(request.Filters.Email);
            result.Attending.Should().Be(request.Filters.Attending);
            result.EventType.Should().Be(request.Filters.EventType);
        }
    }
}