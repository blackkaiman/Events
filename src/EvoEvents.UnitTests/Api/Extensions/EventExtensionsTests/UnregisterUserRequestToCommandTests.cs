using EvoEvents.API.Requests.Events.Reservations;
using EvoEvents.API.Requests.Reservations;
using FluentAssertions;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Extensions.EventExtensionsTests
{
    [TestFixture]
    public class UnregisterUserRequestToCommandTests
    {
        [Test]
        public void ShouldReturnCorrectUnregisterUserCommand()
        {
            var request = new UnregisterUserRequest
            {
                EventId = 2,
                EmailModel = new EmailModel
                {
                    UserEmail = "adelap@yahoo.com",
                }
            };

            var result = request.ToCommand();

            result.EventId.Should().Be(request.EventId);
            result.UserEmail.Should().Be(request.EmailModel.UserEmail);
        }
    }
}