using EvoEvents.API.Requests.Events;
using EvoEvents.API.Requests.Events.Reservations;
using FluentAssertions;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Extensions.EventExtensionsTests
{
    [TestFixture]
    public class CreateReservationRequestToCommandTests
    {
        [Test]
        public void ShouldReturnCorrectCreateReservationCommand()
        {
            var request = new CreateReservationRequest
            {
                EventId = 1,
                RegistrationInformation = new RegistrationInformation
                {
                    UserEmail = "a@abc.com",
                    AccompanyingPerson = "a@abcd.com"
                }
            };

            var result = request.ToCommand();

            result.EventId.Should().Be(request.EventId);
            result.UserEmail.Should().Be(request.RegistrationInformation.UserEmail);
            result.AccompanyingPersonEmail.Should().Be(request.RegistrationInformation.AccompanyingPerson);
        }
    }
}