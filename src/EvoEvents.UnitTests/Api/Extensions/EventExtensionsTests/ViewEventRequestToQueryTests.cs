using EvoEvents.API.Requests.Events;
using EvoEvents.API.Requests.Reservations;
using EvoEvents.Data.Models.Events;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EvoEvents.UnitTests.Api.Extensions.EventExtensionsTests
{
    [TestFixture]
    public class ViewEventRequestToQueryTests
    {
        [Test]
        public void ShouldReturnCorrectViewEventQuery()
        {
            var request = new ViewEventRequest
            {
                Id = 1,
                EmailModel = new EmailModel
                {
                    UserEmail = "paulac@yahoo.com"
                }
            };

            var result = request.ToQuery();

            result.Id.Should().Be(request.Id);
            result.UserEmail.Should().Be(request.EmailModel.UserEmail);
        }
    }
}